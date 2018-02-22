Imports System.Net.Mail

Public Class Chyby
    Public Shared Sub Show(ByRef s As String)
        If Form78.uzivatel = "Admin" Then
            MessageBox.Show(s)
            Exit Sub
        End If

        If s.IndexOf("line") < 0 Then
            MessageBox.Show(s)
            Exit Sub
        End If


        Try
            Dim b As Bitmap = New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height, Imaging.PixelFormat.Format32bppArgb)
            Dim gfx As Graphics = Graphics.FromImage(b)
            gfx.CopyFromScreen(My.Computer.Screen.Bounds.X, My.Computer.Screen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy)
            Dim cesta As String = My.Settings.Rotek3
            cesta = cesta & "chyby.jpg"
            b.Save(cesta, Imaging.ImageFormat.Jpeg)

            Send_mail(cesta, s)

            MessageBox.Show("Vyskytla sa chyba. Správa už bola odoslaná a na oprave sa pracuje. Čokoľvek ste chceli spraviť sa nevykonalo. Možno.")
        Catch ex As Exception
            MessageBox.Show("Vyskytla sa chyba. Správa NEBOLA odoslaná a na oprave sa NEpracuje. Čokoľvek ste chceli spraviť sa nevykonalo. Možno.")
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Shared Sub Send_mail(priloha As String, msg As String)
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.Credentials = New System.Net.NetworkCredential("luboscho@gmail.com", "futuRama11")
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.gmail.com"
        SmtpServer.EnableSsl = True

        mail = New MailMessage()
        mail.From = New MailAddress("luboscho@gmail.com")
        mail.To.Add("Lubomir.Vnenk@zoho.com")
        mail.Subject = "POZIADAVKA"
        mail.Body = Form78.uzivatel & vbNewLine & msg

        Dim oAttch As Attachment
        If (priloha.Length > 0) Then
            oAttch = New Attachment(priloha)
            mail.Attachments.Add(oAttch)
        End If


        SmtpServer.Send(mail)

    End Sub


End Class
