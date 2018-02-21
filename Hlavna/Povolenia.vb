Public Class Povolenia

    Private Sub Povolenia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.SkladList' table. You can move, or remove it, as needed.
        Me.SkladListTableAdapter.Fill(Me.RotekDataSet.SkladList)
        Me.PovoleniaTableAdapter.Fill(Me.RotekDataSet.Povolenia)
        RadioButton1.Checked = True
    End Sub


    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            PovoleniaBindingSource.Filter = "Typ = 'Huta'"
        End If
    End Sub

   
    'tracks for PositionChanged event last row
    Private LastDataRow As DataRow = Nothing

    ''' <SUMMARY>
    ''' Checks if there is a row with changes and
    ''' writes it to the database
    ''' </SUMMARY>
    Private Sub UpdateRowToDatabase()
        If LastDataRow IsNot Nothing Then
            If LastDataRow.RowState = DataRowState.Modified Then
                PovoleniaTableAdapter.Update(LastDataRow)
            End If
        End If
    End Sub



    Private Sub PovoleniaBindingSource_PositionChanged(sender As Object, e As EventArgs) Handles PovoleniaBindingSource.PositionChanged
        ' if the user moves to a new row, check if the 
        '' last row was changed
        'Dim thisBindingSource As BindingSource = DirectCast(sender, BindingSource)
        'Dim ThisDataRow As DataRow = DirectCast(thisBindingSource.Current, DataRowView).Row
        ''If ThisDataRow.Equals(LastDataRow) Then
        '' we need to avoid to write a datarow to the 
        '' database when it is still processed. Otherwise
        '' we get a problem with the event handling of 
        ''the DataTable.
        ''Throw New ApplicationException("It seems the" & " PositionChanged event was fired twice for" & " the same row")
        ''End If

        'UpdateRowToDatabase()
        '' track the current row for next 
        '' PositionChanged event
        'LastDataRow = ThisDataRow
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            PovoleniaBindingSource.Filter = "Typ = 'Príjemka'"
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then
            PovoleniaBindingSource.Filter = "Typ = 'Výdajka'"
        End If
    End Sub

    Private Sub Povolenia_FormClosed(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        UpdateRowToDatabase()
    End Sub

    Private Sub PovoleniaBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles PovoleniaBindingSource.CurrentChanged
        'Dim thisBindingSource As BindingSource = DirectCast(sender, BindingSource)
        'Dim ThisDataRow As DataRow = DirectCast(thisBindingSource.Current, DataRowView).Row
        ''If ThisDataRow.Equals(LastDataRow) Then
        '' we need to avoid to write a datarow to the 
        '' database when it is still processed. Otherwise
        '' we get a problem with the event handling of 
        ''the DataTable.
        ''Throw New ApplicationException("It seems the" & " PositionChanged event was fired twice for" & " the same row")
        ''End If

        'UpdateRowToDatabase()
        '' track the current row for next 
        '' PositionChanged event
        'LastDataRow = ThisDataRow
    End Sub

    Private Sub PovoleniaBindingSource_CurrentItemChanged(sender As Object, e As EventArgs) Handles PovoleniaBindingSource.CurrentItemChanged
        Dim thisBindingSource As BindingSource = DirectCast(sender, BindingSource)
        Dim ThisDataRow As DataRow = DirectCast(thisBindingSource.Current, DataRowView).Row
        'If ThisDataRow.Equals(LastDataRow) Then
        ' we need to avoid to write a datarow to the 
        ' database when it is still processed. Otherwise
        ' we get a problem with the event handling of 
        'the DataTable.
        'Throw New ApplicationException("It seems the" & " PositionChanged event was fired twice for" & " the same row")
        'End If

        UpdateRowToDatabase()
        ' track the current row for next 
        ' PositionChanged event
        LastDataRow = ThisDataRow
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sl As SkladList_SQL = New SkladList_SQL(TextBox1.Text)
        sl.save()
        SkladListTableAdapter.Fill(RotekDataSet.SkladList)
        SkladListBindingSource.ResetBindings(False)
        TextBox1.Text = ""
    End Sub
End Class