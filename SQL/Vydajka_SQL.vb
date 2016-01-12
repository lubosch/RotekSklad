Imports System.Windows.Forms.ListView
Imports WindowsApplication2.RotekDataSetTableAdapters

Public Class Vydajka_SQL
    Public datum As Date
    Public nazov As String
    Public poznamka As String
    Public vyhotovil As String
    Public pokazil_ID As String
    Public firemna_rezia As Boolean
    Public zakazka As String
    Public id As Integer

    Public Shared Function materialByName(nazov As String) As DataTable
        Dim dd As DataTable = New DataTable
        SQL_main.AddCommand("SELECT m.id 'ID', md.Nazov Druh, mn.Nazov Názov, m.Typ Typ, m.Sirka Šírka, m.Rozmer 'Priemer / Hrúbka', m.S_rozmer 'Stena', m.Velkost 'Dĺžka', (0-mv.Ks) 'Kusov materiálu', mv.ks1ks 'Narezených kusov',  mz.ID 'mz_ID', mz.Sirka 'Zvyšok: Šírka', mz.Rozmer 'Zvyšok:Rozmer / Hrúbka', mz.S_Rozmer 'Zvyšok:Stena',   (mz.Velkost) 'Zvyšok:Dĺžka', mv.Zvysok_ks 'Zvyšok:Kusov'")
        SQL_main.AddCommand("~FROM Material_Vydajka mv ")
        SQL_main.AddCommand("~JOIN Material m ON mv.Material_ID = m.ID ")
        SQL_main.AddCommand("~JOIN Vydajky v ON v.ID = mv.Vydajka_ID ")
        SQL_main.AddCommand("~LEFT JOIN Material mz ON mz.id = mv.Material_zvysok_ID ")
        SQL_main.AddCommand("~JOIN MaterialVseobecne mvs ON mvs.id = m.Material_ID ")
        SQL_main.AddCommand("~JOIN MaterialNazov mn ON mn.ID = mvs.Nazov_ID ")
        SQL_main.AddCommand("~JOIN MaterialDruh md ON md.id = mn.Druh_ID ")

        SQL_main.AddCommand("~WHERE v.Nazov = '" & nazov & "'")
        SQL_main.Commit_List(dd)
        Return dd
    End Function

    Public Sub New(nazovv As String)
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT TOP 1 v.*, z.Zakazka, rv.Zamestnanec_ID FROM Vydajky v LEFT JOIN Zakazka z ON z.ID = v.Zakazka_ID LEFT JOIN Reklamacia_Vydajka rv ON rv.Vydajka_ID = v.ID WHERE Nazov = '" & nazovv & "' ", dd)

        Me.id = dd.Rows(0).Item("ID")
        Me.datum = dd.Rows(0).Item("Datum")
        Me.nazov = dd.Rows(0).Item("Nazov")
        Me.poznamka = dd.Rows(0).Item("Poznamka").ToString
        Me.vyhotovil = dd.Rows(0).Item("Vyhotovil")
        Me.zakazka = dd.Rows(0).Item("Zakazka").ToString
        Me.pokazil_ID = dd.Rows(0).Item("Zamestnanec_ID").ToString
        Me.firemna_rezia = dd.Rows(0).Item("Firemna_rezia").ToString

    End Sub

    Public Shared Function vydajkyByMaterial(filter As String) As DataTable
        Dim stopky As Stopwatch = New Stopwatch
        stopky.Start()

        Dim dd As DataTable = New DataTable

        filter = filter.Replace("Nazov", "mn.Nazov")
        filter = filter.Replace("Kusov", "mv.Ks")
        filter = filter.Replace("Typ", "m.Typ")
        filter = filter.Replace("Druh", "md.Nazov")


        SQL_main.AddCommand("SELECT      DISTINCT(Vydajky.Nazov), Vydajky.Datum, Vydajky.Vyhotovil, Vydajky.Poznamka, Vydajky.Zakazka_ID, z.Zakazka, ")
        SQL_main.AddCommand("~E.Surname + ' ' + E.Name AS Pokazil")
        SQL_main.AddCommand("~FROM            Vydajky LEFT OUTER JOIN")
        SQL_main.AddCommand("~Zakazka AS z ON z.ID = Vydajky.Zakazka_ID LEFT OUTER JOIN")
        SQL_main.AddCommand("~          Reklamacia_Vydajka AS rv ON rv.Vydajka_ID = Vydajky.ID LEFT OUTER JOIN")
        SQL_main.AddCommand("~           Rotek.dbo.Employer AS E ON E.ID = rv.Zamestnanec_ID INNER JOIN")
        SQL_main.AddCommand("~           Material_Vydajka AS mv ON mv.Vydajka_ID = Vydajky.ID INNER JOIN")
        SQL_main.AddCommand("~           Material AS m ON m.ID = mv.Material_ID OR m.ID = mv.Material_zvysok_ID JOIN")
        SQL_main.AddCommand("~           MaterialVseobecne AS mvs ON mvs.ID = m.Material_ID JOIN")
        SQL_main.AddCommand("~           MaterialNazov AS mn ON mn.Vseobecne_ID = mvs.ID JOIN")
        SQL_main.AddCommand("~           MaterialDruh AS md ON md.ID = mn.Druh_ID ")

        If filter.Length > 0 Then
            SQL_main.AddCommand("~WHERE " & filter)
        End If
        SQL_main.AddCommand("~ORDER BY Nazov DESC")

        SQL_main.OdpalList(dd)
        Debug.WriteLine(stopky.ElapsedMilliseconds)
        Return dd
    End Function


    Public Shared Function vydajky(zakazka As String) As ListViewItem()

        Dim dd As DataTable = New DataTable
        Dim ztd As ZakazkaTableAdapter = New ZakazkaTableAdapter
        Dim zakazka_ID As Integer = ztd.Zakazka_by_zakazka(zakazka)
        SQL_main.List("SELECT Nazov FROM Vydajky WHERE Zakazka_ID = " & zakazka_ID, dd)
        Dim list(dd.Rows.Count - 1) As ListViewItem

        For i As Integer = 0 To dd.Rows.Count - 1
            list(i) = New ListViewItem(dd.Rows(i).Item("Nazov").ToString)
        Next

        Return list

    End Function

    Public Sub zmazat(material_id As Integer)

        Try
            Dim vm As Vydajka_Material = New Vydajka_Material(Me.id, material_id)


            If vm.material.typ = "Plech" Then
                vm.kusov = vm.kusov * vm.material.sirka * vm.material.velkost
                vm.material = vm.material.GetPlochac
            End If
            vm.material.add_to_stock(vm.kusov)


            If vm.zvysok IsNot Nothing Then

                vm.zvysok.add_to_stock(0 - vm.zv_kusov)

            End If

            vm.delete_me()


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub

    Public Sub delete_me()
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT * FROM Material_Vydajka mv WHERE mv.vydajka_ID = " & Me.id & " ", dd)

        Dim material_id As Integer
        For i As Integer = 0 To dd.Rows.Count - 1
            material_id = dd.Rows(i).Item("Material_ID")
            zmazat(material_id)
        Next

        SQL_main.Odpalovac("DELETE FROM Vydajky WHERE ID =  " & Me.id & " ")

    End Sub
