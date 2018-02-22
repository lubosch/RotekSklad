Imports System.Data.SqlClient

Public Class vymazať

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}<>'{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New SqlConnection
        Dim mDS As New DataSet
        Dim com As New SqlCommand
        Dim mdaorder As New SqlDataAdapter
        Dim cesta As String
        cesta = "\\192.168.1.140\admin\Sklad\"
        cesta = Replace(cesta, "Rotek sklad.exe", "")
        cesta = Replace(cesta, "Rotek sklad.EXE", "")
        con.ConnectionString = My.Settings.Rotek2
        Dim cb As New SqlCommandBuilder(mdaorder)

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

    Private Sub ComboBox1_TextUpdate(sender As System.Object, e As System.EventArgs) Handles ComboBox1.TextUpdate
        ' Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' ", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")

    End Sub
End Class