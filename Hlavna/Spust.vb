Imports System.Net
Imports System.IO
Imports Microsoft.Win32
Imports System.Net.Mail
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Reflection
Imports System.Web
Imports Microsoft.VisualBasic.Devices
Imports System.Globalization

Public Class Spust
    Public Shared spravy As List(Of Sprava)
    Public Shared ds As DataTable
    Public Shared dv As DataView

    Public Event mail_pribudol()




    Public Sub New()



        '  AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf test

        ' This call is required by the designer.
        InitializeComponent()

        Dim customCulture As System.Globalization.CultureInfo = DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture.Clone(), System.Globalization.CultureInfo)
        customCulture.NumberFormat.NumberDecimalSeparator = "."

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture

        Form78.mail = ""
        Form78.mail_heslo = ""

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Spust_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.SkladList' table. You can move, or remove it, as needed.
        Me.SkladListTableAdapter.Fill(Me.RotekDataSet.SkladList)
        'TODO: This line of code loads data into the 'RotekDataSet.Maily' table. You can move, or remove it, as needed.

        aktualizacie()
        '        zoznam_mailov()
        Try

            'Dim Path As String = Application.LocalUserAppDataPath + "\hub.dat"
            My.Settings.Rotek3 = "\\192.168.1.150\Sklad\Sklad\"
            '"\\192.168.1.131\public\Sklad\"
            'My.Settings.Rotek3 = Application.StartupPath + "\"

            My.Settings.Rotek2 = My.Settings.Rotek

            If System.IO.Directory.Exists(My.Settings.Rotek3) Then
            ElseIf System.IO.Directory.Exists(My.Settings.Rotek3) = False Then
                Chyby.Show("Nie ste pripojený k sieti. Program sa nezapne")
                Process.Start("explorer.exe", "\\192.168.1.150\Sklad")
                'System.Environment.Exit(0)
            Else
                Chyby.Show("Databáza sa nenašla. Požiadajte o zálohu. Program sa nezapne.")
                Me.Dispose()
                Exit Sub
            End If

            'Try
            '    My.Computer.FileSystem.CopyFile(My.Settings.Rotek3 & "hub.dat", Path, True)
            'Catch ex As Exception
            '    Chyby.Show(ex.ToString)
            'End Try
            Me.UcetTableAdapter.Fill(Me.RotekDataSet.Ucet)

            Dim objReader As System.IO.StreamReader
            Dim objWriter As System.IO.StreamWriter
            Form78.admin = "dfsgweg54456reg8|DS\'32r45"
            Form78.zakazkar = "hjy5RYbhjsk734u9213:"
            Form78.skladnik = "bVhjsb432768y8jnpds:']"

            'Form78.subor = New List(Of String)
            'While (objReader.EndOfStream) = False
            '    'MessageBox.Show(Replace(SimpleCrypt(objReader.ReadLine()).Trim, vbNewLine, vbNullChar))

            '    Form78.subor.Add(Replace(SimpleCrypt(objReader.ReadLine()).Trim, vbNewLine, vbNullChar))
            '    '                Chyby.Show(Form78.subor(Form78.subor.Count - 1))

            'End While
            'objReader.Close()
            'objReader = New System.IO.StreamReader(My.Settings.Rotek3 & "last.dat", System.Text.Encoding.Default)
            'Dim dateString As String = objReader.ReadLine().Replace(vbCr, "").Replace(vbLf, "")
            'Dim datum As DateTime = DateTime.ParseExact(dateString, "dd.m.yyyy", CultureInfo.InvariantCulture)
            'objReader.Close()
            'objReader.Dispose()
            'datum = datum.AddMonths(1)
            '' MessageBox.Show(datum.ToShortDateString)
            'If datum.CompareTo(Now) <= 0 Then
            '    objWriter = New System.IO.StreamWriter(My.Settings.Rotek3 & "last.dat", False)
            '    objWriter.Write(Now.ToShortDateString)
            '    objWriter.Dispose()
            '    posli_mail()
            'End If


            Me.MailyTableAdapter.Fill(Me.RotekDataSet.Maily)
            MailyBindingSource.Sort = "Datum DESC"

            'Dim thrd As Thread = New Thread(AddressOf nacitak_maily)
            'thrd.IsBackground = True
            'thrd.Start()

            Dim f As New Heslo
            f.TopLevel = True
            f.Dock = DockStyle.None
            f.ShowDialog()
            f.Dispose()

            'If Form78.mail.Length > 0 Then
            '    Dim thrd2 As Thread = New Thread(Sub() Mail_klient(Form78.mail, Form78.mail_heslo))
            '    thrd2.IsBackground = True
            '    thrd2.Start()
            'End If

        Catch ex As Exception
            'My.Computer.FileSystem.CopyFile(My.Settings.Rotek3 + "hub.dat", Path, True)
            'Dim objReader As New System.IO.StreamReader(Path, System.Text.Encoding.Default)
            'Form78.admin = SimpleCrypt(objReader.ReadLine())
            'Form78.zakazkar = SimpleCrypt(objReader.ReadLine())
            'Form78.skladnik = SimpleCrypt(objReader.ReadLine())
            'objReader.Dispose()
            Chyby.Show(ex.ToString)
        End Try

        'Chyby.Show(admin)
        ' MsgBox(admin + " " + zakazkar + " " + skladnik)

        poverenia()
        rozmers()
    End Sub


    'Private Function test( _
    '        ByVal sender As Object, _
    '        ByVal args As System.ResolveEventArgs) As System.Reflection.Assembly

    '    Dim resourceName As String = "AssemblyLoadingAndReflection." & _
    '        New AssemblyName("EPPlus").Name & ".dll"

    '    Using stream = System.Reflection.Assembly.GetExecutingAssembly() _
    '        .GetManifestResourceStream(resourceName)

    '        Dim assemblyData(CInt(stream.Length - 1)) As Byte

    '        stream.Read(assemblyData, 0, assemblyData.Length)

    '        Return System.Reflection.Assembly.Load(assemblyData)

    '    End Using
    'End Function

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New Form78
        Me.Hide()
        f.ShowDialog()
        f.Dispose()
        Me.Show()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim f As New Z_Main
        Me.Hide()
        f.ShowDialog()
        f.Dispose()

        Me.Show()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f As New hForm78
        f.sklad_list_id = ListBox1.SelectedValue
        Me.Hide()
        f.ShowDialog()
        f.Dispose()
        Me.Show()


    End Sub
    Private Sub poverenia()
        Select Case Form78.heslo
            Case Form78.admin
                Label2.Text = Form78.uzivatel
                Button5.Show()
                Button4.Show()
            Case Form78.skladnik
                Button5.Hide()
                Button4.Hide()
                Label2.Text = Form78.uzivatel
            Case Form78.zakazkar
                Button4.Hide()
                Button5.Hide()
                Label2.Text = Form78.uzivatel
            Case Else
                Button5.Hide()
                Button4.Hide()
                Label2.Text = "Neznámy užívateľ"
        End Select
        Label2.Text = Label2.Text + ". Systém k Vaším službám"

    End Sub
    Private Sub zoznam_mailov()
        Dim serverTcpConnection As TcpClient
        Dim popserver As String = "mail.rotek.sk"
        Dim port As Integer = 995
        Try
            serverTcpConnection = New TcpClient(popserver, port)
        Catch ex As Exception
            Chyby.Show("Pripojenie zlyhalo k popserveru")
            Exit Sub
        End Try

        Dim pop3stream As Stream
        pop3stream = serverTcpConnection.GetStream
        pop3stream.ReadTimeout = 5000

        Dim pop3streamReader As StreamReader
        pop3streamReader = New StreamReader(pop3stream, Encoding.ASCII)
        Dim response As String = Nothing
        response = pop3streamReader.ReadLine
        If response = Nothing Then
            Chyby.Show("Timeout")
        End If
        response = "+"


    End Sub

    Private Sub nacitak_maily()
        Try
            'MessageBox.Show(DataGridView1.Rows(0).Cells(0).Value)
            'Exit Sub


            Dim m As POP3Client = New POP3Client()
            m.RemoteServerAddress = "mail.rotek.sk"
            m.UserName = "rotek@rotek.sk"
            m.Password = "mb31icd"
            m.IncreaseNetworkCompatible = True
            If m.Login = False Then
                'MessageBox.Show("Login failed")
                Exit Sub
            End If
            'Exit Sub

            Dim arrlst As Hashtable = New Hashtable
            arrlst.Clear()
            arrlst = m.GetMailList(DataGridView1, True)
            m.Quit()


            'Dim con As New SqlConnection
            'con.ConnectionString = My.Settings.Rotek2       
            'con.Open()
            'Dim cmd As New SqlCommand



            'Dim nick, adresa, sql As String
            'For Each s As String In arrlst.Values
            '    nick = s.Substring(0, s.IndexOf("~|~"))
            '    adresa = s.Substring(s.IndexOf("~|~") + 3)
            '    '                sql = "Insert INTO Rotek (Meno, Priezvisko, Menpr, pocet, Spolu) VALUES ('" + meno + "', '" + priez + "', '" + mp + "','" & nula & "',0)"
            '    sql = "Insert INTO Maily (Nick, Mail) VALUES ('" & nick & "','" & adresa & "')"
            '    cmd = New SqlCommand(sql, con)
            '    Try
            '        cmd.ExecuteNonQuery()
            '    Catch ex As Exception
            '        'Chyby.Show(sql + vbNewLine + ex.ToString)
            '    End Try


            '    'MessageBox.Show(nick & " : " & adresa)
            'Next
            'con.Close()

        Catch ex As Exception
            'Chyby.Show(ex.ToString)
        End Try
        '  MessageBox.Show("Skoncene")

    End Sub

    Private Sub posli_mail()
        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New System.Net.NetworkCredential("luboscho@gmail.com", "futuRama11")
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.gmail.com"
            SmtpServer.EnableSsl = True
            mail = New MailMessage()
            mail.From = New MailAddress("luboscho@gmail.com")
            mail.To.Add("Rotek@rotek.sk")
            mail.To.Add("tekel@rotek.sk")
            mail.To.Add("jurco@rotek.sk")
            mail.Subject = "ZÁLOHA"
            mail.Body = "Treba všetko zálohovať"
            SmtpServer.Send(mail)

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub rozmers()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(Label1.Text + Label2.Text, Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred - 20
        Label1.Location = New System.Drawing.Point(rw, 96)
        Label2.Location = New System.Drawing.Point(rw + 50, 96)
    End Sub

    Private Function strom(ByRef servers() As String, ByRef zalohy As List(Of String)) As Boolean
        Try
            'Dim ftpWebReq As System.Net.FtpWebRequest = CType(System.Net.WebRequest.Create("ftp://vnenk.orava.sk/Rotek"), System.Net.FtpWebRequest)
            'ftpWebReq.Method = System.Net.WebRequestMethods.Ftp.ListDirectory

            'ftpWebReq.Credentials = New System.Net.NetworkCredential("vnenk", "ptkw452d")

            'Dim ftpWebResp As System.Net.FtpWebResponse = CType(ftpWebReq.GetResponse(), System.Net.FtpWebResponse)

            'Dim streamer As IO.Stream = ftpWebResp.GetResponseStream()

            'Dim reader As New IO.StreamReader(streamer)

            ''Chyby.Show(reader.ReadToEnd(), "FTP")
            'Dim i As Integer = 0
            'While (reader.EndOfStream <> True)
            '    servers(i) = (reader.ReadLine().ToString)
            '    i = i + 1
            'End While

            'ftpWebReq = CType(System.Net.WebRequest.Create("ftp://vnenk.orava.sk/Databaza"), System.Net.FtpWebRequest)

            'ftpWebReq.Method = System.Net.WebRequestMethods.Ftp.ListDirectory

            'ftpWebReq.Credentials = New System.Net.NetworkCredential("vnenk", "ptkw452d")

            'ftpWebResp = CType(ftpWebReq.GetResponse(), System.Net.FtpWebResponse)

            'streamer = ftpWebResp.GetResponseStream()

            'reader = New IO.StreamReader(streamer)

            ''Chyby.Show(reader.ReadToEnd(), "FTP")
            'i = 0
            'While (reader.EndOfStream <> True)
            '    zalohy.Add(reader.ReadLine().ToString)
            '    i = i + 1
            'End While
            Dim i As Integer = 0
            For Each filePath As String In System.IO.Directory.EnumerateFiles("\\192.168.1.150\Sklad\Updates\")
                servers(i) = filePath.Substring(filePath.LastIndexOf("\") + 1)
                i += 1
            Next

        Catch ex As Exception
            Chyby.Show("Nedá sa pripojiť k internetu")
            Return False
        End Try
        Return True
    End Function

    Private Sub Upload(ByVal source As String, ByVal target As String, ByVal credential As NetworkCredential)
        Try

            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(target), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.UploadFile
            request.Credentials = credential
            Dim reader As New FileStream(source, FileMode.Open)
            Dim buffer(Convert.ToInt32(reader.Length - 1)) As Byte
            reader.Read(buffer, 0, buffer.Length)
            reader.Close()
            request.ContentLength = buffer.Length
            Dim stream As Stream = request.GetRequestStream
            stream.Write(buffer, 0, buffer.Length)
            stream.Close()
            Dim response As FtpWebResponse = DirectCast(request.GetResponse, FtpWebResponse)
            'Chyby.Show(response.StatusDescription, "File Uploaded")
            response.Close()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub aktualizacie()

        Try
            'MessageBox.Show(Application.LocalUserAppDataPath & "\Update\")
            Dim servers(1000) As String
            Dim zalohy As List(Of String) = New List(Of String)
            If (strom(servers, zalohy)) Then
            Else
                Exit Sub
            End If
            'Chyby.Show("a")
            Dim daty(zalohy.Count) As DateTime
            Dim i As Integer = 0
            'Dim d1, d2 As DateTime
            'd1 = New Date(2099, 12, 24)
            'd2 = New Date(2000, 1, 1)
            'MessageBox.Show("a")

            'Try
            '    For i = 0 To zalohy.Count - 1
            '        Try
            '            daty(i) = zalohy(i)

            '            If daty(i).CompareTo(d2) > 0 Then
            '                d2 = daty(i)
            '            End If
            '            If daty(i).CompareTo(d1) < 0 Then
            '                d1 = daty(i)
            '            End If
            '        Catch ex As Exception
            '            daty(i) = New Date(1990, 1, 1)
            '        End Try


            '    Next
            '    'MessageBox.Show("a")
            '    If zalohy.Count > 20 Then
            '        Dim cesta2 As String = "ftp://vnenk.orava.sk/Databaza/" + d1
            '        Dim FTPDelReq As FtpWebRequest = WebRequest.Create(cesta2)
            '        FTPDelReq.Credentials = New System.Net.NetworkCredential("vnenk", "ptkw452d")
            '        FTPDelReq.Method = WebRequestMethods.Ftp.DeleteFile
            '        Dim FTPDelResp As FtpWebResponse = FTPDelReq.GetResponse
            '    End If
            '    'MessageBox.Show("a")

            '    d2 = d2.AddDays(3)

            '    If d2.CompareTo(Now) < 1 Then
            '        Dim credential As New NetworkCredential("vnenk", "ptkw452d")
            '        Dim cesta As String = My.Settings.Rotek3 + "Rotek.mdb"
            '        Dim cesta2 As String = "ftp://vnenk.orava.sk/Databaza/" + Date.Now.ToShortDateString
            '        Upload(cesta, cesta2, credential)
            '    End If
            '    'MessageBox.Show("a")

            'Catch ex As Exception

            '    'Chyby.Show("Problem so zalohovanim databazy. Tuto spravu kludne ignorujte. " & ex.ToString)
            'End Try
            'Chyby.Show("b")

            Dim localFiles(1000) As String
            localFiles(0) = Application.LocalUserAppDataPath & "\Update\"
            'MessageBox.Show(localFiles(0))
            ' me.Dispose
            '            localFiles(0) = Replace(localFiles(0), "Rotek sklad.EXE", "")
            '           localFiles(0) = localFiles(0) & "Update\"

            If My.Computer.FileSystem.DirectoryExists(localFiles(0)) Then
            Else
                My.Computer.FileSystem.CreateDirectory(localFiles(0))
            End If
            ' make a reference to a directory
            Dim di As New IO.DirectoryInfo(localFiles(0))
            Dim diar1 As IO.FileInfo() = di.GetFiles()
            Dim dra As IO.FileInfo

            'list the names of all files in the specified directory
            i = 0
            'Chyby.Show("c")

            For Each dra In diar1
                i = i + 1
                'If dra.ToString = "Rotek.mdb" Then
                '    My.Computer.FileSystem.CopyFile(Application.StartupPath & "\Update\Rotek.mdb", Application.StartupPath & "\Rotek.mdb", True)
                '    My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Update\Rotek.mdb")
                'End If
                'If dra.ToString = "Rotek sklad.exe.config" Then
                '    My.Computer.FileSystem.CopyFile(Application.StartupPath & "\Update\Rotek sklad.exe.config", Application.StartupPath & "\Rotek sklad.exe.config", True)
                '    My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Update\Rotek sklad.exe.config")
                'End If

                localFiles(i) = dra.ToString
            Next


            Dim DownloadingFiles As IEnumerable(Of String) = servers.Except(localFiles.AsEnumerable)

            For Each FileN As String In DownloadingFiles
                'Chyby.Show("d")

                ' ObjShell.exec("Rotek sklad.exe")
                'ObjShell = Nothing

                'Chyby.Show("Začína " & FileN & " Netreba nič robiť len po chvíli spustiť z plochy")
                System.IO.File.Copy("\\192.168.1.150\Sklad\Updates\" + FileN, localFiles(0) & FileN)

                'My.Computer.Network.DownloadFile("http://vnenk.orava.sk/rotek/" & FileN, localFiles(0) & FileN)


                'Dim strcomputer As String
                'strcomputer = "."
                'Dim objwmiservice, colsoftware
                'objwmiservice = GetObject("winmgmts:" _
                '    & "{impersonationLevel=impersonate}!\\" & strcomputer & "\root\cimv2")

                'colsoftware = objwmiservice.ExecQuery _
                '    ("Select * from Win32_Product Where Name = 'Rotek sklad'")
                'Try
                '    If colsoftware.count < 1 Then
                '        unins()

                '    End If
                '    For Each objSoftware In colsoftware
                '        objSoftware.Uninstall()
                '    Next

                'Catch ex As Exception
                'uninst()


                'odtialto

                If Directory.Exists(localFiles(0) + "temp_install") Then
                    Directory.Delete(localFiles(0) + "temp_install", True)
                End If
                Directory.CreateDirectory(localFiles(0) + "temp_install")
                Dim proc As New Process()

                Dim cmd As String = "/a /s /v""/qb TARGETDIR=\""" + localFiles(0) & "temp_install\"""""
                'MessageBox.Show(cmd)
                proc.StartInfo.FileName = localFiles(0) + FileN
                proc.StartInfo.Arguments = cmd
                proc.Start()
                proc.WaitForInputIdle()
                proc.WaitForExit()
                proc.Close()



                System.IO.File.Delete(Path.GetTempPath() & "Uninstall.bat")
                System.IO.File.WriteAllLines(Path.GetTempPath() & "Uninstall.bat", New String() {"msiexec.exe /x ""{DDD63E0A-414F-480B-BAE0-A4EDB0AC80FE}"" /passive " & vbLf, "msiexec.exe /i """ + localFiles(0) & "temp_install\Rotek IS.msi"" /passive", "start """" """ & Application.ExecutablePath & """"})


                proc = New Process()
                proc.StartInfo.FileName = Path.GetTempPath() & "Uninstall.bat"
                proc.Start()
                Environment.[Exit](0)

                'potialto






                'End Try
                'Process.Start("c:\windows\system32\msiexec.exe", "/qn /i " & """" & Application.StartupPath & "\Update\" & FileN & """")

                'System.Diagnostics.Process.Start(Application.LocalUserAppDataPath & "\Update\" & FileN)
                End
            Next

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub uninst()
        Try
            Const ProductCode = "{DDD63E0A-414F-480B-BAE0-A4EDB0AC80FE}"
            'Const ProductCode = "{F4231357-9D7D-4B88-BA9D-A290D9892A2E}"
            Const msiInstallStateAbsent = 2

            Dim oInstaller = CreateObject("WindowsInstaller.Installer")
            oInstaller.UILevel = 99
            oInstaller.ConfigureProduct(ProductCode, 0, msiInstallStateAbsent)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub unins()
        Try
            Dim MyAppStr As String = "Rotek sklad"

            Dim RegistryKeyObj As RegistryKey
            Dim CurrentRegKeyObj As RegistryKey
            Dim IsMyAppBool As Boolean = False

            ' MessageBox.Show(RegistryKeyObj.GetSubKeyNames.ToString)

            If (Environment.Is64BitOperatingSystem = True) Then
                RegistryKeyObj = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\MICROSOFT\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Products", True)
            Else
                RegistryKeyObj = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\MICROSOFT\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Products", True)
            End If


            For Each ApplicationObj As String In RegistryKeyObj.GetSubKeyNames
                CurrentRegKeyObj = RegistryKeyObj.OpenSubKey(ApplicationObj).OpenSubKey("InstallProperties")
                If Not CurrentRegKeyObj Is Nothing Then
                    If String.Compare(CurrentRegKeyObj.GetValue("DisplayName").ToString, MyAppStr) = 0 Then
                        Dim zmaz As String = "SOFTWARE\MICROSOFT\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Products\" & ApplicationObj
                        RegistryKeyObj.DeleteSubKeyTree(ApplicationObj)
                        Exit For
                    End If
                End If
            Next

            'HKEY_CLASSES_ROOT\Installer\Products\A40E4B7A620A49E46824A8661BB31EAC

            RegistryKeyObj = Registry.ClassesRoot.OpenSubKey("Installer\Products", True)

            If (Environment.Is64BitOperatingSystem = True) Then
                RegistryKeyObj = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64).OpenSubKey("Installer\Products", True)
            Else
                RegistryKeyObj = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32).OpenSubKey("Installer\Products", True)
            End If


            '            RegistryKeyObj = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32).OpenSubKey("Installer\Products", True)

            For Each ApplicationObj As String In RegistryKeyObj.GetSubKeyNames
                CurrentRegKeyObj = RegistryKeyObj.OpenSubKey(ApplicationObj).OpenSubKey("InstallProperties")

                If Not RegistryKeyObj.OpenSubKey(ApplicationObj).GetValue("ProductName") Is Nothing Then
                    Try
                        If String.Compare(RegistryKeyObj.OpenSubKey(ApplicationObj).GetValue("ProductName"), MyAppStr) = 0 Then
                            Dim zmaz As String = ApplicationObj
                            '              MessageBox.Show(ApplicationObj)
                            RegistryKeyObj.DeleteSubKeyTree(ApplicationObj)
                            IsMyAppBool = True
                            Exit For
                        End If
                    Catch ex As Exception
                    End Try
                End If
            Next

            If IsMyAppBool = False Then
                Chyby.Show("Application : " & MyAppStr & " had not installed in the system.")
            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click
        Dim f As New Heslo
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
        poverenia()
        rozmers()

    End Sub

    Private Sub Button17_Click(sender As System.Object, e As System.EventArgs) Handles Button17.Click
        Dim nacitava As Boolean = False
        If Form78.mail.Length > 0 Then
            nacitava = True
        End If
        Dim f As New Heslozmen
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
        If nacitava = False AndAlso Form78.mail.Length > 0 Then
            Dim thrd2 As Thread = New Thread(Sub() Mail_klient(Form78.mail, Form78.mail_heslo))
            thrd2.IsBackground = True
            thrd2.Start()
        End If

    End Sub
    'Public Shared Function SimpleCrypt(ByVal Text As String) As String
    '    ' Encrypts/decrypts the passed string using
    '    ' a simple ASCII value-swapping algorithm
    '    Dim strTempChar As String, i As Integer
    '    For i = 1 To Len(Text)
    '        If Asc(Mid$(Text, i, 1)) < 128 Then
    '            strTempChar = CType(Asc(Mid$(Text, i, 1)) + 128, String)
    '        ElseIf Asc(Mid$(Text, i, 1)) > 128 Then
    '            strTempChar = CType(Asc(Mid$(Text, i, 1)) - 128, String)
    '        Else
    '            Chyby.Show("Zle napisane heslo")
    '            Throw New Exception
    '        End If
    '        Mid$(Text, i, 1) = _
    '            Chr(CType(strTempChar, Integer))
    '    Next i
    '    Return Text
    'End Function

    Private Sub PictureBox2_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox2.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f As New Povolenia()
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub Mail_klient(ByVal meno As String, ByVal heslo As String)
        Try

            spravy = New List(Of Sprava)
            ds = New DataTable()
            ds.TableName = "0"
            With ds
                .Columns.Add("From")
                .Columns.Add("Subject")
                .Columns.Add("Priloha")
                .Columns.Add("Datum_format")
                .Columns.Add("Datum", GetType(Date))
                .Columns.Add("Sprava", GetType(Sprava))
            End With

            dv = ds.DefaultView
            'dv.Sort = "Datum DESC"

            Dim pop As POP3Client = New POP3Client()
            Dim mess As EMail
            Dim j As Integer = 100
            Dim i As Integer = 1
            Dim m As POP3Client = New POP3Client()
            m.RemoteServerAddress = "mail.rotek.sk"
            m.UserName = meno
            m.Password = heslo
            m.IncreaseNetworkCompatible = True
            If m.Login = False Then
                'Chyby.Show("Login failed")
                Exit Sub
            End If

            i = m.GetNumberOfMails
            Dim slovo As String
            While (i > 0)

                j = 100

                mess = Nothing

                While (mess Is Nothing)
                    Try
                        mess = m.RetrieveMail(i.ToString, j)
                    Catch ex As Exception
                        i = i - 1
                        Continue While
                    End Try
                    j = j + 500

                End While

                Dim hlava As String = mess.Header
                Dim nick As String = ""
                slovo = hlava

                Dim msg As Sprava = New Sprava(Mail.vrat_adresu(mess), mess.Subject, mess.Date, mess.Attachments, mess.Body, mess.BodyHTML)
                spravy.Add(msg)

                Dim datum As String = ""
                '            MessageBox.Show("|" & mess.Date.ToShortDateString & "|" & New Date().ToShortDateString & "|")
                'MessageBox.Show(Now.Date().AddMonths(-2).ToShortDateString)
                If mess.Date.ToShortDateString.CompareTo(Now.Date().ToShortDateString) = 0 Then
                    datum = mess.Date.ToShortTimeString
                ElseIf mess.Date.CompareTo(Now.Date().AddMonths(-2)) < 1 Then
                    i = i - 1
                    Continue While
                Else
                    datum = mess.Date.ToShortDateString
                End If

                If mess.Attachments.Count = 0 Then
                    ds.Rows.Add({mess.From, mess.Subject, "", datum, mess.Date, msg})
                Else
                    ds.Rows.Add({mess.From, mess.Subject, "#", datum, mess.Date, msg})
                End If

                RaiseEvent mail_pribudol()
                Dim proc As Process = Process.GetCurrentProcess()
                Dim asas As New ComputerInfo()

                '            Debug.Out.WriteLine(proc.PrivateMemorySize64 / asas.AvailablePhysicalMemory & " " & proc.PrivateMemorySize64 & proc.PrivateMemorySize64 > 1000000000)
                If (proc.PrivateMemorySize64 / asas.AvailablePhysicalMemory > 0.9 OrElse proc.PrivateMemorySize64 > 1000000000) Then
                    Exit Sub

                End If

                '            DataRepeater1.Invoke(Sub() zmen())
                i = i - 1

            End While

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim f As New Heslozmen(5)
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim slovo As String = InputBox("V tomto programe mi ešte chýba", "Požiadavka")
        If slovo.Length > 0 Then
            Chyby.Send_mail("", slovo)
        End If

    End Sub
End Class