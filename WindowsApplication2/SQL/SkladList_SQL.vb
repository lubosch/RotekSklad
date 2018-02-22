Imports System.Windows.Forms.ListView
Imports WindowsApplication2.RotekDataSetTableAdapters

Public Class SkladList_SQL
    Public nazov As String
    Public id As Integer

    Public Sub New(nazovv As String)
        Me.nazov = nazovv
    End Sub

    Public Sub save()
        If Me.nazov.Length >= 1 AndAlso get_id() = -1 Then
            SQL_main.Odpalovac("INSERT INTO SkladList(nazov) VALUES('" + Me.nazov + "')")
        End If
    End Sub

    Public Function get_id() As Integer
        If (Me.nazov.Length < 1) Then
            MessageBox.Show("Nazov nie je vyplneny")
            Return -1
        End If
        Dim mta As SkladListTableAdapter = New SkladListTableAdapter()
        Dim tmp As String = mta.find_id_by_nazov(Me.nazov)
        If String.IsNullOrEmpty(tmp) Then
            id = -1
        Else
            id = tmp
        End If

        Return id

    End Function

End Class

