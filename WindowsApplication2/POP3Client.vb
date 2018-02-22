'===============================================================================
' Description:      POP3Client
'                   POP3 Client Class
'                   Based on TCP Socket
'                   Copyright 2004 by Q

' Created:          2004.7.28, Verion 0.9.0

' Version:          0.9.8

' Modification:     ==2004.8.5, Version 0.9.1==
'                   * Add Thread.Sleep statement into SendCommand function 
'                   in order to be compatible with high latency network 
'                   status; this operation is optional.
'                   * Correct mail index count.
'                   * More return error description added.
'
'                   ==2004.8.11, Version 0.9.2==
'                   * Rewrite most part of RetrieveMail methods, increased
'                   compatible ability with different mail servers.
'                   * Add optional property RemoteServerPort(110 for default).
'                   * Make some changes to private Connect method, make it work
'                   properly when retry login because of a first failed login.
'                   
'                   ==2004.8.13, Version 0.9.3==
'                   * GetMailList method now can get more mail header information.
'                   * RetrieveMail method don't call GetMailList to get mail count
'                   at first now.
'                   * Add RetrieveAndDeleteMail method
'                   * Add Reset method
'
'                   ==2004.8.16, Version 0.9.4==
'                   * Rewrite the most part of RetrieveMail method, use MemoryStream
'                   instead of StringBuilder to save data, greatly increased performance.
'
'                   ==2004.8.16==
'                   * Add GC.Collect into RetrieveMail method in order to save memory when
'                   receiving large-size-mail.
'
'                   ==2004.9.7, Version 0.9.5==
'                   * Add Statue property, which only return two value(open or closed)
'
'                   ==2004.10.15, Version 0.9.6==
'                   *Rewrite the SendCommand() method for better performance and compatibility
'                   in some network condition(not for all, still have some problem when Norton 
'                   Antivirus's option, scan incoming mails, is on.)
'
'                   ==2004.11.22, Version 0.9.7==
'                   *Add socket timeout option
'                   *Use UID of a mail to retrieve or delete it instead of using Index
'                   
'                   ==2005.7.8, Version 0.9.8==
'                   *Change the return type of GetUIDList() from StringDictionary to Hashtable 
'                   Because the keys of StringDictionary is case-insensitive which will cause exception
'
'
' Notes:            * After call the Logout method, the socket will dispose;
'                   so, if you want to login again, you MUST declare a new 
'                   instance for use.
'                   * You can Login any times after a failed login. If you do
'                   this after a successful login, a error will occured.
'                   * DO NOT forget to Logout before close your application!
'                   And the Delete method won't take effect before Logout.
'===============================================================================

Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Collections.Specialized
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Data.SqlClient

