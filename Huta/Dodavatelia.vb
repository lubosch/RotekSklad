Imports WindowsApplication2.RotekDataSetTableAdapters

Public Class Dodavatelia


    Private Sub Dodavatelia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Dodavatel' table. You can move, or remove it, as needed.
        Me.DodavatelTableAdapter.Fill(Me.RotekDataSet.Dodavatel)

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        stuk()
    End Sub
    Private Sub stuk()
        Dim nazov As String = TextBox2.Text
        If nazov.Length = 0 Then
            Exit Sub
        End If
        Dim dodavatel_ID As String
        Dim dd As DodavatelTableAdapter = New DodavatelTableAdapter
        dodavatel_ID = dd.byNazov(nazov)
        If String.IsNullOrEmpty(dodavatel_ID) Then
            Chyby.Show("Nenašiel sa taký dodávateľ")
            Exit Sub
        End If

        If MessageBox.Show("Naozaj chcete navždy vymazať dodávateľa """ & nazov & """ ? Táto zmena je nezvratná. ", "Zmazať dodávateľa", MessageBoxButtons.YesNo) = vbYes Then
            SQL_main.AddCommand("UPDATE Prijemky SET Dodavatel_ID = NULL WHERE Dodavatel_ID = " & dodavatel_ID)
            SQL_main.AddCommand("DELETE FROM Dodavatel WHERE ID = " & dodavatel_ID)
            SQL_main.Commit_Transaction()
            Me.DodavatelTableAdapter.Fill(Me.RotekDataSet.Dodavatel)
        End If

    End Sub

    Private Sub ListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseClick
        If ListBox1.SelectedItems.Count > 0 Then
            TextBox2.Text = ListBox1.SelectedValue
            TextBox2.Focus()
            TextBox2.Select(TextBox2.Text.Length, 0)
        End If
    End Sub
End Class