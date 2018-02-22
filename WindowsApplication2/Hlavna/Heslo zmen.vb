Public Class Heslozmen
    Private mode As Integer
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        mode = 0
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal i As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        mode = 1
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox1.UseSystemPasswordChar = False
            TextBox2.UseSystemPasswordChar = False
            TextBox3.UseSystemPasswordChar = False
        Else
            TextBox1.UseSystemPasswordChar = True
            TextBox2.UseSystemPasswordChar = True
            TextBox3.UseSystemPasswordChar = True

        End If
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Form78.heslo = "kjsndiofenelkdnoi"
        Me.Close()
    End Sub
    Private Sub stuk()
        Dim Path As String = Application.LocalUserAppDataPath + "\hub.dat"
        Dim path2 As String = My.Settings.Rotek3 & "hub.dat"
        If TextBox2.Text <> TextBox3.Text Then
            Chyby.Show("Nové heslá sa nezhodujú")
            Exit Sub
        End If
        If TextBox4.Text.Length > 0 AndAlso TextBox5.Text.Length = 0 Then
            Chyby.Show("Nezadané heslo mailu")
        End If
        If TextBox4.Text.Length > 0 Then
            Dim m As POP3Client = New POP3Client()
            m.RemoteServerAddress = "mail.rotek.sk"
            m.UserName = TextBox4.Text & Label5.Text
            m.Password = TextBox5.Text
            m.IncreaseNetworkCompatible = True
            If m.Login = False Then
                MessageBox.Show("Neplatné meno alebo heslo mailu")
                Exit Sub
            End If

        End If

        If mode = 0 Then
            Me.UcetBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ucet.HesloColumn, TextBox1.Text)
            If DataGridView1.RowCount = 1 Then
                Dim sql As String
                Form78.uzivatel = TextBox6.Text
                sql = "UPDATE Ucet SET Heslo='" & TextBox2.Text & "', Mail='" & TextBox4.Text & "@rotek.sk', Mail_heslo='" & TextBox5.Text & "', Nick='" & TextBox6.Text & "' WHERE Heslo='" & TextBox1.Text & "'"
                Form78.sqa(sql)
            Else
                Chyby.Show("Zlé staré heslo. Nič nebolo zmenené")
                Exit Sub
            End If
            Form78.uzivatel = TextBox6.Text
            Form78.mail = TextBox4.Text
            Form78.mail_heslo = TextBox5.Text

            Chyby.Show("Heslo zmenené")
            Me.Close()
        Else
            Dim nick As String = TextBox6.Text
            Dim heslo As String = TextBox2.Text
            If String.IsNullOrEmpty(nick) OrElse String.IsNullOrEmpty(heslo) Then
                Chyby.Show("Nevyplnene meno a heslo")
                Exit Sub
            End If
            Dim prava As String
            If RadioButton1.Checked = True Then
                Form78.heslo = Form78.skladnik
                prava = "Sklad"
            ElseIf RadioButton2.Checked = True Then
                prava = "Zakaz"
                Form78.heslo = Form78.zakazkar
            ElseIf RadioButton3.Checked = True Then
                prava = "Admin"
                Form78.heslo = Form78.admin
            Else
                Chyby.Show("Nevybratý typ používateľa")
                Exit Sub
            End If

            Dim sql As String
            sql = "INSERT INTO UCET (Nick, Heslo, Mail, Mail_heslo, Prava) Values('" & nick & "','" & heslo & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & prava & "')"
            Form78.sqa(sql)
            Form78.uzivatel = nick
            Form78.mail = TextBox4.Text
            Form78.mail_heslo = TextBox5.Text

            MessageBox.Show("Pridané")
            Me.Close()

        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        stuk()

    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyData = Keys.Enter Then
            stuk()

        ElseIf e.KeyData = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Heslozmen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Ucet' table. You can move, or remove it, as needed.
        Me.UcetTableAdapter.Fill(Me.RotekDataSet.Ucet)
        TextBox1.UseSystemPasswordChar = True
        TextBox2.UseSystemPasswordChar = True
        TextBox3.UseSystemPasswordChar = True
        If mode = 0 Then

            Me.UcetBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ucet.NickColumn, Form78.uzivatel)
            If DataGridView1.RowCount = 1 Then
                TextBox6.Text = DataGridView1.Rows(0).Cells(0).Value.ToString
                TextBox5.Text = DataGridView1.Rows(0).Cells(3).Value.ToString
                Try
                    TextBox4.Text = DataGridView1.Rows(0).Cells(2).Value.ToString.Substring(0, DataGridView1.Rows(0).Cells(2).Value.ToString.IndexOf("@"))
                Catch ex As Exception
                End Try

                TextBox3.Text = DataGridView1.Rows(0).Cells(1).Value.ToString
                TextBox2.Text = DataGridView1.Rows(0).Cells(1).Value.ToString
            End If
            RadioButton1.Hide()
            RadioButton2.Hide()
            RadioButton3.Hide()

        Else
            TextBox1.Hide()
            Label1.Hide()

        End If
    End Sub

    Private Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyData = Keys.Enter Then
            stuk()

        ElseIf e.KeyData = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyData = Keys.Enter Then
            stuk()

        ElseIf e.KeyData = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class