''' -----------------------------------------------------------------------------
''' Project	 : QMailClient
''' Class	 : POP3Client
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Represents the object which communicate with a POP3 mail server. This class can be inherited. 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Tony]	2005-12-1	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class POP3Client
    Protected sockPOP3 As Socket
    Protected m_Port As Int16 = 110     'Use default POP3 port 110
    Protected m_Remote As IPEndPoint
    Protected m_User As String
    Protected m_Pass As String
    Protected m_arrMailList As New Hashtable
    Protected m_WaitBeforeReceive As Boolean = True

    Protected Const SendBufferLength As Int16 = 1023
    Protected Const ReceiveBufferLength As Int32 = 65535

    Private bufferReceive(ReceiveBufferLength) As Byte
    Private bufferSend(SendBufferLength) As Byte

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set the reomte mail server address.
    ''' </summary>
    ''' <value>IP address or domain name</value>
    ''' <remarks>
    ''' It can be a IP address like "10.0.0.1" or a domain name like "mail.yahoo.com"
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property RemoteServerAddress() As String
        Get
            Return m_Remote.Address.ToString
        End Get
        Set(ByVal Value As String)
            Try
                m_Remote = New IPEndPoint(Dns.Resolve(Value).AddressList(0), m_Port)
            Catch ex As Exception
                Throw ex
            End Try
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set the remote POP3 server's listenning port.
    ''' </summary>
    ''' <value>An Int16 value.</value>
    ''' <remarks>
    ''' It should be 0 ~ 65535. Default for POP3 server is 110
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property RemoteServerPort() As Int16
        Get
            Return m_Port
        End Get
        Set(ByVal Value As Int16)
            m_Port = Value
            If Not m_Remote Is Nothing Then
                m_Remote.Port = m_Port
            End If
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set POP3 mail server login user name
    ''' </summary>
    ''' <value>User name string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property UserName() As String
        Get
            Return m_User
        End Get
        Set(ByVal Value As String)
            m_User = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set POP3 mail server login user password
    ''' </summary>
    ''' <value>Password string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Password() As String
        Get
            Return m_Pass
        End Get
        Set(ByVal Value As String)
            m_Pass = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set whether the working socket should wait for some period of time 
    ''' between sending and receving message.
    ''' </summary>
    ''' <value>True or False</value>
    ''' <remarks>
    ''' Set this property to TRUE when you are behide a firewall, using an anti-virus 
    ''' software which scans incoming emails or there is a high network latency. 
    ''' Otherwise, set this property to FALSE, it will increase performance.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property IncreaseNetworkCompatible() As Boolean
        Get
            Return m_WaitBeforeReceive
        End Get
        Set(ByVal Value As Boolean)
            m_WaitBeforeReceive = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return the working socket's connection state
    ''' </summary>
    ''' <value>ConnectionState enumeration</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property Statue() As ConnectionState
        Get
            If sockPOP3.Connected = True Then
                Return ConnectionState.Open
            Else
                Return ConnectionState.Closed
            End If
        End Get
    End Property

    Public Sub New()
        sockPOP3 = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        sockPOP3.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000) 'Timeout: 60 sec
        sockPOP3.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 1000)    'Timeout: 60 sec
    End Sub

    Public Sub New(ByVal UserName As String, ByVal Password As String)
        Me.New()
        m_User = UserName
        m_Pass = Password
    End Sub

    Public Sub New(ByVal RemoteServerAddress As String, ByVal UserName As String, ByVal Password As String, Optional ByVal RemoteServerPort As Int16 = 110)
        Me.New()
        Try
            m_Remote = New IPEndPoint(Dns.Resolve(RemoteServerAddress).AddressList(0), RemoteServerPort)
        Catch ex As Exception
            Throw New Exception("Invalid server address" & vbCrLf & ex.Message)
        End Try
        m_User = UserName
        m_Pass = Password
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Try loginning the POP3 mail server using the given user name and password.
    ''' </summary>
    ''' <returns>True, login successful; False, login failed.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Login() As Boolean
        Try

            If Connect() = True Then
                If CorrectedResponse(SendCommand("USER " & UserName)) = True Then
                    If CorrectedResponse(SendCommand("PASS " & Password)) = True Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Return Login
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Get a mail list on the POP3 mail server.
    ''' </summary>
    ''' <param name="GetMailHeader">Optional. Indicate whether to get mail header information.</param>
    ''' <returns>
    ''' An ArrayList contains the instances of the EMail class. But these instances have empty mail content. 
    ''' In order to get a certain mail content, use RetrieveMail() method after calling GetMailList() method.
    ''' </returns>
    ''' <remarks>
    ''' The mail list included old and new mails on the mail server. Set GetMailHeader to True is recommended because 
    ''' it will return all useful information about a mail to user, otherwise it will only return mail's unique ID and 
    ''' mail size information.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' 

    Public Function GetNumberOfMails() As Integer
        Dim strList As String
        If CorrectedResponse(SendCommand("LIST", True), strList) = True Then
            Dim strItem() As String = strList.Split(vbCrLf)
            If strItem.Length > 2 Then
                Return strItem.Length - 3                          'Ignore the first and the last two items, they are not items' information
            Else : Return 0
            End If
        Else : Return 0

        End If

    End Function
    Public Function GetMailList(ByVal DataGridView1 As DataGridView, Optional ByVal GetMailHeader As Boolean = False) As Hashtable
        Dim strList As String
        Dim i As Int32
        Dim d As Date = Now
        Dim max_datum As Date
        If DataGridView1.RowCount > 0 Then
            max_datum = DataGridView1.Rows(0).Cells(3).Value
        Else
            max_datum = New Date(1950, 10, 10)
        End If
        Dim prve As Boolean = True



        'Get UID list
        Dim dicUID As Hashtable = GetUIDList(False)

        Dim nick As String
        Dim slovo As String
        Dim kusov As Integer = 0
        Dim datumy As Dictionary(Of String, Date) = New Dictionary(Of String, Date)
        For i = 0 To DataGridView1.RowCount - 1 Step 1
            slovo = DataGridView1.Rows(i).Cells(1).Value
            nick = DataGridView1.Rows(i).Cells(0).Value
            If Not IsDBNull(DataGridView1.Rows(i).Cells(2).Value) Then
                kusov = DataGridView1.Rows(i).Cells(2).Value
            Else
                kusov = 0
            End If
            If Not IsDBNull(DataGridView1.Rows(i).Cells(3).Value) Then
                d = DataGridView1.Rows(i).Cells(3).Value
            Else
                d = New Date(1995, 10, 10)
            End If
            Try

                Dim testAddress = New MailAddress(slovo, nick)
                slovo = testAddress.Address.ToLower
                Dim hes As String = GenerateHash(slovo)
                If m_arrMailList.ContainsKey(hes) Then
                    Dim val As Dictionary(Of String, Integer) = m_arrMailList.Item(hes)
                    Dim jop As Boolean = False
                    For Each s As String In val.Keys
                        If s = slovo Then
                            jop = True
                            Exit For
                        End If
                    Next
                    If jop = False Then
                        val.Add(slovo, kusov)
                        datumy.Add(slovo, d)
                    Else
                        val.Item(slovo) = val.Item(slovo) + 1
                    End If
                Else
                    Dim val As New Dictionary(Of String, Integer)
                    val.Add(slovo, kusov)
                    datumy.Add(slovo, d)
                    m_arrMailList.Add(hes, val)
                End If
                'Debug.WriteLine(slovo & " " & i & "/" & DataGridView1.RowCount)

            Catch ex As FormatException
                'MessageBox.Show(mailInfo.Header)
                'Chyby.Show(ex.ToString)
                ' not a valid email address
            End Try

        Next
        d = Now
        If Not dicUID Is Nothing Then
            If CorrectedResponse(SendCommand("LIST", True), strList) = True Then
                Dim strItem() As String = strList.Split(vbCrLf)
                Dim strSubItem() As String
                If strItem.Length > 2 Then
                    Dim con As New SqlConnection
                    con.ConnectionString = My.Settings.Rotek2
                    con.Open()
                    Dim cmd As New SqlCommand
                    Dim sql As String

                    For i = 1 To strItem.Length - 3 ' To 1 Step -1                         'Ignore the first and the last two items, they are not items' information

                        Dim mailInfo As New EMail
                        strSubItem = strItem(i).Split(" ")
                        mailInfo.m_UniqueID = dicUID.Item(strSubItem(0).Replace(Chr(10), ""))                   'The splict(vbCrLF) will leave a chr(10) before the string
                        mailInfo.m_Size = strSubItem(1)
                        If GetMailHeader = True Then
                            'Get mail header using "TOP" command
                            mailInfo.Header = Encoding.ASCII.GetString(SendCommand("TOP " & i & " 0", True))
                            mailInfo.Header = mailInfo.Header.Substring(mailInfo.Header.IndexOf(vbCrLf) + 2)    'Delete the "+OK" message at the beginning of the received message
                            mailInfo.SplitHeader()

                            'MessageBox.Show(mailInfo.Date)
                            If mailInfo.Date.CompareTo(max_datum) > 0 Then
                                '                               Debug.WriteLine(mailInfo.Date & "  " & i & "/" & strItem.Length)
                                If prve = True Then
                                    prve = False
                                    i = i - 102
                                    If i < 0 Then
                                        i = 0
                                    End If
                                    Continue For
                                End If
                            Else
                                'Debug.WriteLine(mailInfo.Date & " SKIP " & i & "/" & strItem.Length)
                                If i + 100 > strItem.Length - 3 Or prve <> True Then
                                    If prve = True Then
                                        prve = False
                                    End If
                                    Continue For
                                Else
                                    i = i + 100
                                    Continue For
                                End If
                            End If
                            d = mailInfo.Date


                            Try

                                Dim testAddress = Mail.vrat_adresu(mailInfo)
                                slovo = testAddress.Address.ToLower
                                Dim hes As String = GenerateHash(slovo)

                                sql = "Insert INTO Maily (Nick, Mail, Datum, Pocet) VALUES ('" & testAddress.DisplayName.ToLower & "','" & testAddress.Address.ToLower & "','" & mailInfo.Date & "','" & 1 & "' )"

                                If m_arrMailList.ContainsKey(hes) Then

                                    Dim val As Dictionary(Of String, Integer) = m_arrMailList.Item(hes)
                                    Dim jop As Boolean = False
                                    For Each s As String In val.Keys
                                        If s = slovo Then
                                            jop = True
                                            '                                            Debug.WriteLine("Ignore: " & slovo & " " & i & "/" & strItem.Length)
                                            Exit For
                                        End If
                                    Next
                                    If jop = False Then
                                        val.Add(slovo, 1)
                                        datumy.Add(slovo, d)
                                        'Debug.WriteLine("Zapisujem 2 : " & slovo & " " & i & "/" & strItem.Length)
                                    Else
                                        val.Item(slovo) = val.Item(slovo) + 1
                                        If datumy.Item(slovo).CompareTo(d) >= 0 Then
                                            sql = "Update Maily SET Pocet=" & val.Item(slovo) & " WHERE Mail='" & slovo & "'"
                                            'Debug.WriteLine("Updateujem: " & slovo & " NIE:" & datumy.Item(slovo).ToShortDateString & " " & val.Item(slovo) & " " & i & "/" & strItem.Length)
                                        Else
                                            datumy.Item(slovo) = d
                                            sql = "Update Maily SET Pocet=" & val.Item(slovo) & ", Datum='" & mailInfo.Date & "' WHERE Mail='" & slovo & "'"
                                            ' Debug.WriteLine("Updateujem: " & slovo & " " & d.ToShortDateString & " " & val.Item(slovo) & " " & i & "/" & strItem.Length)
                                        End If
                                    End If

                                    Try
                                        cmd = New SqlCommand(sql, con)
                                        cmd.ExecuteNonQuery()
                                    Catch ex As Exception
                                        'Chyby.Show(sql + vbNewLine + ex.ToString)
                                    End Try

                                Else
                                    Dim val As New Dictionary(Of String, Integer)
                                    val.Add(slovo, 1)
                                    datumy.Add(slovo, d)
                                    m_arrMailList.Add(hes, val)

                                    Try
                                        cmd = New SqlCommand(sql, con)
                                        cmd.ExecuteNonQuery()
                                    Catch ex As Exception
                                        'Chyby.Show(sql + vbNewLine + ex.ToString)
                                    End Try
                                    'Debug.WriteLine("Zapisujem: " & slovo & " " & i & "/" & strItem.Length)
                                End If

                            Catch ex As FormatException
                                '                                Chyby.Show(mailInfo.Header)
                                ' not a valid email address
                            End Try
                        End If
                    Next
                    con.Close()


                End If
                Return m_arrMailList

            Else
                Return Nothing
            End If
        Else

            Return Nothing
        End If
    End Function

    Private Function GenerateHash(ByVal SourceText As String) As String
        'Create an encoding object to ensure the encoding standard for the source text
        Dim Ue As New UnicodeEncoding()
        'Retrieve a byte array based on the source text
        Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
        'Instantiate an MD5 Provider object
        Dim Md5 As New MD5CryptoServiceProvider()
        'Compute the hash value from the source
        Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
        'And convert it to String format for return
        Return Convert.ToBase64String(ByteHash)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retrieve a whole mail from POP3 server.
    ''' </summary>
    ''' <param name="UniqueID">The unique ID of the mail required to retrieve</param>
    ''' <returns>An instance of the EMail class</returns>
    ''' <remarks>
    ''' The unique ID could be found in the ArrayList returned by GetMailList() method.
    ''' The GetMailList() returns an ArrayList contains many instances of EMail class and
    ''' the properties UniqueID of these instances are already set by that method. 
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Function RetrieveMail(ByVal UniqueID As String, ByVal timeout As Integer) As EMail
        'Dim dicUID As Hashtable = GetUIDList(True)


        'If Not dicUID Is Nothing Then
        Dim mailReceived As New EMail
        Dim strResult As String
        Dim intPos As Int32
        Dim intCount As Int64 = 0
        Dim intTotal As Int64 = 0

        Dim streamMail As New MemoryStream
        Dim strReceived As String
        Try

            '    bufferSend = Encoding.ASCII.GetBytes("RETR " & dicUID.Item(UniqueID) & vbCrLf)
            bufferSend = Encoding.ASCII.GetBytes("RETR " & UniqueID & vbCrLf)
            sockPOP3.Send(bufferSend)

            If Me.IncreaseNetworkCompatible = True Then                     'Wait a second before receive data
                Thread.Sleep(timeout)
            End If

            Do
                Array.Clear(bufferReceive, 0, bufferReceive.Length)         'Clear buffer

                intCount = sockPOP3.Receive(bufferReceive)                  'Receive data
                streamMail.Write(bufferReceive, 0, intCount)
                intTotal += intCount
                'Check if there is an error
                strReceived = Encoding.ASCII.GetString(bufferReceive)
                If strReceived.ToString.StartsWith("-ERR") Then              'If mail index is out of range, throw an exception
                    Throw New Exception("Mail Not Found")
                End If
                'Show received size
                'RaiseEvent MailReceiving(intTotal)
                intPos = strReceived.ToString.IndexOf(vbCrLf & "." & vbCrLf)
                If intPos <> -1 Then                                        '<CRLF>.<CRLF> indicates the end of a mail
                    Exit Do                                                 'If detected this symbol, exit loop
                End If

            Loop

        Catch ex As Exception
            'Debug.Out.WriteLine(ex.ToString)
            Return (Nothing)
        End Try

        streamMail.Close()

        Dim bufferMail() As Byte = streamMail.GetBuffer
        mailReceived.m_SourceCode = Encoding.ASCII.GetString(bufferMail)

        intPos = mailReceived.SourceCode.IndexOf(vbCrLf & "." & vbCrLf)
        mailReceived.m_SourceCode = mailReceived.m_SourceCode.Substring(0, intPos)                                      'Delete the symbol "." from received message
        mailReceived.m_SourceCode = mailReceived.m_SourceCode.Substring(mailReceived.m_SourceCode.IndexOf(vbCrLf) + 2)    'Delete the "+OK" message at the beginning of the received message

        mailReceived.DecodeMail()
        'Set mail's UID
        mailReceived.m_UniqueID = UniqueID


        'Save resources
        strReceived = Nothing
        bufferMail = Nothing
        GC.Collect()
        Return mailReceived                                              'Return the mail content include the attaches in ASCII string format
        'Else
        'Return Nothing
        'End If

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retrieve a whole mail from POP3 server.
    ''' </summary>
    ''' <param name="UniqueID">The unique ID of the mail required to retrieve</param>
    ''' <param name="ReturnSourceCode">If True, it will return the whole mail source codes; if False, it will return only the codes of the mail body section.</param>
    ''' <returns>Mail source codes</returns>
    ''' <remarks>
    ''' Use this overload version only when you want to decode the mail using your own method.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Function RetrieveMail(ByVal UniqueID As String, ByVal ReturnSourceCode As Boolean) As String
        Dim mailTmp As EMail = RetrieveMail(UniqueID, 100)
        If Not mailTmp Is Nothing Then
            If ReturnSourceCode = True Then
                Return mailTmp.SourceCode
            Else
                Return mailTmp.Body
            End If
        Else
            Return ""
        End If
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Delete a mail from the server permanently.
    ''' </summary>
    ''' <param name="UniqueID">The unique ID of the mail required to delete</param>
    ''' <returns>
    ''' An Int16 value, 1 - Successful; 2 - Failed; -1 - No such mail on server(the mail may be already deleted)
    '''</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function DeleteMail(ByVal UniqueID As String) As Int16
        Dim dicUID As Hashtable = GetUIDList(True)
        If Not dicUID Is Nothing Then
            If dicUID.ContainsKey(UniqueID) = True Then
                If CorrectedResponse(SendCommand("DELE " & dicUID.Item(UniqueID))) = True Then
                    Return 1
                Else
                    Return 0
                End If
            Else
                Return -1
            End If
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retrieve the mail the server and then delete it.
    ''' </summary>
    ''' <param name="UniqueID">The unique ID of the mail required to retrieve and then delete</param>
    ''' <returns>An instance of EMail class if success, else it will return Nothing(null in C#)</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RetrieveAndDeleteMail(ByVal UniqueID As String) As EMail
        Dim mailReceived As EMail = RetrieveMail(UniqueID, 100)
        If mailReceived.SourceCode <> "" Then
            If DeleteMail(UniqueID) = 1 Then
                Return mailReceived
            Else
                Return Nothing
            End If
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Reset the connection state.
    ''' </summary>
    ''' <remarks>
    ''' Not recommended to use this method directly.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub Reset()
        SendCommand("RSET")
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Leave the mail server and close the connection.
    ''' </summary>
    ''' <returns>True, success; False, failed.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Quit() As Boolean
        If CorrectedResponse(SendCommand("QUIT")) = True Then
            sockPOP3.Close()
            Return True
        Else
            Return False
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Close the connection forcedly.
    ''' </summary>
    ''' <remarks>
    ''' Not recommended to use this method directly. This may cause data losing or other 
    ''' problems.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub CloseForcedly()
        sockPOP3.Close()
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Send a POP3 command to the server
    ''' </summary>
    ''' <param name="Command">Command name</param>
    ''' <param name="ReceiveLongData">Optional. If the command will cause the server to return a large text response, set this to True.</param>
    ''' <returns>Server's response text in bytes array</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function SendCommand(ByVal Command As String, Optional ByVal ReceiveLongData As Boolean = False) As Byte()
        Try
            bufferSend = Encoding.ASCII.GetBytes(Command & vbCrLf)
            sockPOP3.Send(bufferSend)


            'This command is used to be compatible with high latency network
            If m_WaitBeforeReceive = True Then
                Thread.Sleep(100)
            End If

            If ReceiveLongData = False Then                                     'Data without end symbol "."
                Array.Clear(bufferReceive, 0, bufferReceive.Length)
                sockPOP3.Receive(bufferReceive)
                Return bufferReceive
            ElseIf ReceiveLongData = True Then
                Dim intCount As Int32
                Dim streamMail As New MemoryStream
                Dim strReceived As String
                Do
                    Array.Clear(bufferReceive, 0, bufferReceive.Length)         'Clear buffer

                    intCount = sockPOP3.Receive(bufferReceive)                  'Receive data
                    streamMail.Write(bufferReceive, 0, intCount)
                    strReceived = Encoding.ASCII.GetString(bufferReceive)

                    Dim intPos As Int32 = strReceived.ToString.IndexOf(vbCrLf & "." & vbCrLf)
                    If intPos <> -1 Then                                        '"." indicates the end of server response
                        Exit Do                                                 'If detected this symbol, exit loop
                    End If
                Loop
                streamMail.Close()
                Return streamMail.GetBuffer
            End If

        Catch ex As Exception
            Return SendCommand(Command, ReceiveLongData)
            Throw ex
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Check the response of the server.
    ''' </summary>
    ''' <param name="ReceivedBytes">Server's response, it is in bytes array format.</param>
    ''' <param name="Message">Optional. The message of the server's response.</param>
    ''' <returns>True, command execute successful; False, command execute failed.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function CorrectedResponse(ByVal ReceivedBytes() As Byte, Optional ByRef Message As String = "") As Boolean
        Dim strText As String = Encoding.ASCII.GetString(ReceivedBytes)
        Message = strText
        'RaiseEvent GotResponse(strText)
        If strText.StartsWith("+OK") Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Make the connection to the mail server.
    ''' </summary>
    ''' <returns>True, connect successful; False, connect failed.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function Connect() As Boolean
        Try
            If sockPOP3.Connected = True Then       'If first Login failed, the second retry Login do not need connect again!
                Return True
            Else
                sockPOP3.Connect(m_Remote)
            End If

            If sockPOP3.Connected = True Then
                sockPOP3.Receive(bufferReceive)
                'RaiseEvent GotResponse(Encoding.ASCII.GetString(bufferReceive))
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            sockPOP3.Close()
            Throw ex
        End Try
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Get mail's unique ID list
    ''' </summary>
    ''' <param name="UIDAsKey">Indicate whether to use the UID as the key in the return hashtable</param>
    ''' <returns>A hashtable contains a UID list</returns>
    ''' <remarks>
    ''' If UIDAsKey is True, the UID is the key of the hashtable, otherwise the mail index will be the key.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function GetUIDList(ByVal UIDAsKey As Boolean) As Hashtable
        Dim UIDL As Hashtable

        Dim strList As String
        If CorrectedResponse(SendCommand("UIDL", True), strList) = True Then
            UIDL = New Hashtable
            Dim strItem() As String = strList.Split(vbCrLf)
            Dim strSubItem() As String
            If strItem.Length > 2 Then
                For i As Int32 = 1 To strItem.Length - 3
                    strSubItem = strItem(i).Split(" ")
                    If UIDAsKey = True Then
                        UIDL.Add(strSubItem(1), strSubItem(0).Replace(Chr(10), ""))         'Set UID as key, Index as value
                    Else
                        UIDL.Add(strSubItem(0).Replace(Chr(10), ""), strSubItem(1))         'Set Index as key, UID as value
                    End If
                Next
            End If
        End If
        Return UIDL
    End Function
End Class
