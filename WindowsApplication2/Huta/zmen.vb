Public Class zmen

    Property rozmer As Object
    Property nazov As Object
    Property druh As Object
    Property velkost As Object

    Property zakazka As String

    Private Sub zmen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = druh
        TextBox3.Text = nazov
        TextBox4.Text = rozmer
        TextBox5.Text = velkost
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New OleDb.OleDbConnection
        Dim sql As String
        Dim cesta As String
        cesta = "\\192.168.1.140\admin\Sklad\"
        cesta = Replace(cesta, "Rotek sklad.exe", "")
        cesta = Replace(cesta, "Rotek sklad.EXE", "")
        con.ConnectionString = my.Settings.Rotek2      '"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cesta & "Rotek.mdb"
        con.Open()

        sql = "UPDATE Huta SET Druh='" + TextBox1.Text + "', Ident='" + TextBox2.Text + "', Nazov='" + TextBox3.Text + "', Rozmer='" + TextBox4.Text + "', Velkost='" & TextBox5.Text & "' WHERE Druh='" + druh + "' AND Nazov='" + nazov + "' AND Rozmer='" + rozmer + "' AND Velkost='" & velkost & "' AND pocet='" & 1 & "' AND zakazka='" + zakazka + "'"
        Dim cmd As New OleDb.OleDbCommand
        cmd = New OleDb.OleDbCommand(sql, con)
        cmd.ExecuteNonQuery()
        Me.Close()
    End Sub
End Class