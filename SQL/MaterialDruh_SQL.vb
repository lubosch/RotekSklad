Imports WindowsApplication2.RotekDataSetTableAdapters
Imports WindowsApplication2.RotekDataSet

Public Class MaterialDruh_SQL
    Public id As Integer
    Public druh_id As String
    Public nazov As String

    Public Sub New(nazov As String)
        Me.nazov = nazov
        get_ID()
    End Sub

    Public Sub New(id As Integer)
        Me.id = id
        Dim dd As DataTable = New DataTable
        SQL_main.AddCommand("SELECT TOP 1 Nazov, Druh_ID")
        SQL_main.AddCommand("~FROM MaterialDruh")
        SQL_main.AddCommand("~WHERE ID = " & id & " ")
        SQL_main.Commit_List(dd)

        Me.nazov = dd.Rows(0).Item("Nazov")

        If get_ID() = -1 Then
            Throw New Exception
        End If
    End Sub

    Public Function get_ID() As Integer
        Dim mtda As MaterialDruhTableAdapter = New MaterialDruhTableAdapter()
        Dim tmp As String = mtda.findByNazov(nazov)

        If String.IsNullOrEmpty(tmp) Then
            id = -1
        Else
            id = tmp
        End If

        Return id
    End Function

    Public Sub combineInto(druh2 As MaterialDruh_SQL)
        SQL_main.AddCommand("UPDATE MaterialNazov SET Druh_ID  = " + druh2.id.ToString + " WHERE Druh_ID = " + Me.id.ToString)
        SQL_main.AddCommand("UPDATE MaterialDruh SET Druh_ID  = " + druh2.id.ToString + " WHERE ID = " + Me.id.ToString)
        SQL_main.Commit_Transaction()
    End Sub

End Class
