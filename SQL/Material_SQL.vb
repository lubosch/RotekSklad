Imports WindowsApplication2.RotekDataSetTableAdapters
Imports WindowsApplication2.RotekDataSet

Public Class Material_SQL
    Public id As Integer
    Public druh As String
    Public nazov As String
    Public sirka As Decimal
    Public rozmer As Decimal
    Public s_rozmer As Decimal
    Public velkost As Integer
    Public kusov As Integer
    Public typ As String
    Public hustota As String
    Public cena As String
    Public skladlist_id As Integer


    Public Sub New(skladlist_id As Integer, druh As String, nazov As String, sirka As Decimal, rozmer As Decimal, s_rozmer As Decimal, velkost As Integer, kusov As Integer, typ As String)
        Me.druh = druh
        Me.nazov = nazov
        Me.velkost = velkost
        Me.rozmer = rozmer
        Me.s_rozmer = s_rozmer
        Me.sirka = sirka
        Me.kusov = kusov
        Me.typ = typ
        Me.skladlist_id = skladlist_id

        get_ID()
    End Sub

    Public Sub New(id As Integer)
        Me.id = id
        Dim dd As DataTable = New DataTable
        SQL_main.AddCommand("SELECT TOP 1 md.Nazov Druh, mn.Nazov Nazov, m.Velkost, m.Rozmer, m.S_rozmer, m.Sirka, m.Typ, m.SkladList_ID, mv.Hustota, mc.Cena ")
        SQL_main.AddCommand("~FROM Material m ")
        SQL_main.AddCommand("~JOIN MaterialVseobecne mv ON mv.ID = m.Material_ID ")
        SQL_main.AddCommand("~JOIN MaterialNazov mn ON mn.ID = mv.Nazov_ID ")
        SQL_main.AddCommand("~JOIN MaterialDruh md ON md.ID = mn.Druh_ID ")
        SQL_main.AddCommand("~LEFT JOIN MaterialCena mc ON mc.Material_ID = m.ID  ")
        SQL_main.AddCommand("~WHERE m.ID = " & id & " ")
        SQL_main.Commit_List(dd)

        Me.druh = dd.Rows(0).Item("Druh")
        Me.nazov = dd.Rows(0).Item("Nazov")
        Me.velkost = dd.Rows(0).Item("Velkost")
        Me.rozmer = dd.Rows(0).Item("Rozmer")
        Me.s_rozmer = dd.Rows(0).Item("S_rozmer")
        Me.sirka = dd.Rows(0).Item("Sirka")
        Me.typ = dd.Rows(0).Item("Typ")
        Me.hustota = dd.Rows(0).Item("Hustota").ToString
        Me.cena = dd.Rows(0).Item("Cena").ToString
        Me.skladlist_id = dd.Rows(0).Item("SkladList_ID")

        If get_ID() = -1 Then
            Throw New Exception
        End If
    End Sub

    Public Sub New(druh As String, nazov As String)
        Dim dd As DataTable = New DataTable
        SQL_main.AddCommand("SELECT TOP 1 * FROM MaterialVseobecne mv ")
        SQL_main.AddCommand("~JOIN MaterialNazov mn ON mn.Vseobecne_ID = mv.ID ")
        SQL_main.AddCommand("~JOIN MaterialDruh md ON md.ID = mn.Druh_ID ")
        SQL_main.AddCommand("~WHERE md.Nazov = '" & druh & "' AND mn.Nazov = '" & nazov & "' AND Hustota > 0 ")
        SQL_main.Commit_List(dd)
        If dd.Rows.Count > 0 Then
            Me.hustota = dd.Rows(0).Item("Hustota")
        Else
            Me.hustota = 0
        End If

    End Sub

    Public Sub New(druh As String, nazov As String, sirka As Decimal, rozmer As Decimal, s_rozmer As Decimal, typ As String)
        Dim dd As DataTable = New DataTable

        Dim mvta As MaterialVseobecneTableAdapter = New MaterialVseobecneTableAdapter
        Dim material_vseobecne_id As Integer = mvta.byDruhNazov(druh, nazov)

        SQL_main.List("SELECT TOP 1 Cena FROM MaterialCena WHERE Material_ID IN (SELECT ID FROM Material m  WHERE m.Material_ID = " & material_vseobecne_id & ") ORDER BY Datum DESC", dd)
        If dd.Rows.Count > 0 Then
            Me.cena = dd.Rows(0).Item("Cena")
        Else
            Me.cena = 0
        End If

    End Sub

    Public Sub New(skladlist_id As Integer, druh As String, nazov As String, sirka As Decimal, rozmer As Decimal, s_rozmer As Decimal, velkost As Integer, typ As String)
        Dim dd As DataTable = New DataTable

        Dim mvta As MaterialVseobecneTableAdapter = New MaterialVseobecneTableAdapter
        Dim material_vseobecne_id As Integer = mvta.byDruhNazov(druh, nazov)

        SQL_main.List("SELECT TOP 1 Kusov FROM Material m WHERE m.SkladList_ID = " + skladlist_id + " AND m.Material_ID = " & material_vseobecne_id & " AND Sirka = " & sirka & " AND Rozmer = " & rozmer & " AND S_rozmer = " & s_rozmer & " AND Velkost = " & velkost & " AND Typ = '" & typ & "'", dd)
        If dd.Rows.Count > 0 Then
            Me.kusov = dd.Rows(0).Item("Kusov")
        Else
            Me.kusov = 0
        End If

    End Sub

    Public Sub add_to_stock(kusov As Integer)
        If save() <> "NULL" Then
            SQL_main.Odpalovac("UPDATE Material SET Kusov = Kusov + (" & kusov & ") WHERE ID = " & id)
        End If
    End Sub

    Public Function get_ID() As Integer
        Dim mta As MaterialTableAdapter = New MaterialTableAdapter()
        Console.Out.WriteLine(skladlist_id, "|" & druh & "|" & nazov & "|" & sirka & "|" & rozmer & "|" & s_rozmer & "|" & velkost & "|" & typ & "|")
        Dim tmp As String = mta.findByMaterial(skladlist_id, druh, nazov, rozmer, velkost, sirka, typ, s_rozmer)

        If String.IsNullOrEmpty(tmp) Then
            id = -1
        Else
            id = tmp
        End If

        Return id
    End Function

    Public Function save() As String
        If get_ID() = -1 Then
            If Me.velkost <> 0 AndAlso Me.rozmer <> 0 Then

                SQL_main.AddCommand("IF NOT EXISTS(SELECT * FROM MaterialDruh WHERE Nazov = '" & Me.druh & "')")
                SQL_main.AddCommand("~BEGIN")
                SQL_main.AddCommand("INSERT INTO MaterialDruh (Nazov) VALUES ('" & Me.druh & "')")
                SQL_main.AddCommand("END")
                SQL_main.Commit_Transaction()


                Dim mdta As MaterialDruhTableAdapter = New MaterialDruhTableAdapter
                Dim druh_id As Integer = mdta.findByNazov(Me.druh)

                SQL_main.AddCommand("IF NOT EXISTS(SELECT * FROM MaterialNazov WHERE Nazov = '" & Me.nazov & "' AND Druh_ID = (SELECT ID FROM MaterialDruh WHERE Nazov = '" & Me.druh & "') )")
                SQL_main.AddCommand("~BEGIN")
                SQL_main.AddCommand("INSERT INTO MaterialVseobecne (Nazov_ID) VALUES ( NULL ) ")
                SQL_main.AddCommand("INSERT INTO MaterialNazov (Nazov, Druh_ID, Vseobecne_ID) VALUES ('" & Me.nazov & "' , " & druh_id & " , (SELECT SCOPE_IDENTITY()) )")
                SQL_main.AddCommand("UPDATE MaterialVseobecne SET Nazov_ID = (SELECT SCOPE_IDENTITY()) WHERE ID = (SELECT Vseobecne_ID FROM MaterialNazov WHERE ID = (SELECT SCOPE_IDENTITY()))")
                SQL_main.AddCommand("END")
                SQL_main.Commit_Transaction()

                Dim mvta As MaterialVseobecneTableAdapter = New MaterialVseobecneTableAdapter
                Dim material_vseobecne_id As Integer = mvta.byDruhNazov(Me.druh, Me.nazov)

                SQL_main.AddCommand("Insert INTO Material (SkladList_ID, Sirka, Rozmer, S_rozmer, Velkost, Kusov, Typ, Material_ID) VALUES ( " & skladlist_id & " , " & sirka & " , " & rozmer & " , " & s_rozmer & " , " & velkost & " , " & 0 & " , '" & typ & "', " & material_vseobecne_id & ")")
                SQL_main.Commit_Transaction()
            Else
                Return "NULL"
            End If
        End If
        Return get_ID()

    End Function

    Public Sub saveCena(cena As Decimal)
        Me.cena = cena
        save()
        SQL_main.Odpalovac("Insert INTO MaterialCena (Material_ID, Cena, Datum) VALUES ( " & Me.id & " , " & cena & " , GETDATE() )")
    End Sub

    Public Sub saveHustota(hustota As Decimal)
        Me.hustota = hustota
        save()

        Dim mvta As MaterialVseobecneTableAdapter = New MaterialVseobecneTableAdapter
        Dim material_vseobecne_id As Integer = mvta.byDruhNazov(Me.druh, Me.nazov)
        SQL_main.Odpalovac("UPDATE MaterialVseobecne SET Hustota = " & hustota & "WHERE ID = " & material_vseobecne_id)
    End Sub

    Public Function rozmer_slovom() As String
        Return Huta_SQL.rozmer_slovom(Me.sirka, Me.rozmer, Me.s_rozmer, Me.velkost, Me.typ)
    End Function
    Public Function GetPlochac() As Material_SQL
        Return New Material_SQL(skladlist_id, druh, nazov, -1, Me.rozmer, -1, -1, 0, typ)

    End Function
    Public Shared Sub fill(ByRef dd As DataTable)
        dd.Clear()

        SQL_main.AddCommand("SELECT md.Nazov AS Druh, mn.Nazov, m.*")
        SQL_main.AddCommand("~FROM  Material AS m INNER JOIN")
        SQL_main.AddCommand("~   MaterialVseobecne AS mv ON mv.ID = m.Material_ID INNER JOIN")
        SQL_main.AddCommand("~   MaterialNazov AS mn ON mn.ID = mv.Nazov_ID INNER JOIN")
        SQL_main.AddCommand("~   MaterialDruh AS md ON md.ID = mn.Druh_ID")
        SQL_main.Commit_List(dd)

    End Sub

    Public Shared Sub fillFiltered(ByRef dd As DataTable, filter As String, nazov As String)
        dd.Clear()

        filter = filter.Replace("Nazov", "mn.Nazov")
        filter = filter.Replace("Kusov", "m.Kusov")
        filter = filter.Replace("Typ", "m.Typ")
        filter = filter.Replace("Druh", "md.Nazov")

        SQL_main.AddCommand("SELECT md.Nazov AS Druh, mn.Nazov, m.*")
        SQL_main.AddCommand("~FROM  Material AS m INNER JOIN")
        SQL_main.AddCommand("~   MaterialVseobecne AS mv ON mv.ID = m.Material_ID INNER JOIN")
        SQL_main.AddCommand("~   MaterialNazov AS mn ON mn.ID = mv.Nazov_ID INNER JOIN")
        SQL_main.AddCommand("~   MaterialDruh AS md ON md.ID = mn.Druh_ID")
        SQL_main.AddCommand("~ WHERE " & filter)
        SQL_main.Commit_List(dd)



    End Sub
    Public Sub throwOut(pocet As Integer)
        SQL_main.AddCommand("INSERT INTO Odpady (Material_ID, Ks) VALUES( " & Me.id & " , " & pocet & ")")
        SQL_main.AddCommand("UPDATE Material SET Kusov = Kusov - " & pocet & " WHERE ID = " & Me.id)
        SQL_main.Commit_Transaction()
    End Sub

    Public Shared Sub getChoices(ByRef dd As DataTable, material As Material_SQL)
        Dim velkostMax As Integer = material.kusov * material.velkost
        dd.Clear()

        SQL_main.AddCommand("SELECT m.sirka, m.rozmer Hrubka, m.velkost, m.kusov, m.ID FROM Material as m INNER JOIN")
        SQL_main.AddCommand("~   MaterialVseobecne AS mv ON mv.ID = m.Material_ID INNER JOIN")
        SQL_main.AddCommand("~   MaterialNazov AS mn ON mn.ID = mv.Nazov_ID INNER JOIN")
        SQL_main.AddCommand("~   MaterialDruh AS md ON md.ID = mn.Druh_ID")
        SQL_main.AddCommand("~WHERE m.SkladList_ID = " & material.skladlist_id & " AND mn.Nazov ='" & material.nazov & "' AND md.Nazov='" & material.druh & "' AND m.Typ = '" & material.typ & "' ")

        Select Case material.typ
            Case "Plech"
                SQL_main.AddCommand("~AND m.Sirka = -1 AND m.Rozmer = " & material.rozmer & " AND m.S_rozmer = -1 AND m.Velkost = -1 AND m.Kusov >= " & material.kusov * material.sirka * material.velkost)
            Case "Valec"
                SQL_main.AddCommand("~AND m.Rozmer = " & material.rozmer & " AND m.Velkost >= " & material.velkost)
            Case "Rurka"
                SQL_main.AddCommand("~AND m.Sirka = " & material.sirka & " AND m.Rozmer = " & material.rozmer & " AND m.Velkost >= " & material.velkost)
            Case "6 - hran"
                SQL_main.AddCommand("~AND m.Rozmer = " & material.rozmer & " AND m.Velkost >= " & material.velkost)
            Case "L - profil"
                SQL_main.AddCommand("~AND m.Sirka = " & material.sirka & " AND m.Rozmer = " & material.rozmer & " AND m.S_rozmer = " & material.s_rozmer & " AND m.Velkost >= " & material.velkost)
            Case "U - profil"
                SQL_main.AddCommand("~AND m.Sirka = " & material.sirka & " AND m.Rozmer = " & material.rozmer & " AND m.S_rozmer = " & material.s_rozmer & " AND m.Velkost >= " & material.velkost)
            Case "Jokelt"
                SQL_main.AddCommand("~AND m.Sirka = " & material.sirka & " AND m.Rozmer = " & material.rozmer & " AND m.S_rozmer = " & material.s_rozmer & " AND m.Velkost >= " & material.velkost)
            Case "Hranol"
                SQL_main.AddCommand("~AND (m.Sirka = " & material.sirka & " AND m.Rozmer = " & material.rozmer & " AND m.Velkost >= " & material.velkost)
                SQL_main.AddCommand("~OR m.Velkost = " & material.velkost & " AND m.Rozmer = " & material.rozmer & " AND m.Sirka >= " & material.sirka)
                SQL_main.AddCommand("~OR m.Velkost = " & material.sirka & " AND m.Rozmer = " & material.rozmer & " AND m.Sirka >= " & material.velkost)
                SQL_main.AddCommand("~OR m.Sirka = " & material.velkost & " AND m.Rozmer = " & material.rozmer & " AND m.Velkost >= " & material.sirka)
                SQL_main.AddCommand("~OR m.Sirka = " & material.sirka & " AND m.Velkost = " & material.velkost & " AND m.Rozmer >= " & material.rozmer & " )")

        End Select

        SQL_main.AddCommand("~AND m.kusov > 0 ORDER BY m.Velkost")
        SQL_main.Commit_List(dd)



    End Sub

    Public Sub resetRozmery()
        Dim rozmery As Rozmery = Hvydat.get_rozmery(sirka, rozmer, s_rozmer, velkost, typ)
        Me.sirka = rozmery.sirka
        Me.rozmer = rozmery.rozmer
        Me.s_rozmer = rozmery.s_rozmer
        Me.velkost = rozmery.velkost

    End Sub


End Class
