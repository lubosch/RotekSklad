Public Class Zakazka_SQL

    Public Firma As Firma_SQL
    Public Odovzdat As String
    Public Zaevidoval As String
    Public Objednavka As String
    Public Nazov As String
    Public Cislo_zakazky As String
    Public Datum_Pridania As Date
    Public Datum_Ukoncenia_Planovany As Date
    Public Datum_Ukoncenia_Skutocny As Date
    Public Datum_Ukoncenia_Skutocny_Slovo As String


    Public Podzakazky As DataTable
    Public Sub New(zakazka As String)
        Me.Cislo_zakazky = zakazka

        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT * FROM Zakazka WHERE Zakazka = '" & zakazka & "' AND pocet = 1", dd)
        If (dd.Rows.Count = 0) Then
            Return
        End If

        Me.Odovzdat = dd.Rows(0).Item("Veduci")
        Me.Zaevidoval = dd.Rows(0).Item("Zaevidoval")
        Me.Firma = New Firma_SQL(dd.Rows(0).Item("Zakaznik"))
        If dd.Rows(0).Item("Objednavka").ToString <> "" Then
            Me.Objednavka = dd.Rows(0).Item("Objednavka")
        Else
            Me.Objednavka = ""
        End If
        Me.Nazov = dd.Rows(0).Item("Meno")
        Me.Datum_Pridania = dd.Rows(0).Item("D_prijatia")
        Me.Datum_Ukoncenia_Planovany = dd.Rows(0).Item("D_plan")
        If dd.Rows(0).Item("D_real").ToString <> "" Then
            Me.Datum_Ukoncenia_Skutocny = dd.Rows(0).Item("D_real")
            Me.Datum_Ukoncenia_Skutocny_Slovo = Me.Datum_Ukoncenia_Skutocny.ToShortDateString
        Else
            Me.Datum_Ukoncenia_Skutocny = Date.MinValue
            Me.Datum_Ukoncenia_Skutocny_Slovo = "Neukončená"
        End If

        Podzakazky = New DataTable
        SQL_main.List("SELECT Podzakazka FROM Zakazka WHERE Zakazka = '" & zakazka & "' AND pocet = 2", Podzakazky)

    End Sub

    Public Function podzakazky_datatable() As DataTable
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT * FROM Zakazka WHERE Zakazka = '" & Me.Cislo_zakazky & "' AND pocet = 2", dd)
        Return dd
    End Function

End Class
