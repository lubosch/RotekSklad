Imports System.Data.SqlClient

Public Class mPridat





    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button1.Enter, MyBase.Enter
        klik()

    End Sub
    Sub klik()

        rob()
    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            klik()
        End If
    End Sub

    Private Sub Pridat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Pridat_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub rob()
        Dim meno As String
        meno = TextBox1.Text
        Dim con As New SqlConnection
        Dim ds As New DataSet
        Dim cmd As SqlCommand
        Dim sql As String
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        sql = "Insert INTO Rotek (Meno, Priezvisko, Menpr, pocet, Spolu) VALUES ('Firma', ' od ', '" + meno + "'," & 0 & ",0)"
        cmd = New SqlCommand(sql, con)
        cmd.ExecuteNonQuery()
        con.Close()
        Me.Close()
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then rob()

    End Sub
End Class