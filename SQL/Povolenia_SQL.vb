Public Class Povolenia_SQL

    Public Shared Function getRights(typ As String) As List(Of Dictionary(Of String, Boolean))
        Dim dt As DataTable = New DataTable
        SQL_main.List("SELECT Nazov, Administrator, Zakazkar, Skladnik, Ktokolvek FROM Povolenia Where Typ = '" + typ + "'", dt)
        Dim d As List(Of Dictionary(Of String, Boolean)) = New List(Of Dictionary(Of String, Boolean))
        d.Add(New Dictionary(Of String, Boolean))
        d.Add(New Dictionary(Of String, Boolean))
        d.Add(New Dictionary(Of String, Boolean))
        d.Add(New Dictionary(Of String, Boolean))

        For i As Integer = 0 To dt.Rows.Count - 1
            d(0).Add(dt.Rows(i).Item(0), Integer.Parse(dt.Rows(i).Item(1)))
            d(1).Add(dt.Rows(i).Item(0), Integer.Parse(dt.Rows(i).Item(2)))
            d(2).Add(dt.Rows(i).Item(0), Integer.Parse(dt.Rows(i).Item(3)))
            d(3).Add(dt.Rows(i).Item(0), Integer.Parse(dt.Rows(i).Item(4)))
        Next

        Return d
    End Function

End Class
