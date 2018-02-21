Public Class Firma_SQL
    Public Nazov As String
    Public Mesto As String
    Public Ulica As String
    Public PSC As String
    Public Krajina As String
    Public ICO As String
    Public ICO_DPH As String

    Public Sub New(nazov As String)
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT * FROM ZoznamF WHERE Nazov = '" & nazov & "' AND pocet = 0", dd)
        Me.Nazov = nazov
        Me.Mesto = dd.Rows(0).Item("Mest").ToString
        Me.Ulica = dd.Rows(0).Item("Ulica").ToString
        Me.PSC = dd.Rows(0).Item("PSČ").ToString
        Me.Krajina = dd.Rows(0).Item("Krajina").ToString
        Me.ICO = dd.Rows(0).Item("ICO").ToString
        Me.ICO_DPH = dd.Rows(0).Item("DICO").ToString

    End Sub


End Class
