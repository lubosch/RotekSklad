Imports System.Net.Mail
Imports System.Threading
Imports Ionic
'Imports Microsoft.Exchange.WebServices.Data

Public Class Mail
    Private firma As String
    Private veduci As String
    'Public ds As DataSet
    '   Public spravy As List(Of Sprava)
    Private accessLock As New Object
    Private endthread As Boolean

    Private zakazka As String
    Private tema As String
    Private dielec as String
    Public Sub New(ByVal zakazka As String, dielec as String, tema as String)
        InitializeComponent()
        Me.zakazka = zakazka
        Me.tema = tema
        Me.dielec = dielec
    End Sub

    Public Sub New(ByVal priloha As String, ByVal predmet As String, ByVal firma As String, ByVal veduci As String)
        InitializeComponent()
        Me.zakazka = ""
        If String.IsNullOrEmpty(priloha) Then
            LinkLabel1.Dispose()
        Else
            LinkLabel1.Text = priloha.Substring(priloha.LastIndexOf("\") + 1).Replace("•", "/")
            LinkLabel1.Text = LinkLabel1.Text.Substring(0, LinkLabel1.Text.LastIndexOf("."))
            LinkLabel1.Links(0).LinkData = priloha
        End If

        TextBox4.Text = predmet
        If veduci <> "0" Then
            Me.veduci = veduci
            Label8.Text = veduci
        Else
            Me.veduci = "0"
            Label8.Text = ""
        End If
        Label8.Text = Label8.Text & " : " & firma
        Me.firma = firma

        RichTextBox1.Text = "Dobrý deň" & vbCrLf & vbCrLf
    End Sub


    Private Sub Mail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Maily' table. You can move, or remove it, as needed.
        Me.MailyTableAdapter.Fill(Me.RotekDataSet.Maily)
        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        Dim firma_je As Boolean = False
        If veduci = "0" Then
            ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, firma)
            firma_je = True
        Else
            ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.NazovColumn, firma, RotekDataSet.ZoznamF.VeducColumn, veduci)
            If DataGridView1.RowCount = 0 OrElse String.IsNullOrEmpty(DataGridView1.Rows(0).Cells(0).Value.ToString) Then
                ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, firma)
                firma_je = True
            End If
        End If

        If DataGridView1.RowCount = 1 Then
            If String.IsNullOrEmpty(DataGridView1.Rows(0).Cells(0).Value.ToString) Then
            Else
                TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value
                If firma_je Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If
        End If

        MailyBindingSource.Sort = "Pocet DESC"
        DataRepeater1.BeginResetItemTemplate()
        DataRepeater1.ItemTemplate.Controls("Datum").Location = New Point(DataRepeater1.Width - 60, 0)
        DataRepeater1.ItemTemplate.Controls("Priloha").Location = New Point(DataRepeater1.Width - 30, 13)
        DataRepeater1.ItemTemplate.Controls("From").Size = New Size(203, 17)
        DataRepeater1.ItemTemplate.Controls("Subject").Size = New Size(234, 17)

        DataRepeater1.EndResetItemTemplate()
        TextBox1.Text = Form78.mail.Substring(0, Form78.mail.IndexOf("@rotek.sk"))
        TextBox3.Text = Form78.mail_heslo

        nacitat_maily()


        If Form78.uzivatel = "Ťapák" Then
            RichTextBox1.Text = RichTextBox1.Text & vbCrLf & "S pozdravom" & vbCrLf & vbCrLf & "Ťapák Daniel" & vbCrLf & vbCrLf & "tel: 043 5328551" & vbCrLf & "fax: 043 5328558" & vbCrLf & vbCrLf & "ROTEK s.r.o." & vbCrLf & "Závodná 459" & vbCrLf & "027 43 Nižná" & vbCrLf & "rotek@ rotek.sk" & vbCrLf & "www.rotek.sk"
        ElseIf Form78.uzivatel = "Argaláš" Then
            RichTextBox1.Text = RichTextBox1.Text & vbCrLf & "S pozdravom" & vbCrLf & vbCrLf & "Argaláš Ján" & vbCrLf & vbCrLf & "mob: 0908 696608" & vbCrLf & "tel: 043 5381246" & vbCrLf & "fax: 043 5328558" & vbCrLf & vbCrLf & "ROTEK s.r.o." & vbCrLf & "Závodná 459" & vbCrLf & "027 43 Nižná" & vbCrLf & "rotek@ rotek.sk" & vbCrLf & "www.rotek.sk"
        ElseIf Form78.uzivatel = "Jurčo" Then
            RichTextBox1.Text = RichTextBox1.Text & vbCrLf & "S pozdravom" & vbCrLf & vbCrLf & "Ing. Peter Jurčo" & vbCrLf & vbCrLf & "mob: 0907 805 887" & vbCrLf & "tel: 043 5328551" & vbCrLf & "fax: 043 5328558" & vbCrLf & vbCrLf & "ROTEK s.r.o." & vbCrLf & "Závodná 459" & vbCrLf & "027 43 Nižná" & vbCrLf & "rotek@ rotek.sk" & vbCrLf & "www.rotek.sk"
        ElseIf Form78.uzivatel = "Tomkuliak" Then
            RichTextBox1.Text = RichTextBox1.Text & vbCrLf & "S pozdravom" & vbCrLf & vbCrLf & "Juraj Tomkulia" & vbCrLf & vbCrLf & "mob: 0903 886 330" & vbCrLf & "tel: 043 5328551" & vbCrLf & "fax: 043 5328558" & vbCrLf & vbCrLf & "ROTEK s.r.o." & vbCrLf & "Závodná 459" & vbCrLf & "027 43 Nižná" & vbCrLf & "rotek@ rotek.sk" & vbCrLf & "www.rotek.sk"
        ElseIf Form78.uzivatel = "Paňko" Then
            RichTextBox1.Text = RichTextBox1.Text & vbCrLf & "S pozdravom" & vbCrLf & vbCrLf & "Paňko Tibor" & vbCrLf & vbCrLf & "tel: 043 5328551" & vbCrLf & "fax: 043 5328558" & vbCrLf & vbCrLf & "ROTEK s.r.o." & vbCrLf & "Závodná 459" & vbCrLf & "027 43 Nižná" & vbCrLf & "rotek@ rotek.sk" & vbCrLf & "www.rotek.sk"

        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try

            Dim od, heslo, komu, predmet, text As String
            od = TextBox1.Text & "@rotek.sk"
            heslo = TextBox3.Text
            komu = TextBox2.Text
            predmet = "|> " & TextBox4.Text
            text = RichTextBox1.Text

            'Dim service As ExchangeService = New ExchangeService(ExchangeVersion.Exchange2013)
            'service.Credentials = New WebCredentials(Form78.mail, Form78.mail_heslo)
            'service.AutodiscoverUrl(Form78.mail)
            'Dim em As EmailMessage = New EmailMessage(service)
            'em.ToRecipients.Add(komu)
            'em.Subject = predmet
            'em.Body = text

            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New System.Net.NetworkCredential(od, heslo)
            SmtpServer.Port = 587
            SmtpServer.Host = "mail.rotek.sk"
            SmtpServer.EnableSsl = False

            od = TextBox1.Text & "@rotek.sk"
            mail.From = New MailAddress(od, Form78.uzivatel)
            mail.To.Add(komu)
            mail.Subject = predmet
            mail.Body = text
            mail.Bcc.Add(Form78.mail)

            For Each cntrl As Control In FlowLayoutPanel2.Controls
                Dim oAttch As Attachment = New Attachment(CType(cntrl, LinkLabel).Links(0).LinkData)
                mail.Attachments.Add(oAttch)
            Next
            'For Each cntrl As Control In FlowLayoutPanel2.Controls
            '    'Dim oAttch As Microsoft.Exchange.WebServices.Data.Attachment = New Microsoft.Exchange.WebServices.Data.Attachment(CType(cntrl, LinkLabel).Links(0).LinkData)
            '    em.Attachments.AddFileAttachment(CType(cntrl, LinkLabel).Links(0).LinkData)
            'Next

            Me.Cursor = Cursors.WaitCursor
            'em.Send()
            'em.SendAndSaveCopy(WellKnownFolderName.SentItems)
            SmtpServer.Send(mail)
            Me.Cursor = Cursors.Default
            ' Sprava odoslana
            Chyby.Show("Poslané")


            Dim sql As String

            MailyBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Maily.MailColumn, komu)
            If DataGridView2.RowCount = 0 Then
                sql = "Insert INTO Maily (Nick, Mail, Datum, Pocet) VALUES ('" & veduci.ToLower & "','" & komu.ToLower & "','" & Now & "','" & 1 & "' )"
                Form78.sqa(sql)
            Else
                Dim krat As Integer = DataGridView2.Rows(0).Cells(2).Value
                sql = "Update Maily SET Pocet=" & krat + 1 & ", Datum='" & Now & "' WHERE Mail='" & komu & "'"
                Form78.sqa(sql)

            End If


            If String.IsNullOrEmpty(firma) Then
                Exit Sub
            End If
            'MessageBox.Show("")
            Dim m As String

            If CheckBox1.Checked = True Then
                ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, firma)
            Else
                ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.NazovColumn, firma, RotekDataSet.ZoznamF.VeducColumn, veduci)
            End If

            If DataGridView1.RowCount = 1 Then
                If String.IsNullOrEmpty(DataGridView1.Rows(0).Cells(0).Value.ToString) Then
                Else
                    m = DataGridView1.Rows(0).Cells(0).Value
                    If m = komu Then
                        ' MessageBox.Show("Je")
                        Exit Sub
                    Else
                        Dim sprava As String = "Chete zmeniť mail `" & m & "` na " & komu & "` ?"
                        If MessageBox.Show(sprava, "Zmeniť mail", MessageBoxButtons.YesNo) = vbNo Then
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If CheckBox1.Checked = True Then
                sql = "UPDATE ZoznamF SET Mail='" & komu & "' WHERE pocet=0 AND Nazov='" & firma & "'"
            Else
                sql = "UPDATE ZoznamF SET Mail='" & komu & "' WHERE pocet=1 AND Nazov='" & firma & "' AND Veduc='" & veduci & "'"
            End If


            ' MessageBox.Show(sql)
            Form78.sqa(sql)
        Catch ex As Security.Authentication.AuthenticationException
            Chyby.Show("Zlé prihlasovacie udaje")
            Chyby.Show(ex.ToString)


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start(LinkLabel1.Links(0).LinkData)
        LinkLabel1.LinkVisited = True
    End Sub

    Private Sub nacitat_maily()

        Dim From_text As Binding = New Binding("Text", Spust.dv, "From")
        From.DataBindings.Add(From_text)
        Dim Subject_text As Binding = New Binding("Text", Spust.dv, "Subject", True)
        Subject.DataBindings.Add(Subject_text)

        Dim Priloha_text As Binding = New Binding("Text", Spust.dv, "Priloha")
        Priloha.DataBindings.Add(Priloha_text)

        Dim Datum_text As Binding = New Binding("Text", Spust.dv, "Datum_format")
        Datum.DataBindings.Add(Datum_text)

        DataRepeater1.DataSource = Spust.dv

        'AddHandler Spust.mail_pribudol, AddressOf zmen

    End Sub



    Public Shared Function vrat_adresu(mess As EMail) As MailAddress
        Try

            Dim slovo As String
            Dim hlava As String = mess.Header
            Dim nick As String = ""
            slovo = hlava

            Dim adresa As MailAddress
            Dim pos As Integer
            pos = hlava.IndexOf("From:")
            If pos = -1 Then
            Else
                hlava = hlava.Substring(hlava.IndexOf("From:") + 6)
            End If
            slovo = hlava

            Dim slovo1, slovo2 As String
            pos = slovo.IndexOf("@")
            If pos = -1 Then
                slovo = hlava
                pos = slovo.IndexOf("@")
                If pos = -1 Then
                    'MessageBox.Show(mess.Header & vbCrLf & slovo)
                End If
            End If
            slovo1 = slovo.Substring(0, slovo.IndexOf("@"))
            slovo2 = slovo.Substring(slovo.IndexOf("@"))
            pos = slovo1.LastIndexOfAny({vbCrLf, "<", ">", ";", " ", "!", "#", "$", """", "&", "'", "(", ")", "*", ",", ":", "<", ">", "[", "]", "\", "`", "|", "{", "}"})
            If pos > -1 Then
                nick = slovo1.Substring(0, pos)
            End If
            slovo1 = slovo1.Substring(pos + 1)
            pos = slovo2.IndexOfAny({vbCrLf, "<", ">", ";", " ", "!", "#", "$", """", "&", "'", "(", ")", "*", ",", ":", "<", ">", "[", "]", "\", "`", "|", "{", "}"})
            If pos > -1 Then
                slovo2 = slovo2.Substring(0, pos)
            End If
            slovo = slovo1 & slovo2
            nick = nick.Replace("""", "")

            If nick.IndexOf("=?") = 0 Then
                nick = mess.From
            End If

            If mess.From.IndexOf("@") = -1 Then
                nick = mess.From
            End If


            Try
                adresa = New MailAddress(slovo, nick)
            Catch ex As Exception
                Debug.WriteLine(vbCrLf & slovo)
                Return New MailAddress("Nenaslo@sa.com", "Neviem")

            End Try

            Return adresa

        Catch ex As Exception
            Return New MailAddress("Nenaslo@sa.com", "Neviem")

        End Try
    End Function

    Private Sub zmen()

        Try
            If DataRepeater1.InvokeRequired Then
                If DataRepeater1.IsDisposed Then
                    Exit Sub
                Else
                    DataRepeater1.Invoke(Sub() zmen())
                End If
            End If
            If DataRepeater1.VerticalScroll.Value > 0 Or DataRepeater1.VerticalScroll.Maximum > 1000 Then
                Dim j As Integer = DataRepeater1.VerticalScroll.Value
                If j > DataRepeater1.VerticalScroll.Maximum - 1000 Then
                    DataRepeater1.VerticalScroll.Value = j + 1
                End If
                '  MessageBox.Show(DataRepeater1.VerticalScroll. & " SDA " & DataRepeater1.VerticalScroll.Maximum & " " & DataRepeater1.VerticalScroll.Minimum)
                ' Else : MessageBox.Show(DataRepeater1.VerticalScroll.Value & " " & DataRepeater1.VerticalScroll.Maximum & " " & DataRepeater1.VerticalScroll.Minimum)
            Else
                DataRepeater1.CurrentItemIndex = DataRepeater1.CurrentItemIndex + 1
                DataRepeater1.CurrentItemIndex = DataRepeater1.CurrentItemIndex - 1

            End If

        Catch ex As Exception
            ' Chyby.Show(ex.ToString)

        End Try

    End Sub


    Private Sub DataRepeater1_CurrentItemIndexChanged(sender As Object, e As EventArgs) Handles DataRepeater1.CurrentItemIndexChanged
        Dim jj As Integer = FlowLayoutPanel1.Controls.Count - 1
        For i As Integer = jj To 0 Step -1
            FlowLayoutPanel1.Controls(i).Dispose()
        Next

        ' TextBox5.Text = DataRepeater1.CurrentItem.Controls("From").Text
        Dim msg As Sprava = CType(Spust.dv(DataRepeater1.CurrentItemIndex)("Sprava"), Sprava)
        If String.IsNullOrEmpty(msg.From.DisplayName) Then
            TextBox5.Text = msg.From.Address
        Else
            TextBox5.Text = msg.From.DisplayName & " | " & msg.From.Address
        End If
        'If TextBox5.Text.Length = 0 Then
        '    '            TextBox5.Text = spravy(DataRepeater1.CurrentItemIndex).From.Address

        'End If
        'TextBox7.Text = Spust.spravy(DataRepeater1.CurrentItemIndex).Subject


        TextBox7.Text = msg.Subject
        WebBrowser1.DocumentText = msg.sprava_html


        Dim k As Integer = FlowLayoutPanel1.Height

        For Each ma As MailAttachment In msg.Priloha
            Dim lb As LinkLabel = New LinkLabel()
            lb.AutoSize = True
            lb.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            lb.Text = ma.FileName
            lb.Links(0).LinkData = ma.FilePath
            AddHandler lb.LinkClicked, AddressOf klikol_nalavo

            FlowLayoutPanel1.Controls.Add(lb)
        Next



        DataRepeater1.Focus()
        'MessageBox.Show(spravy(DataRepeater1.CurrentItemIndex).Priloha.Count)
    End Sub

    Private Sub klikol_napravo(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If e.Button = MouseButtons.Left Then
            Process.Start(e.Link.LinkData)
        ElseIf e.Button = MouseButtons.Right Then
            sender.dispose()
        End If

        'MessageBox.Show(e.Link.LinkData)
    End Sub

    Private Sub klikol_nalavo(sender As Object, e As LinkLabelLinkClickedEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim msg As Sprava = CType(Spust.dv(DataRepeater1.CurrentItemIndex)("Sprava"), Sprava)
            msg.Priloha(e.Link.LinkData).SaveToFile(System.IO.Path.GetTempPath & e.Link.LinkData)
            Process.Start(System.IO.Path.GetTempPath & e.Link.LinkData)
        ElseIf e.Button = MouseButtons.Right AndAlso zakazka <> "" Then
            ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
            ContextMenuStrip1.Tag=e.Link.LinkData
        End If

        'MessageBox.Show(e.Link.LinkData)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim msg As Sprava = CType(Spust.dv(DataRepeater1.CurrentItemIndex)("Sprava"), Sprava)
        TextBox2.Text = msg.From.Address
        ListBox1.Hide()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim msg As Sprava = CType(Spust.dv(DataRepeater1.CurrentItemIndex)("Sprava"), Sprava)
        TextBox2.Text = msg.From.Address
        TextBox4.Text = "RE: " & TextBox7.Text
        RichTextBox1.Text = RichTextBox1.Text & vbCrLf & vbCrLf & "______________________________________" & vbCrLf & "Pôvodná správa:" & vbCrLf & vbCrLf & msg.sprava
        ListBox1.Hide()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim priecinok As String = My.Settings.Rotek3 & "Pomocny\" & DataRepeater1.CurrentItem.Controls("Datum").Text
        priecinok.Replace(":", ".")
        Dim msg As Sprava = CType(Spust.dv(DataRepeater1.CurrentItemIndex)("Sprava"), Sprava)

        Dim i As Integer = 0
        System.IO.Directory.CreateDirectory(priecinok)
        For Each ma As MailAttachment In msg.Priloha
            ma.SaveToFile(priecinok & "\" & ma.FilePath)
        Next

        Dim zp As Zip.ZipFile = New Zip.ZipFile()
        zp.AddDirectory(priecinok)
        zp.Save(My.Settings.Rotek3 & "Pomocny\" & DataRepeater1.CurrentItem.Controls("Datum").Text & ".zip")
        zp.Dispose()

        System.IO.Directory.Delete(priecinok, True)
        Process.Start(My.Settings.Rotek3 & "Pomocny\" & DataRepeater1.CurrentItem.Controls("Datum").Text & ".zip")

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged


    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        If ListBox1.Focused = False Then
            ListBox1.Hide()
        End If
    End Sub

    Private Sub ListBox1_Leave(sender As Object, e As EventArgs) Handles ListBox1.Leave
        If TextBox2.Focused = False Then
            ListBox1.Hide()
        End If
    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        ListBox1.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            For Each f As String In OpenFileDialog1.FileNames
                Dim lb As LinkLabel = New LinkLabel()
                lb.AutoSize = True
                lb.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
                lb.Text = f.Substring(f.LastIndexOf("\") + 1)
                lb.Links(0).LinkData = f
                AddHandler lb.LinkClicked, AddressOf klikol_napravo
                FlowLayoutPanel2.Controls.Add(lb)


            Next
        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        TextBox2.Text = ListBox1.SelectedItems(0).ToString.Substring(0, ListBox1.SelectedItems(0).ToString.IndexOf(" |"))
        TextBox2.SelectionStart = TextBox2.Text.Length
    End Sub

    Private Sub TextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyData = Keys.Down Then
            Try
                ListBox1.SelectedItem = ListBox1.Items(ListBox1.SelectedIndex + 1)
            Catch ex As Exception
                ListBox1.SelectedItem = ListBox1.Items(0)
            End Try

        ElseIf e.KeyData = Keys.Up Then
            Try
                ListBox1.SelectedItem = ListBox1.Items(ListBox1.SelectedIndex - 1)
            Catch ex As Exception
                ListBox1.SelectedItem = ListBox1.Items(ListBox1.Items.Count - 1)
            End Try
        Else
            ListBox1.Show()
            Me.MailyBindingSource.Filter = String.Format("{0} LIKE '%{1}%' OR {2} LIKE '%{3}%'", RotekDataSet.Maily.NickColumn, TextBox2.Text, RotekDataSet.Maily.MailColumn, TextBox2.Text)
            ListBox1.Items.Clear()
            For i As Integer = 0 To DataGridView2.RowCount - 1
                ListBox1.Items.Add(DataGridView2.Rows(i).Cells(1).Value & " | " & DataGridView2.Rows(i).Cells(0).Value)
            Next
        End If
    End Sub

    Private Sub Mail_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler Spust.mail_pribudol, AddressOf zmen
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, firma)
            If DataGridView1.RowCount = 1 Then
                TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value.ToString
            Else
                TextBox2.Text = ""
            End If
        Else
            ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.NazovColumn, firma, RotekDataSet.ZoznamF.VeducColumn, veduci)
            If DataGridView1.RowCount = 1 Then
                TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value.ToString
            Else
                TextBox2.Text = ""
            End If

        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If PictureBox1.Tag = "0" Then
            Dim j As Integer = SplitContainer2.Panel2.Width

            SplitContainer1.SplitterDistance = SplitContainer1.Width - 50
            SplitContainer2.SplitterDistance = SplitContainer2.Width - j
            PictureBox1.Image = WindowsApplication2.My.Resources.Resources.fajka
            PictureBox1.Tag = 1
        Else
            SplitContainer1.SplitterDistance = SplitContainer1.Width * 0.56420000000000003
            SplitContainer2.SplitterDistance = SplitContainer2.Width * 0.63600000000000001
            PictureBox1.Image = WindowsApplication2.My.Resources.Resources.krizik
            PictureBox1.Tag = 0

        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If PictureBox2.Tag = "0" Then
            SplitContainer1.SplitterDistance = SplitContainer2.Width - SplitContainer2.SplitterDistance + 50
            SplitContainer2.SplitterDistance = 50
            PictureBox2.Image = WindowsApplication2.My.Resources.Resources.fajka
            PictureBox2.Tag = 1
        Else
            SplitContainer1.SplitterDistance = SplitContainer1.Width * 0.56420000000000003
            SplitContainer2.SplitterDistance = SplitContainer2.Width * 0.63600000000000001
            PictureBox2.Image = WindowsApplication2.My.Resources.Resources.krizik
            PictureBox2.Tag = 0

        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        Spust.dv.RowFilter = "From LIKE '%" & TextBox6.Text & "%'"
    End Sub

    Private Sub PridaťKZákazkeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PridaťKZákazkeToolStripMenuItem.Click
        Dim msg As Sprava = CType(Spust.dv(DataRepeater1.CurrentItemIndex)("Sprava"), Sprava)

        Dim cesta2 As String = My.Settings.Rotek3
        cesta2 = cesta2 & "\zakazky\" + zakazka + "\" & tema & "\"
       
        If cesta2.LastIndexOf("\") <> cesta2.Length - 1 Then
            cesta2 = cesta2 & "\"
        End If

        If dielec <> "" Then
            cesta2 = cesta2 & dielec

        End If

        If System.IO.Directory.Exists(cesta2) = False Then
            System.IO.Directory.CreateDirectory(cesta2)
        End If

        msg.Priloha(ContextMenuStrip1.Tag).SaveToFile(cesta2 & ContextMenuStrip1.Tag)
        If tema = "Vykresy\" Then
            dielec = dielec.Replace("\", "")
            Dim sql As String
            sql = "UPDATE Zakazka SET " & "Vykresy" & "='2' WHERE pocet=2 AND Zakazka='" & zakazka & "' AND Podzakazka='" & dielec & "'"
            Form78.sqa(sql)
        End If

        Chyby.Show("Pridané")

    End Sub
End Class



Public Class Sprava
    Public From As MailAddress
    Public Subject As String
    Public Datum As Date
    Public Priloha As EMail.MailAttachmentCollection
    Public sprava As String
    Public sprava_html As String
    Public Sub New()
    End Sub

    Public Sub New(ByVal From As MailAddress, ByVal Subject As String, ByVal Datum As Date, ByVal Priloha As EMail.MailAttachmentCollection, ByVal sprava As String, ByVal sprava_html As String)
        Me.From = From
        Me.Subject = Subject
        Me.Priloha = Priloha
        Me.Datum = Datum
        Me.sprava_html = sprava_html
        Me.sprava = sprava
    End Sub


End Class