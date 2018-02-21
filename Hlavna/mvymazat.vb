Public Class vymazať

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Rotek' table. You can move, or remove it, as needed.


        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.pocetColumn, 0)
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New OleDb.OleDbConnection
        Dim mDS As New DataSet
        Dim com As New OleDb.OleDbCommand
        Dim mdaorder As New OleDb.OleDbDataAdapter
        Dim cesta As String
        cesta = Application.ExecutablePath
        cesta = Replace(cesta, "Rotek sklad.exe", "")
        cesta = Replace(cesta, "Rotek sklad.EXE", "")
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cesta & "Rotek.mdb"
        Dim cb As New OleDb.OleDbCommandBuilder(mdaorder)

        con.Open()
        com.Connection = con
        com.CommandText = "DELETE FROM Rotek WHERE Menpr='" & ComboBox1.Text & "' "
        com.ExecuteNonQuery()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        con.Close()
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class