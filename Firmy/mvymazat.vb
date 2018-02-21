Imports System.Data.SqlClient

Public Class mvymazať

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Rotek' table. You can move, or remove it, as needed.
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        rob()
    End Sub
    Private Sub rob()
        Dim con As New SqlConnection
        Dim com As New SqlCommand
        Dim cesta As String
        cesta = "\\192.168.1.140\admin\Sklad\"
        cesta = Replace(cesta, "Rotek sklad.exe", "")
        cesta = Replace(cesta, "Rotek sklad.EXE", "")
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim sql As String
        sql = "DELETE FROM Rotek WHERE Menpr='" + ComboBox1.Text + "' "
        com = New SqlCommand(sql, con)
        com.ExecuteNonQuery()

        con.Close()
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then rob()
    End Sub


End Class