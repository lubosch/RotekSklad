'===============================================================================
' Description:      MailAttachment Class
'                   Copyright 2004 by Q

' Created:          2004.8.11, Verion 0.9.0

' Version:          0.9.3
'
' Modification:     ==2004.9.8, Version 0.9.1==
'                   *Add support for 7bit content transfer encoding attachment
'                   *Add ContentID property to support MultipartRelated mail
'                   
'                   ==2004.9.16, Version 0.9.2==
'                   *Add support for quoted-printable content transfer encoding attachment(HTML files in attachments)
'
'                   ==2004.9.24==
'                   *Fixed the bug which will cause exception when attach and save Zero-Length files
'                   
'                   ==2004.12.11, Version 0.9.3==
'                   *Delete the FileName's readonly property in order to enable custom file name
'===============================================================================

Imports System.IO
Imports System.Text
Imports System.web
''' -----------------------------------------------------------------------------
''' Project	 : QMailClient
''' Class	 : MailAttachment
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Represents a mail attachment. This class can not be inherited. 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Tony]	2005-12-1	Created
''' </history>
''' -----------------------------------------------------------------------------
Public NotInheritable Class MailAttachment
    Protected Friend m_FilePath As String
    Protected Friend m_FileName As String
    Protected Friend m_Content As String
    Protected Friend m_TransEncoding As String
    Protected Friend m_ContentID As String      'Only for related attachment such as images

    Public Sub New()
        'DO NOTHING
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Initializes a new instance of the MailAttachment class and specify the attachment file path.
    ''' </summary>
    ''' <param name="FilePath">Attachment file path on the disk</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal FilePath As String)
        Me.FilePath = FilePath
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Initializes a new instance of the MailAttachment class and specify the attachment file path and display file name.
    ''' </summary>
    ''' <param name="FilePath">Attachment file path on the disk</param>
    ''' <param name="FileName">The file name displayed in the mail(For display only).</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal FilePath As String, ByVal FileName As String)
        Me.FilePath = FilePath
        Me.FileName = FileName
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return the content transfer encoding type.
    ''' </summary>
    ''' <value>Encoding type string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property ContentTransferEncoding() As String
        Get
            Return m_TransEncoding
        End Get
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set the attachment file path.
    ''' </summary>
    ''' <value>File path string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property FilePath() As String
        Get
            Return m_FilePath
        End Get
        Set(ByVal Value As String)
            If File.Exists(Value) = False Then
                Throw New Exception("File not found")
            Else
                m_FilePath = Value
                If m_FileName = "" Then
                    Dim i As Int32 = -1
                    Dim j As Int32
                    Do
                        j = i
                        i = m_FilePath.IndexOf("\", i + 1)
                    Loop While i <> -1
                    m_FileName = m_FilePath.Substring(j + 1)
                End If
            End If
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set the attachment display name.
    ''' </summary>
    ''' <value>Display file name</value>
    ''' <remarks>
    ''' It's optional, for display in mail client program only. 
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property FileName() As String
        Get
            Return m_FileName
        End Get
        Set(ByVal Value As String)
            m_FileName = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return the attachment size in bytes.
    ''' </summary>
    ''' <value>An Int32 value</value>
    ''' <remarks>
    ''' The unit is BYTE.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property FileSize() As Int32
        Get
            Return m_Content.Length
        End Get
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set the attachment encoded content.
    ''' </summary>
    ''' <value>Content string</value>
    ''' <remarks>
    ''' Not recommended to use this property directly, unless you want to encode/decode the attachment use your own method.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Content() As String
        Get
            Return m_Content
        End Get
        Set(ByVal Value As String)
            m_Content = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return the attachment content unique ID.
    ''' </summary>
    ''' <value>ID string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property ContentID() As String
        Get
            Return m_ContentID
        End Get
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Generate Base64 content for the attachment file.
    ''' </summary>
    ''' <remarks>
    ''' This method will be called automatically in EMail class
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Sub GenerateBase64ContentFromFile()
        'Notes: 
        '==Base64 encoding standard==
        'It put inputing chars into it's 24-bit(3 bytes) buffer,
        'then split the buffer to 4 6-bit parts,
        'using "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
        'these 64 chars to replace each 6-bit binary value in turn.
        'If buffer is not full, using "=" instead.
        'It generally has 76 chars each line.

        Dim strmFileRead As New FileStream(m_FilePath, FileMode.Open, FileAccess.Read, FileShare.None)

        If strmFileRead.Length = 0 Then
            m_Content = ""
        Else
            'The buffer size must be a multiple of 3 because the Base64 encoding's 24-bit(3 bytes) buffer
            'Large buffer greatly increase performance but also increase memory usage
            Dim buffer(524285) As Byte          'Here using 524286 bytes for buffer(512KB-1B)

            Dim i, j As Int32
            Dim strTemp As New StringBuilder
            Do
                Array.Clear(buffer, 0, buffer.Length)
                i = strmFileRead.Read(buffer, 0, buffer.Length)
                If i < buffer.Length And i <> 0 Then
                    'Drop zero bytes
                    ReDim Preserve buffer(i - 1)
                ElseIf i = 0 Then
                    'File reading is finished
                    strmFileRead.Close()
                    Exit Do
                End If
                m_Content &= Convert.ToBase64String(buffer)
            Loop


            For i = 0 To m_Content.Length \ 76 - 1
                strTemp.Append(m_Content.Substring(76 * i, 76) & vbLf)
            Next
            strTemp.Append(m_Content.Substring(76 * (i)))
            m_Content = strTemp.ToString

            'Release resources
            strTemp = Nothing
            buffer = Nothing
        End If


        'Release resources
        strmFileRead = Nothing
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Save the attchment to a disk file
    ''' </summary>
    ''' <param name="SaveFilePath">Disk file path</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub SaveToFile(ByVal SaveFilePath As String)
        Try
            Dim strmFileWrite As New FileStream(SaveFilePath, FileMode.Create, FileAccess.Write, FileShare.None)
            If m_Content.Replace(vbCrLf, "") <> "" Then 'Ensure the file is not empty
                Dim buffer() As Byte
                Select Case m_TransEncoding.ToLower
                    Case "base64"
                        buffer = Convert.FromBase64String(m_Content.Replace(vbLf, ""))
                    Case "quoted-printable"
                        m_Content = m_Content.Replace("=" & vbCrLf, "")
                        m_Content = m_Content.Replace("=", "%")
                        Dim strCharset As String
                        Dim i As Int32 = m_Content.IndexOf("charset=")
                        If i <> -1 Then
                            strCharset = m_Content.Substring(i + 7, m_Content.IndexOf("""", i) - i - 7)
                        End If
                        If strCharset <> "" Then
                            Dim enc As Encoding = Encoding.GetEncoding(strCharset)
                            buffer = enc.GetBytes(HttpUtility.UrlDecode(m_Content))
                        Else
                            buffer = Encoding.ASCII.GetBytes(HttpUtility.UrlDecode(m_Content))
                        End If
                    Case Else       '7bit or other, may be error in decode
                        buffer = Encoding.ASCII.GetBytes(m_Content)
                End Select
                strmFileWrite.Write(buffer, 0, buffer.Length)
            End If
            strmFileWrite.Close()
        Catch ex As Exception
            Throw New Exception("Error in saving file to disk:" & ex.Message)
        End Try
    End Sub

End Class
