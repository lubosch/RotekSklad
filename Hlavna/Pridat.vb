Imports System.Data.SqlClient

Public Class Pridatt





    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button1.Enter, MyBase.Enter
        klik()

    End Sub
    Sub klik()

        Dim meno, priez As String
        meno = TextBox1.Text
        priez = TextBox2.Text
        Dim con As New SqlConnection
        Dim ds As New DataSet
        Dim cmd As SqlCommand

        Dim nula As Double
        Dim sql As String
        Dim mp As String
        Dim cesta As String
        cesta = "\\192.168.1.140\admin\Sklad\"
        cesta = Replace(cesta, "Rotek sklad.exe", "")
        cesta = Replace(cesta, "Rotek sklad.EXE", "")
        con.ConnectionString = My.Settings.Rotek2

        con.Open()
        nula = 0

        mp = meno & " " & priez
        If mp.Length > 30 Then
            Chyby.Show("Dlhé meno, priezvisko")
            Exit Sub

        End If

        sql = "Insert INTO Rotek (Meno, Priezvisko, Menpr, pocet, Spolu) VALUES ('" + meno + "', '" + priez + "', '" + mp + "','" & nula & "',0)"
        cmd = New SqlCommand(sql, con)
        cmd.ExecuteNonQuery()

        con.Close()
        Me.Close()


    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            klik()
        End If
    End Sub

    Private Sub Pridat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Pridat_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class