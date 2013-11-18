Public Class Podzakazka_SQL

    Public Nazov As String
    Public Zakazka As String
    Public Tepelna As String
    Public Povrchova As String
    Public Cena As String
    Public CenaKs As String
    Public Kusov As Integer

    Public Sub New(nazov As String, zakazka As String)
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT * FROM Zakazka WHERE Zakazka = '" & zakazka & "' AND pocet = 2 AND Podzakazka = '" & nazov & "'", dd)
        Me.Nazov = nazov
        Me.Zakazka = zakazka
        Me.Tepelna = dd.Rows(0).Item("Tepel_uprava")
        Me.Povrchova = dd.Rows(0).Item("Povrch_uprava")
        Me.CenaKs = dd.Rows(0).Item("Cena").ToString
        Me.Kusov = dd.Rows(0).Item("Kusov")
        If CenaKs <> "" Then
            Dim tmp As Decimal = Decimal.Parse(CenaKs)
            Cena = String.Format("{0:N3}", tmp * Kusov)
        Else
            Cena = ""
        End If



    End Sub

End Class
