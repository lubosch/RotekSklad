Public Class Heslo

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox1.UseSystemPasswordChar = False
        Else
            TextBox1.UseSystemPasswordChar = True
        End If
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form78.heslo = "kjsndiofenelkdnoi"
        Me.Close()
    End Sub
    Private Sub nastav_heslo()
        Dim heslo As String = TextBox1.Text
        Me.UcetBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ucet.HesloColumn, heslo)
        If DataGridView2.RowCount = 1 Then
            Dim typ As String = DataGridView2.Rows(0).Cells(4).Value
            Select Case typ
                Case "Zakaz"
                    Form78.heslo = Form78.zakazkar
                Case "Admin"
                    Form78.heslo = Form78.admin
                Case "Sklad"
                    Form78.heslo = Form78.skladnik
            End Select
            Dim nick As String = DataGridView2.Rows(0).Cells(0).Value
            Form78.uzivatel = nick
            Dim mail As String = DataGridView2.Rows(0).Cells(2).Value.ToString
            Form78.mail = mail
            Dim mail_heslo As String = DataGridView2.Rows(0).Cells(3).Value.ToString
            Form78.mail_heslo = mail_heslo
        Else
            Form78.heslo = "fghernme|r||"
            Form78.mail_heslo = ""
            Form78.mail = ""
            Form78.uzivatel = "neznámy užívateľ"
        End If

        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        nastav_heslo()
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyData = Keys.Enter Then
            nastav_heslo()
        ElseIf e.KeyData = Keys.Escape Then
            Form78.heslo = "kjsndiofenelkdnoi"
            Form78.mail_heslo = ""
            Form78.mail = ""
            Form78.uzivatel = "neznámy užívateľ"
            Me.Close()
        End If
    End Sub

    Private Sub poverenie()
        Button3.Hide()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'TextBox1.Text=Form78.SimpleCrypt(TextBox1.Text)
        RotekIS.SQL.Account_SQL.Add_Type("admin")
        RotekIS.SQL.Account_SQL.Add_Type("sklad")
        RotekIS.SQL.Account_SQL.Add_Type("zakaz")


        For i As Integer = 0 To DataGridView2.RowCount - 1
            Dim nick As String = DataGridView2(0, i).Value.ToString
            Dim pass As String = DataGridView2(1, i).Value.ToString
            Dim mail As String = DataGridView2(2, i).Value.ToString
            Dim mail_pass As String = DataGridView2(3, i).Value.ToString
            Dim prava As String = DataGridView2(4, i).Value.ToString
            Dim pravaID As Integer = 0
            If prava = "Admin" Then
                pravaID = 1
            ElseIf prava = "Zakaz" Then
                pravaID = 2
            ElseIf prava = "Sklad" Then
                pravaID = 3
            End If

            RotekIS.SQL.Account_SQL.Add(nick, nick, pass, Nothing, mail, mail_pass, pravaID)
        Next
        Chyby.Show("Hotovo")

    End Sub

    Private Sub Heslo_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        '   Form78.premen()
    End Sub


    Private Sub Heslo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Ucet' table. You can move, or remove it, as needed.
        poverenie()
        Me.UcetTableAdapter.Fill(Me.RotekDataSet.Ucet)

    End Sub
End Class