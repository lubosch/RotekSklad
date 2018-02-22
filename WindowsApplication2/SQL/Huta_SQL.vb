Imports WindowsApplication2.RotekDataSetTableAdapters

Public Class Huta_SQL
    Public id As Integer
    Public druh As String
    Public nazov As String
    Public sirka As Integer
    Public rozmer As Integer
    Public s_rozmer As Integer
    Public velkost As Integer
    Public kusov As Integer
    Public typ As String
    Public hustota As String
    Public cena As String



    Public Sub New(druh As String, nazov As String, sirka As Integer, rozmer As Integer, s_rozmer As Integer, velkost As Integer, kusov As Integer, typ As String)
        Me.druh = druh
        Me.nazov = nazov
        Me.velkost = velkost
        Me.rozmer = rozmer
        Me.s_rozmer = s_rozmer
        Me.sirka = sirka
        Me.kusov = kusov
        Me.typ = typ

        Dim hutaTableAdapter As HutaTableAdapter = New HutaTableAdapter
        get_ID()
    End Sub

    Public Sub New(id As Integer)
        Me.id = id
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT TOP 1 * FROM Huta WHERE ID = '" & id & "' ", dd)

        Me.druh = dd.Rows(0).Item("Druh")
        Me.nazov = dd.Rows(0).Item("Nazov")
        Me.velkost = dd.Rows(0).Item("Velkost")
        Me.rozmer = dd.Rows(0).Item("Rozmer")
        Me.s_rozmer = dd.Rows(0).Item("S_rozmer")
        Me.sirka = dd.Rows(0).Item("Sirka")
        Me.typ = dd.Rows(0).Item("Typ")
        Me.hustota = dd.Rows(0).Item("Hustota")
        Me.cena = dd.Rows(0).Item("Cena")


        If get_ID() = -1 Then
            Throw New Exception
        End If
    End Sub

    Public Sub New(druh As String, nazov As String)
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT TOP 1 * FROM Huta WHERE Druh = '" & druh & "' AND Nazov = '" & nazov & "' AND pocet = '0' AND CONVERT(decimal(13,3) , Replace(Hustota, ',','.')) > 0 ", dd)
        If dd.Rows.Count > 0 Then
            Me.hustota = dd.Rows(0).Item("Hustota")
        Else
            Me.hustota = 0
        End If

    End Sub

    Public Sub add_to_stock(kusov As Integer)
        save()
        SQL_main.Odpalovac("UPDATE Huta SET Kusov = Kusov + (" & kusov & ") WHERE ID = " & id)
    End Sub

    Public Function get_ID() As Integer
        Dim hutaTableAdapter As HutaTableAdapter = New HutaTableAdapter
        Dim tmp As String = hutaTableAdapter.byMaterial(druh, nazov, rozmer, velkost, sirka, typ, s_rozmer).ToString
        If String.IsNullOrEmpty(tmp) Then
            id = -1
        Else
            id = tmp
        End If

        Return id
    End Function

    Public Function save() As Integer
        If get_ID() = -1 Then
            SQL_main.Odpalovac("Insert INTO Huta (Druh,  Nazov, Hustota, Sirka, Rozmer, Velkost, Cena, pocet, srot, srotcena, Vaha, Kusov, Typ, S_rozmer) VALUES ('" + druh + "', '" + nazov + "', (SELECT TOP 1 Hustota FROM Huta WHERE Nazov = '" & nazov & "' AND Druh = '" & druh & "' ) ," & sirka & "," & rozmer & "," & velkost & ",'" & 0 & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 0 & "'," & 0 & ",'" + typ + "'," & s_rozmer & ")")
        End If
        Return get_ID()
    End Function


    Public Shared Function rozmer_slovom(sirka As Integer, rozmer As Integer, srozmer As Integer, velkost As Integer, typ As String) As String
        Dim slovo As String = ""
        Select Case typ
            Case "Valec"
                slovo = rozmer & "dx" & velkost
            Case "Plech"
                slovo = sirka & "x" & rozmer & "x" & velkost
            Case "Rurka"
                slovo = sirka & "d x " & rozmer & "D x " & velkost
            Case "6 - hran"
                slovo = rozmer & " x " & velkost
            Case "L - profil"
                slovo = sirka & "a x " & rozmer & "b x " & srozmer & "s x " & velkost
            Case "U - profil"
                slovo = sirka & "a x " & rozmer & "b x " & srozmer & "s x " & velkost
            Case "Jokelt"
                slovo = sirka & "a x " & rozmer & "b x " & srozmer & "s x " & velkost
            Case "Hranol"
                slovo = sirka & " x " & rozmer & " x " & velkost

        End Select

        Return slovo
    End Function


    Public Shared Function objem(ByVal rozmer As Decimal, ByVal velkost As Decimal, ByVal sirka As Decimal, ByVal s_rozmer As Decimal, ByVal typ As String) As Double
        Dim obj As Double = 0
        sirka = sirka / 1000
        velkost = velkost / 1000
        rozmer = rozmer / 1000
        s_rozmer = s_rozmer / 1000

        If StrComp(typ, "Valec") = 0 Then
            obj = Math.PI * rozmer * rozmer * velkost / 4
        ElseIf StrComp(typ, "Plech") = 0 Then
            obj = rozmer * velkost * sirka
        ElseIf StrComp(typ, "Rurka") = 0 Then
            obj = Math.PI * rozmer * rozmer / 4 - Math.PI * sirka * sirka / 4
            obj = obj * velkost
        ElseIf StrComp(typ, "6 - hran") = 0 Then
            obj = velkost * 6 * rozmer * rozmer / 4 * Math.Tan(Math.PI / 6)
        ElseIf StrComp(typ, "L - profil") = 0 Then
            obj = sirka * rozmer - (sirka - s_rozmer) * (rozmer - s_rozmer)
            obj = obj * velkost
        ElseIf StrComp(typ, "U - profil") = 0 Then
            obj = s_rozmer * rozmer * 2 + s_rozmer * (sirka - s_rozmer - s_rozmer)
            obj = obj * velkost
        ElseIf StrComp(typ, "Jokelt") = 0 Then
            obj = sirka * rozmer - (sirka - s_rozmer - s_rozmer) * (rozmer - s_rozmer - s_rozmer)
            obj = obj * velkost
        ElseIf StrComp(typ, "Hranol") = 0 Then
            obj = rozmer * velkost * sirka
        End If
        Return obj
    End Function




End Class