End Class

Public Class Vydajka_Material
    Public vydajka_id As Integer

    Public material As Material_SQL
    Public material_id As Integer
    Public kusov As Integer

    Public zvysok As Material_SQL
    Public zvysok_id As Integer
    Public zv_kusov As Integer

    Public Sub New(vydajka_id As Integer, material_id As Integer)
        Me.vydajka_id = vydajka_id
        Me.material_id = material_id
        Me.material = New Material_SQL(material_id)

        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT TOP 1 * FROM Material_Vydajka mv WHERE mv.vydajka_ID = " & vydajka_id & " AND Material_id = " & material_id & " ", dd)


        kusov = 0 - dd.Rows(0).Item("Ks")

        If (dd.Rows(0).Item("Material_zvysok_ID").ToString <> "") Then
            Me.zvysok_id = dd.Rows(0).Item("Material_zvysok_ID")
            Me.zvysok = New Material_SQL(zvysok_id)
            Me.zv_kusov = dd.Rows(0).Item("Zvysok_ks")
        Else
            zvysok = Nothing
        End If

    End Sub

    Public Sub delete_me()
        SQL_main.Odpalovac("DELETE FROM Material_Vydajka WHERE Vydajka_ID = " & vydajka_id & " AND Material_ID = " & material_id)
    End Sub

End Class
