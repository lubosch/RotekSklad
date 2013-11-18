'===============================================================================
' Description:      EMail Class
'                   Copyright 2004-2005 by Q

' Created:          2004.8.11, Verion 0.9.0

' Version:          0.9.22
'                   *All features except HTML mail support has been finished
'                   on 2004.8.24
'
' Modification:     ==2004.9.2, Version 0.9.1==
'                   *Change class name from Mail to EMail
'                   *Add multipart/alternative support 
'                   *Add support for quoted-printable content transfer encoding(HTML mail)
'
'                   ==2004.9.6, Version 0.9.2==
'                   *Fix the bug that the mail with attachments can't be recognized in Foxmail and
'                   outlook express. Because there must be only one <CRLF> between boundary and "content-type",
'                   but this program write two. Since none empty lines are NOT allowed between boudnary and "content-type",
'                   the CombineBody() method is modified.
'                   *Fix another bug in mail body decode functions, which is also caused by two <CRLF>
'                   *Add MessageID property
'                   *Change member variant from "protected" to "protected friend"
'                   *Add SaveToFile() method
'                   *Add ReadFromFile() method
'
'                   ==2004.9.6, Version 0.9.3==
'                   *Modify the SplitBody() method, this method now has a self call(recursion) in order to decode mails which
'                   have sub content type(e.g. Multi/Alternative content in Multi/Mixed content with attachments as a sub content)
'                   *Add a sub class MailAttachmentCollection
'
'                   ==2004.9.8==
'                   *Modify the SplitBody() method, add support for non-English-char file name of attachments.
'   
'                   ==2004.9.8, Version 0.9.4==
'                   *Fix two bugs in SplitBody() and Decode() methods, which will cause errors when process some kind of sub content 
'                   type mails and mails with no body text.
'                   *Add support for content type application/x-compress mail which has no body text                   
'
'                   ==2004.9.10, Version 0.9.5==
'                   *Add support for multilines Subject, From, To fields                   
'
'                   ==2004.9.14==
'                   *Add an optional parameter OnlyDecodeMailHeaderInformation to ReadFromFile() method,
'                   this parameter is used to increase performance when there is only mail information 
'                   needed(When it is True, the mail body will not be decoded).
'
'                   ==2004.9.15, Version 0.9.6==
'                   *Add 3 new content type(multipart/report,message/rfc822,message/deliverystatus)
'                   in order to support decoding mail failure report from mail servers.
'
'                   ==2004.9.17, Version 0.9.7==
'                   *Modify the GetValueString() method to support non-case-care search but increased memory usage
'                   *Add code to ignore attachment with non-filename
'                   *Fix bugs in GetDateTimeFromString() method to support time zone with "+","-" and symbol (CST etc.)
'                                   
'                   ==2004.9.20, Version 0.9.8==
'                   *Fixed some bugs in processing message/report content-type-mail
'
'                   ==2004.9.24, Version 0.9.9==
'                   *Fixed a bug in decode HTML-content-type mails
'           
'                   ==2004.9.27, Version 0.9.10==
'                   *Fixed a bug when reading mail from file which contains mail end symbol "."
'
'                   ==2004.9.29, Version 0.9.11==
'                   *Fixed a bug in decode quote-printable formated subject string
'
'                   ==2004.10.12, Version 0.9.12==
'                   *The ReturnPath property now will return "From" address if the "Return-path" 
'                   field is not included in mail content
'
'                   ==2004.10.14, Version 0.9.13==
'                   *Now ignore the X-Priority filed when decode mail because of there is not a standard type of this field
'                   *Fixed a bug that it will ignore attachment with no file name which will ignore the
'                   attachments which is mails forwarded as attachments by Outlook and other program, because 
'                   these attachments has no file name and Content-type is message/rfc822
'
'                   ==2004.10.15, Version 0.9.14==
'                   *Fixed a bug may cause error when the "name" field of attachments in multipart/related mails are the same
'                   Now detect "Content-location" field first to avoid it.
'                   *Add ReadFromStream() method to read mail source code from a memory stream or a file stream 
'                   in order to increase performance when using the memory stream. 
'                   *Use system's default encoding to decode 8bit chars(beta)
'
'                   ==2004.10.27, Version 0.9.15==
'                   *Fixed a bug in ConvertStringEncodingFromBase64() method, now find "=?" instead of only find "?" to determind
'                   whether the string is formatted as "=?xxxx=?".
'                   Here can NOT use StartWith() to determind because some string start with '"', mostly in mail addresses.
'
'                   ==2004.11.22, Version 0.9.16==
'                   *Add UniqueID property, delete Index property
'
'                   ==2004.12.3, Version 0.9.17==
'                   *Add SaveToStream() method for easy use
'                   *Add AddCustomHeaderField() method for custom header
'
'                   ==2004.12.7, Version 0.9.18==
'                   *Modifyed SaveToStream() method, allow only mail header to be outputed to stream
'
'                   ==2004.12.11, Version 0.9.19==
'                   *Use the "Importance" field in mail header for mail priority
'
'                   ==2005.2.3, Version 0.9.20==
'                   *Ignore the exception which caused by related non-image object
'                   This type of non-image object may be dangerous or useless, but can't make sure of this.
'
'                   ==2005.4.8, Version 0.9.21==
'                   *Modify GetValueString() method in order to ignore the miss quotation marks in multiline base64 string format.
'                   This change still can't resolve the issue that too long non-englisth attachment file names is not supported,
'                   only ignore the exception when this scence occurs.
'
'                   ==2005.12.22, Version 0.9.22==
'                   *Modify SplitHeader() method, use KEY+<SPACE> as field searching key word instead of only using a KEY which may
'                   cause decode exception when KEY is used as a normal word in field values.
'                   *Add "Reply-To" property(mail header field)
'                   *Add "In-Reply-To" property(mail header field)
'
' Notes:            * All chars(Chinese, English) in mail include subject and body
'                   are encoded by UTF-8 before sending
'                   * Mail encoding are using BASE64
'                   e.g. String->bytes->UTF-8 bytes->BASE64 String
'                   
'===============================================================================
Imports System.Text
Imports System.Web
Imports System.IO
Imports QMailClient

''' -----------------------------------------------------------------------------
''' Project	 : QMailClient
''' Class	 : EMail
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Represents an E-mail object. This class can be inherited.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Tony]	2005-12-1	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class EMail
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Mail priority enumeration.
    ''' </summary>
    ''' <remarks>
    ''' It's just a priority flag, can be ignored by other mail client program.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Enum MailPriority As Short
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Low priority
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Low = 0
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Normal priority(default)
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Normal = 1
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' High priority
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        High = 2
    End Enum

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Mail content type supported by QMailClient library.
    ''' </summary>
    ''' <remarks>
    ''' After initialize a QMail object, this is the default content type.
    ''' And a Empty content type is not allowed when sending the mail.
    ''' It should be set before sending mails.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Enum MailContentType
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Null content
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Empty
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Not supported content type will be set to UnknownOrDefault.
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        UnknownOrDefault
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Plain text mail
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        TextPlain
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' HTML mail
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        TextHTML
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Mails with attachments or other contents.
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        MultipartMixed
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Mails with two parts which are alternative, generally a text in both plain 
        ''' text and HTML format can be found in the mail body.
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        MultipartAlternative
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Mails with more than one parts which are related, generally it is a HTML 
        ''' mail with some pictures in it.
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        MultipartRelated
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Mail status report sent back by mail servers.
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        MultipartReport
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Attachment content type, octet stream
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        ApplicationOctetStream
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Attachment content type, compressed attachments
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        ApplicationXCompress
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' JPEG Image
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        ImageJPEG
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' GIF Image
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        ImageGIF
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Other image(TIFF,BMP and etc.)
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        ImageOther
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Audio file
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Audio
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Message from mail servers
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        MessageRFC822
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Message from mail servers which reports mail delivery status(Failed delivery)
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        MessageDeliveryStatus
    End Enum

    ''' -----------------------------------------------------------------------------
    ''' Project	 : QMailClient
    ''' Class	 : EMail.MailAttachmentCollection
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Represents a mail attachments' collection, contains the attachements which will be sent with 
    ''' the mail.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class MailAttachmentCollection
        Implements IEnumerable
        Protected m_Attachments As Hashtable
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Return Mail attachment items.
        ''' </summary>
        ''' <param name="Name">Item name, it should be unique</param>
        ''' <value>An instance of a MailAttachment object</value>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Default Public Overloads ReadOnly Property Item(ByVal Name As String) As MailAttachment
            Get
                Return m_Attachments.Item(Name)
            End Get
        End Property
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Attachments' count in the collection.
        ''' </summary>
        ''' <value>A int32 value</value>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public ReadOnly Property Count() As Int32
            Get
                Return m_Attachments.Count
            End Get
        End Property
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Get enumerator function used by "For Each" statement.
        ''' </summary>
        ''' <returns>Interface IEnumerator</returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return m_Attachments.Values.GetEnumerator
        End Function

        Public Sub New()
            m_Attachments = New Hashtable
        End Sub
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Add an attachment into the collection.
        ''' </summary>
        ''' <param name="Item">Should be a valid instance of a MailAttachment.</param>
        ''' <remarks>
        ''' An Argument Exception will be thrown when Item is null or invalid.
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub Add(ByVal Item As MailAttachment)
            If Item Is Nothing Then
                Throw New ArgumentException("Item can't be null")
            End If
            'Ignore
            If Not m_Attachments.Contains(Item.FilePath) Then
                m_Attachments.Add(Item.FilePath, Item)
            End If
        End Sub
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Remove an attachment from the collection.
        ''' </summary>
        ''' <param name="Item">An instance of MailAttachment.</param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub Remove(ByVal Item As MailAttachment)
            m_Attachments.Remove(Item.FilePath)
        End Sub
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Remove an attachment from the collection.
        ''' </summary>
        ''' <param name="Name">The attachment name used when call Add method.</param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub Remove(ByVal Name As String)
            m_Attachments.Remove(Name)
        End Sub
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Remove all attachments from the collection.
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Tony]	2005-12-1	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub Clear()
            m_Attachments.Clear()
        End Sub
    End Class


    'Boundary string used in mail body
    Protected Const BoundaryString = "===QMailClient_Library_Mark_Version_0919==="

    Protected Friend m_UniqueID As String
    Protected Friend m_Subject As String
    Protected Friend m_To As String
    Protected Friend m_From As String
    Protected Friend m_Date As DateTime
    Protected Friend m_Header As String
    Protected Friend m_Body As String              'Only store plain text body content
    Protected Friend m_BodyHTML As String          'Optional content, used to store HTML body content
    Protected Friend m_ReturnPath As String
    Protected Friend m_Bcc As String
    Protected Friend m_Cc As String
    Protected Friend m_Priority As MailPriority = MailPriority.Normal
    Protected Friend m_SourceCode As String
    Protected Friend m_ContentType As MailContentType = MailContentType.Empty    'The content type of the whole mail, not a single part
    Protected Friend m_MessageID As String
    Protected Friend m_Attachments As New MailAttachmentCollection
    Protected Friend m_Size As Int32
    Protected Friend m_CustomHeader As String
    Protected Friend m_ReplyTo As String
    Protected Friend m_InReplyTo As String

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return unique ID of a mail.
    ''' </summary>
    ''' <value>A long string</value>
    ''' <remarks>
    ''' This ID is used to get and delete mails from the mail server, it's unique on a certain server.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property UniqueID() As String
        Get
            Return m_UniqueID
        End Get
    End Property


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return mail size in bytes.
    ''' </summary>
    ''' <value>A int32 value</value>
    ''' <remarks>
    ''' This unit is BYTE.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property Size() As Int32
        Get
            Return m_Size
        End Get
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set mail subject.
    ''' </summary>
    ''' <value>Subject description string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Subject() As String
        Get
            Return m_Subject
        End Get
        Set(ByVal Value As String)
            m_Subject = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set MailTo addresses.
    ''' </summary>
    ''' <value>Address string</value>
    ''' <remarks>
    ''' It can accept multi-address, multi-address should be split with ";".
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property [To]() As String
        Get
            Return m_To
        End Get
        Set(ByVal Value As String)
            m_To = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set MailFrom address.
    ''' </summary>
    ''' <value>Address string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property From() As String
        Get
            Return m_From
        End Get
        Set(ByVal Value As String)
            m_From = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set mail send/receive date.
    ''' </summary>
    ''' <value>DateTime value</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property [Date]() As DateTime
        Get
            Return m_Date
        End Get
        Set(ByVal Value As DateTime)
            m_Date = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set mail header source codes.
    ''' </summary>
    ''' <value>Mail header string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Header() As String
        Get
            Return m_Header
        End Get
        Set(ByVal Value As String)
            m_Header = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set mail body source codes.
    ''' </summary>
    ''' <value>Mail body string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Body() As String
        Get
            Return m_Body
        End Get
        Set(ByVal Value As String)
            m_Body = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return mail body source codes when mail is HTML formatted.
    ''' </summary>
    ''' <value>HTML codes</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property BodyHTML() As String
        Get
            Return m_BodyHTML
        End Get
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set mail address which should be used when replying the mail.
    ''' </summary>
    ''' <value>Address string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property ReturnPath() As String
        Get
            Return m_ReturnPath
        End Get
        Set(ByVal Value As String)
            m_ReturnPath = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set Blind Carbon Copy addresses.
    ''' </summary>
    ''' <value>Address string</value>
    ''' <remarks>
    ''' It can accept multi-address, multi-address should be split with ";".
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Bcc() As String
        Get
            Return m_Bcc
        End Get
        Set(ByVal Value As String)
            m_Bcc = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set Carbon Copy addresses.
    ''' </summary>
    ''' <value>Address string</value>
    ''' <remarks>
    ''' It can accept multi-address, multi-address should be split with ";".
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Cc() As String
        Get
            Return m_Cc
        End Get
        Set(ByVal Value As String)
            m_Cc = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set mail priority.
    ''' </summary>
    ''' <value>MailPriority enumeration</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Priority() As MailPriority
        Get
            Return m_Priority
        End Get
        Set(ByVal Value As MailPriority)
            m_Priority = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return mail content type.
    ''' </summary>
    ''' <value>MailContentType enumeration</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property ContentType() As MailContentType
        Get
            Return m_ContentType
        End Get
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return all source codes in a mail.
    ''' </summary>
    ''' <value>Source code string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property SourceCode() As String
        Get
            Return m_SourceCode
        End Get
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return the MessageID in the mail header section.
    ''' </summary>
    ''' <value>Message ID string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property MessageID() As String
        Get
            Return m_MessageID
        End Get
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set the attachments in the mail.
    ''' </summary>
    ''' <value>MailAttachmentCollection</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Attachments() As MailAttachmentCollection
        Get
            Return m_Attachments
        End Get
        Set(ByVal Value As MailAttachmentCollection)
            m_Attachments = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set the reply address of the mail
    ''' </summary>
    ''' <value>Mail address string</value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-22	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property ReplyTo() As String
        Get
            Return m_ReplyTo
        End Get
        Set(ByVal Value As String)
            m_ReplyTo = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Return or set the in-reply-to information of the mail
    ''' </summary>
    ''' <value>A unique mail ID</value>
    ''' <remarks>
    ''' This field contains a piece of text describing a message you are replying to.  
    ''' Some mail systems can use this information to correlate related pieces of mail.  
    ''' It should be a unique mail ID like "BEE5E5C3FA49A445C23F034F75@abc.com"
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-22	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property InReplyTo() As String
        Get
            Return m_InReplyTo
        End Get
        Set(ByVal Value As String)
            m_InReplyTo = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convert content type string into MailContentType enumeration.
    ''' </summary>
    ''' <param name="TypeString">Content type string in the mail body description.</param>
    ''' <returns>MailContentType enumeration</returns>
    ''' <remarks>
    ''' Used for receiving mails
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Function GetContentTypeFromString(ByVal TypeString As String) As MailContentType
        Select Case TypeString.ToLower
            Case "application/octet-stream"
                Return ContentType.ApplicationOctetStream
            Case "application/x-compress"
                Return ContentType.ApplicationXCompress
            Case "audio/x-wave"
                Return ContentType.Audio
            Case "text/html"
                Return ContentType.TextHTML
            Case "text/plain"
                Return ContentType.TextPlain
            Case "image/jpeg"
                Return ContentType.ImageJPEG
            Case "image/gif"
                Return ContentType.ImageGIF
            Case "multipart/mixed"
                Return ContentType.MultipartMixed
            Case "multipart/alternative"
                Return ContentType.MultipartAlternative
            Case "multipart/related"
                Return ContentType.MultipartRelated
            Case "multipart/report"
                Return ContentType.MultipartReport
            Case "message/rfc822"
                Return ContentType.MessageRFC822
            Case "message/delivery-status"
                Return ContentType.MessageDeliveryStatus
            Case Else
                Return ContentType.UnknownOrDefault
        End Select
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convert MailContentType enumeration into content type string.
    ''' </summary>
    ''' <param name="Type">MailContentType enumeration</param>
    ''' <returns>Content type string</returns>
    ''' <remarks>
    ''' Used for sending mails
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Overloads Function ConvertContentTypeToString(ByVal Type As MailContentType) As String
        Select Case Type
            Case ContentType.ApplicationOctetStream
                Return "application/octet-stream"
            Case ContentType.ApplicationXCompress
                Return "application/x-compress"
            Case ContentType.Audio
                Return "audio/x-wave"
            Case ContentType.TextHTML
                Return "text/html"
            Case ContentType.TextPlain
                Return "text/plain"
            Case ContentType.ImageJPEG
                Return "image/jpeg"
            Case ContentType.ImageGIF
                Return "image/gif"
            Case ContentType.MultipartMixed
                Return "multipart/mixed"
            Case ContentType.MultipartAlternative
                Return "multipart/alternative"
            Case ContentType.MultipartRelated
                Return "multipart/related"
            Case ContentType.MultipartReport
                Return "multipart/report"
            Case ContentType.MessageRFC822
                Return "message/rfc822"
            Case ContentType.MessageDeliveryStatus
                Return "message/delivery-status"
        End Select
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Get content type of an attachment according to the given attachment's file name.
    ''' </summary>
    ''' <param name="FileName">The file name of an attachment</param>
    ''' <returns>Content type string</returns>
    ''' <remarks>
    ''' Used when sending mails
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Overloads Function ConvertContentTypeToString(ByVal FileName As String) As String
        Select Case Path.GetExtension(FileName).ToUpper
            Case "TXT"
                Return ConvertContentTypeToString(ContentType.TextPlain)
            Case "JPG", "JPEG"
                Return ConvertContentTypeToString(ContentType.ImageJPEG)
            Case "GIF"
                Return ConvertContentTypeToString(ContentType.ImageGIF)
            Case "WAV", "MP3", "OGG"
                Return ConvertContentTypeToString(ContentType.Audio)
            Case "HTML", "HTM"
                Return ConvertContentTypeToString(ContentType.TextHTML)
            Case Else
                Return ConvertContentTypeToString(ContentType.ApplicationOctetStream)
        End Select
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Generate the mail header, body and sourcecode from mail properties.
    ''' </summary>
    ''' <remarks>
    ''' Used when sending mails
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Sub EncodeMail()
        CombineHeader()
        CombineBody()
        m_SourceCode = m_Header & m_Body
    End Sub

    '
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Analyze the mail's sourcecode.
    ''' </summary>
    ''' <param name="OnlyDecodeMailHeaderInformation"></param>
    ''' <remarks>
    ''' Used when receiving mails.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Sub DecodeMail(Optional ByVal OnlyDecodeMailHeaderInformation As Boolean = False)
        If m_SourceCode = "" Then Exit Sub
        Dim intPos As Int32 = m_SourceCode.IndexOf(vbCrLf & vbCrLf)
        If intPos = -1 Then
            'This indicates there is no body text, only occures when sending mail using SMTP commands directly
            m_Header = m_SourceCode
            SplitHeader()
        Else
            m_Header = m_SourceCode.Substring(0, intPos + 2)    '+2 needed, must include the first one of the last two <CRLF>
            SplitHeader()

            If OnlyDecodeMailHeaderInformation = False Then
                m_Body = m_SourceCode.Substring(intPos)
                SplitBody()
            End If
        End If
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Create codes in mail body section.
    ''' </summary>
    ''' <remarks>
    ''' Used when sending mails.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Sub CombineBody()
        If Me.Attachments.Count = 0 Then
            m_Body = ConvertStringEncodingToBase64Ex(m_Body, "utf-8")
        Else
            Dim strBody As New StringBuilder
            Dim i As Int32
            'Add mulit-part message title
            strBody.Append("This is a multi-part message in MIME format." & vbCrLf & vbCrLf)
            'Add boundary
            strBody.Append("--" & BoundaryString & vbCrLf)
            'Add body text
            strBody.Append("Content-Type: " & ConvertContentTypeToString(ContentType.TextPlain) & ";" & vbCrLf & vbTab & "charset=""utf-8""" & vbCrLf)
            strBody.Append("Content-Transfer-Encoding: base64" & vbCrLf)
            strBody.Append(vbCrLf)
            strBody.Append(ConvertStringEncodingToBase64Ex(m_Body, "utf-8") & vbCrLf)
            'Add attachments 
            Dim attach As MailAttachment
            For Each attach In Attachments
                'Generate content code first
                attach.GenerateBase64ContentFromFile()
                'Add boundary title
                strBody.Append(vbCrLf & "--" & BoundaryString & vbCrLf)
                strBody.Append("Content-Type: " & ConvertContentTypeToString(attach.FileName) & ";" & vbCrLf & vbTab & "name=""")

                If Encoding.Unicode.GetByteCount(attach.FileName) <> attach.FileName.Length Then
                    'Contains other chars except English chars
                    strBody.Append(ConvertStringEncodingToBase64(attach.FileName, "utf-8") & """" & vbCrLf)
                Else
                    'Only English chars
                    strBody.Append(attach.FileName & """" & vbCrLf)
                End If

                strBody.Append("Content-Transfer-Encoding: base64" & vbCrLf)
                strBody.Append("Content-Disposition: attachment;" & vbCrLf & vbTab)
                strBody.Append("filename=""")
                If Encoding.Unicode.GetByteCount(attach.FileName) <> attach.FileName.Length Then
                    'Contains other chars except English chars
                    strBody.Append(ConvertStringEncodingToBase64(attach.FileName, "utf-8"))
                Else
                    'Only English chars
                    strBody.Append(attach.FileName)
                End If
                strBody.Append("""" & vbCrLf & vbCrLf)
                'Add content
                strBody.Append(vbCrLf & attach.Content & vbCrLf & vbCrLf)
            Next
            strBody.Append("--" & BoundaryString & vbCrLf)
            m_Body = strBody.ToString

            strBody = Nothing
            GC.Collect()
        End If
        'm_SourceCode = m_Header & vbCrLf & vbCrLf & m_Body
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Analyze mail body.
    ''' </summary>
    ''' <param name="MailHeader">Optional. If MailHeader is not given, it will analyze mail header in the body instead, otherwise use the given header information.</param>
    ''' <remarks>
    ''' Used when receiving mails.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Sub SplitBody(Optional ByVal MailHeader As String = "")
        If MailHeader = "" Then MailHeader = m_Header

        If m_ContentType = MailContentType.Empty Then
            'Ensure the ContentType is not empty
            m_ContentType = GetContentTypeFromString(GetValueString(MailHeader, "Content-Type:", , True))
        End If

        Select Case m_ContentType
            Case ContentType.MultipartMixed, ContentType.MultipartAlternative, ContentType.MultipartRelated, ContentType.MultipartReport
                'Multipart message
                Dim intPosStart As Int32 = 0        'Start boundary indicator
                Dim intPosEnd As Int32 = 0          'End boundary indicator
                Dim intPos As Int32 = 0             'Position indicator
                Dim strBoundary As String = "--" & GetValueString(MailHeader, "boundary=", True)
                Dim strContent As String
                Dim strCharset As String
                Dim strBody As String
                Dim strHTML As String
                Dim id As New Random                'Random ID generator

                'Indicate the beginning of the first boundary
                'JUST DO ONCE HERE
                intPosStart = m_SourceCode.IndexOf(strBoundary, 0)
                Do
                    'Indicate the beginning of the second boundary
                    intPosEnd = m_SourceCode.IndexOf(strBoundary, intPosStart + 1)
                    If intPosEnd = -1 Then
                        'Reach the end of the mail
                        Exit Do
                    End If
                    'Get the content between two boundary
                    strContent = m_SourceCode.Substring(intPosStart + strBoundary.Length, intPosEnd - intPosStart - strBoundary.Length)

                    '====Decode the content====
                    If GetValueString(strContent, "Content-Disposition:", , True).ToLower = "attachment" Then
                        'Attachment detected
                        Dim attach As New MailAttachment
                        'get content-type
                        Dim ct As MailContentType = GetContentTypeFromString(GetValueString(strContent, "Content-Type:", , True))
                        If ct = ContentType.MessageRFC822 Then
                            attach.m_FileName = "message" & id.Next(1000, 9999) & ".eml"
                        Else
                            attach.m_FileName = ConvertStringEncodingFromBase64(GetValueString(strContent, "filename=", True))
                        End If
                        'This indicates there are mails in attachments(Outlook forward mails will format mail like this)
                        If attach.m_FileName = "" And GetContentTypeFromString(GetValueString(strContent, "Content-Type:", , True)) = ContentType.MessageRFC822 Then
                            'In this condition, if the filename is empty then give it a new name 

                            'Try use suject as file name first
                            Dim strSubjuect As String = GetEncodedValueString(strContent, "Subject:")
                            If strSubjuect <> "" Then
                                attach.m_FileName = strSubjuect & "_" & id.Next(1000, 9999) & ".eml"
                            Else
                                attach.m_FileName = "message" & id.Next(1000, 9999) & ".eml"
                            End If
                        End If

                        If attach.m_FileName <> "" Then                     'Ignore attachment which has no name
                            attach.m_FilePath = attach.m_FileName           'Set default file path to the same of the file name
                            intPos = strContent.IndexOf(vbCrLf & vbCrLf)
                            attach.m_Content = strContent.Substring(intPos + 4)
                            attach.m_TransEncoding = GetValueString(strContent, "Content-Transfer-Encoding:")
                            'Add attachment into AttachmentCollection
                            m_Attachments.Add(attach)
                        End If
                    Else
                        'Only text or have sub content-type
                        Select Case GetContentTypeFromString(GetValueString(strContent, "Content-Type:", , True))
                            Case ContentType.TextHTML
                                'Contains HTML body
                                strHTML &= DecodeTextContent(strContent, , , True)
                            Case ContentType.TextPlain, ContentType.MessageDeliveryStatus
                                'Contains plain text
                                strBody &= DecodeTextContent(strContent, , , True)
                            Case ContentType.MultipartAlternative, ContentType.MultipartMixed, ContentType.MultipartRelated, ContentType.MultipartReport
                                'Contains sub content-type
                                Dim strHeader As String = strContent.Substring(0, strContent.IndexOf(vbCrLf & vbCrLf) + 2)
                                strContent = strContent.Substring(strContent.IndexOf(vbCrLf & vbCrLf) + 4)
                                '**Self call to split body part**
                                SplitBody(strHeader)
                            Case ContentType.MessageRFC822
                                'Contains the whole mail content
                                Dim attach As New MailAttachment
                                'Try use suject as file name first
                                Dim strSubjuect As String = GetEncodedValueString(strContent, "Subject:")
                                If strSubjuect <> "" Then
                                    attach.m_FileName = strSubjuect & "_" & id.Next(1000, 9999) & ".eml"
                                Else
                                    attach.m_FileName = "message" & id.Next(1000, 9999) & ".eml"
                                End If
                                attach.m_FilePath = attach.m_FileName           'Set default file path to the same of the file name
                                intPos = strContent.IndexOf(vbCrLf & vbCrLf)
                                attach.m_Content = strContent.Substring(intPos + 4)
                                attach.m_TransEncoding = "7bit"                 'The message is always encoded by 7bit 
                                'Add attachment into AttachmentCollection
                                m_Attachments.Add(attach)
                            Case Else
                                'May be attachments or other
                                If GetValueString(strContent, "Content-Type:", , True).Substring(0, 5).ToLower = "image" Then
                                    'Not an attachment but an inner element of the mail(often used in MultipartRelated content-type
                                    'Image detected, save as attachment

                                    'Set body content type 
                                    'm_ContentType = ContentType.MultipartRelated

                                    Dim attach As New MailAttachment
                                    'Get "Content-Location " field value as file name first, if not existed then get "name" field value.
                                    'Because "name" fields may be the same for the attachments.
                                    attach.m_FileName = ConvertStringEncodingFromBase64(GetValueString(strContent, "Content-Location:"))
                                    If attach.m_FileName = "" Then
                                        attach.m_FileName = ConvertStringEncodingFromBase64(GetValueString(strContent, "name=", True))
                                    End If
                                    attach.m_FilePath = attach.m_FileName           'Set default file path to the same of the file name
                                    intPos = strContent.IndexOf(vbCrLf & vbCrLf)
                                    attach.m_Content = strContent.Substring(intPos)
                                    attach.m_TransEncoding = GetValueString(strContent, "Content-Transfer-Encoding:")
                                    'Get content id
                                    Dim strCID As String = GetValueString(strContent, "Content-ID:")
                                    If strCID.StartsWith("<") Then
                                        strCID = strCID.Substring(1, strCID.Length - 2)
                                    End If
                                    attach.m_ContentID = strCID
                                    'Add attachment into AttachmentCollection
                                    m_Attachments.Add(attach)
                                Else
                                    'Ignore this type of inline attachments
                                    'Throw New Exception("unknown body element detected")

                                End If
                        End Select
                    End If


                    '========
                    'Set the next boundary start position
                    'AND DO NOT NEED TO SEARCH START BOUNDARY AGAIN!!!
                    'JUST USE LAST END BOUNDARY INSTEAD!!!
                    intPosStart = intPosEnd
                Loop
                If strBody <> "" Then m_Body = strBody
                If strHTML <> "" Then m_BodyHTML = strHTML
            Case ContentType.TextPlain, ContentType.MessageDeliveryStatus
                'Single part message
                'If message is encoding with base64 then decode, otherwise do nothing
                m_Body = DecodeTextContent(m_Body, GetValueString(m_Header, "Content-Transfer-Encoding:"), GetValueString(m_Header, "charset=", True))
            Case ContentType.TextHTML
                'Single part HTML message
                m_BodyHTML = DecodeTextContent(m_Body, GetValueString(m_Header, "Content-Transfer-Encoding:"), GetValueString(m_Header, "charset=", True))
            Case Else
                'May be applications or other, save it as an attachment
                If GetValueString(m_Header, "Content-Disposition:", , True).ToLower = "attachment" Then
                    m_Body = ""         'There is no body text for this kind of mail
                    'Attachment detected
                    Dim attach As New MailAttachment
                    Dim intPos As Int32
                    attach.m_FileName = ConvertStringEncodingFromBase64(GetValueString(m_Header, "filename=", True))
                    attach.m_FilePath = attach.m_FileName           'Set default file path to the same of the file name
                    intPos = m_SourceCode.IndexOf(vbCrLf & vbCrLf)
                    attach.m_Content = m_SourceCode.Substring(intPos)
                    attach.m_TransEncoding = GetValueString(m_Header, "Content-Transfer-Encoding:")
                    'Add attachment into AttachmentCollection
                    m_Attachments.Add(attach)
                Else
                    'Error in decoding mail, just show the mail source code here
                    'm_ContentType = ContentType.TextPlain
                    m_Body = "--==Unknown mail content type, given up decoding this mail==--" & vbCrLf & vbCrLf & m_Body
                End If
        End Select
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Generate mail header section.
    ''' </summary>
    ''' <remarks>
    ''' Used when sending mails.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Sub CombineHeader()
        m_Header = ""
        'Add Subject
        If m_Subject.Length <> Encoding.Unicode.GetByteCount(m_Subject) Then
            'Contains non-English chars
            m_Header &= "Subject: " & ConvertStringEncodingToBase64(m_Subject, "utf-8") & vbCrLf
        Else
            m_Header &= "Subject: " & m_Subject & vbCrLf
        End If
        'Add Reply To
        If m_ReplyTo.Trim <> "" Then
            m_Header &= "Reply-To: " & m_ReplyTo & vbCrLf
        End If

        'Add From
        m_Header &= "From: " & m_From & vbCrLf
        'Add To
        m_Header &= "To: " & m_To & vbCrLf
        'Add Cc
        If m_Cc.Trim <> "" Then
            m_Header &= "Cc: " & m_Cc & vbCrLf
        End If
        'Add priority field
        m_Header &= "Importance: " & m_Priority.ToString & vbCrLf

        'Add In-Reply-To
        If m_InReplyTo.Trim <> "" Then
            m_Header &= "In-Reply-To: <" & m_InReplyTo & ">" & vbCrLf
        End If

        'Add MIME Version and other
        m_Header &= "MIME-Version: 1.0" & vbCrLf
        If Me.Attachments.Count = 0 Then
            m_Header &= "Content-Type: " & ConvertContentTypeToString(ContentType.TextPlain) & ";" & vbCrLf & vbTab & "charset=""utf-8""" & vbCrLf
            m_Header &= "Content-Transfer-Encoding: base64" & vbCrLf
        Else
            m_Header &= "Content-Type: " & ConvertContentTypeToString(ContentType.MultipartMixed) & ";" & vbCrLf & vbTab & "boundary=""" & BoundaryString & """" & vbCrLf
        End If

        'Add additional information
        If m_CustomHeader <> "" Then
            m_Header &= m_CustomHeader & vbCrLf
        End If
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Analyze mail header section.
    ''' </summary>
    ''' <remarks>
    ''' Used when receiving mails.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Sub SplitHeader()
        '==Only trim mail address, such as From, To, CC==
        'Get "Subject"
        m_Subject = GetEncodedValueString(m_Header, "Subject: ")

        'Get "From"
        m_From = GetEncodedValueString(m_Header, "From: ").Trim

        'Get "To"
        m_To = GetEncodedValueString(m_Header, "To: ", True).Trim

        'Get "Date"
        m_Date = GetDateTimeFromString(GetValueString(m_Header, "Date: "))

        'Get "Reply-To"
        m_ReplyTo = GetValueString(m_Header, vbCrLf & "Reply-To: ") 'Specail key word in order to differ from "In-Reply-To"

        'Get "Return-Path"
        'If the return-path is empty, use "From" address instead
        m_ReturnPath = GetValueString(m_Header, "Return-Path: ")
        If m_ReturnPath = "" Then
            Dim strAddr As String = GetValueString(m_Header, "From: ")
            Dim i As Int16 = strAddr.IndexOf("<")
            If i <> -1 Then
                m_ReturnPath = strAddr.Substring(i + 1, strAddr.IndexOf(">") - i - 1)
            Else
                m_ReturnPath = strAddr
            End If
        End If

        'Get "Cc"
        m_Cc = GetEncodedValueString(m_Header, "Cc: ", True).Trim

        'Get "Priority"
        Dim strPri As String = GetValueString(m_Header, "Importance: ").Trim
        If strPri = "" Then
            m_Priority = MailPriority.Normal
        Else
            Select Case strPri.ToLower
                Case "low"
                    m_Priority = MailPriority.Low
                Case "normal"
                    m_Priority = MailPriority.Normal
                Case "high"
                    m_Priority = MailPriority.High
            End Select
        End If

        'Get "Message-ID"(should delete char "<" and ">"
        m_MessageID = GetValueString(m_Header, "Message-ID: ")
        If m_MessageID.StartsWith("<") Then
            m_MessageID = m_MessageID.Substring(1, m_MessageID.Length - 2)
        End If

        'Get "In-Reply-To"(should delete char "<" and ">"
        m_InReplyTo = GetValueString(m_Header, "In-Reply-To: ")
        If m_InReplyTo.StartsWith("<") Then
            m_InReplyTo = m_InReplyTo.Substring(1, m_InReplyTo.Length - 2)
        End If

        'Get "Content-type"
        m_ContentType = GetContentTypeFromString(GetValueString(m_Header, "Content-Type: ", , True))

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Get date time information from date time string in mail header.
    ''' </summary>
    ''' <param name="DateTimeString">Date time string with time zone</param>
    ''' <returns>A DateTime value</returns>
    ''' <remarks>
    ''' In this version, time zone is ignored.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Shared Function GetDateTimeFromString(ByVal DateTimeString As String) As DateTime
        If DateTimeString = "" Then
            Return Nothing
        Else
            Try
                Dim strDateTime As String
                If DateTimeString.IndexOf("+") <> -1 Then
                    strDateTime = DateTimeString.Substring(0, DateTimeString.IndexOf("+"))
                ElseIf DateTimeString.IndexOf("-") <> -1 Then
                    strDateTime = DateTimeString.Substring(0, DateTimeString.IndexOf("-"))
                Else
                    strDateTime = DateTimeString
                End If
                Dim dt As DateTime
                dt = DateTime.Parse(strDateTime)

                Return dt
            Catch
                Return Nothing
            End Try
        End If
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Decode the non-ascii type of senders' and the receivers' names in "FROM:" & "TO:" string.
    ''' </summary>
    ''' <param name="SourceString">Mail codes</param>
    ''' <param name="Key">Searching key</param>
    ''' <param name="SplitBySemicolon">Indicate whether the string is ended with a semicolon,default is False.</param>
    ''' <returns>A decoded string which can be correct displayed</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Shared Function GetEncodedValueString(ByVal SourceString As String, ByVal Key As String, Optional ByVal SplitBySemicolon As Boolean = False) As String
        Dim j As Int32
        Dim strValue As String
        Dim strSource As String = SourceString.ToLower      'Use strSource as a copy of SourceString to find but get value only in SourceString
        Dim strReturn As String
        j = strSource.IndexOf(Key.ToLower)
        If j <> -1 Then
            j += Key.Length                 'j indecates the end index of the Key string
            strValue = SourceString.Substring(j, strSource.IndexOf(vbCrLf, j) - j).TrimEnd       'Get the string before the string Key and the <CRLF> symbol
            Do
                If strValue.IndexOf("=?") <> -1 Then
                    'Contains user names, then use names instead of using email address
                    If SplitBySemicolon = True Then
                        strReturn &= ConvertStringEncodingFromBase64(strValue) & "; "
                    Else
                        strReturn &= ConvertStringEncodingFromBase64(strValue)
                    End If
                Else
                    strReturn &= strValue
                End If
                j += strValue.Length + 2            '<CRLF>: +2
                If strSource.IndexOf(vbCrLf, j) = -1 Then
                    Exit Do                         'Reach the end of the mail header, exit
                Else
                    strValue = SourceString.Substring(j, strSource.IndexOf(vbCrLf, j) - j).TrimEnd   'Get the next string on the next line
                End If
            Loop While strValue.StartsWith(" ") Or strValue.StartsWith(vbTab)                           'There must be continous string
        Else
            strReturn = ""
        End If
        Return strReturn
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Generally used to get string according to the key given in body content or 
    ''' get ascii values in mail header section.
    ''' </summary>
    ''' <param name="SourceString">Mail codes</param>
    ''' <param name="Key">Searching key</param>
    ''' <param name="ContainsQuotationMarks">Optional. Indicate whether the string is contains a pair of quotation marks, default is False.</param>
    ''' <param name="ContainsSemicolon">Optional. Indicate whether the string is contains a semicolon, default is False.</param>
    ''' <returns>The string required according to the Key</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Function GetValueString(ByVal SourceString As String, ByVal Key As String, Optional ByVal ContainsQuotationMarks As Boolean = False, Optional ByVal ContainsSemicolon As Boolean = False) As String
        Dim j As Int32
        Dim strReturn As String
        Dim strSource As String = SourceString.ToLower      'Use strSource as a copy of SourceString to find but get value only in SourceString
        j = strSource.IndexOf(Key.ToLower)
        If j <> -1 Then
            j += Key.Length                 'j indecates the end index of the Key string
            strReturn = SourceString.Substring(j, strSource.IndexOf(vbCrLf, j) - j).TrimStart.TrimEnd   'Get the string before the string Key and the <CRLF> symbol

            'Delete semicolon(;) in string
            If ContainsSemicolon = True Then
                If strReturn.IndexOf(";") <> -1 Then
                    strReturn = strReturn.Substring(0, strReturn.IndexOf(";"))
                End If
            End If

            'Delete quotation marks(") in string
            If ContainsQuotationMarks = True Then
                Dim i As Int32 = strReturn.IndexOf("""")
                Dim k As Int32
                If i <> -1 Then
                    k = strReturn.IndexOf("""", i + 1)
                    'Ignore the end quotation mark if it is missed
                    'This method caused it can not support too long 
                    'attchment file name in multiline base64 string format
                    'with multiply "=?..?=" marks like:
                    '=?xxxx?=
                    '=?yyyyyyy?=
                    'TODO: Should be advanced
                    If k <> -1 Then
                        strReturn = strReturn.Substring(i + 1, k - i - 1)
                    Else
                        strReturn = strReturn.Substring(i + 1)
                    End If
                End If
            End If

            Return strReturn
        Else
            Return ""
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convert Base64 string into text string.
    ''' </summary>
    ''' <param name="SourceString">Base64 codes</param>
    ''' <returns>Text string</returns>
    ''' <remarks>
    ''' For string with "=?,?="(Charset information included)
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Shared Function ConvertStringEncodingFromBase64(ByVal SourceString As String) As String
        Try
            If SourceString.IndexOf("=?") = -1 Then
                'DO Nothing
                Return SourceString
            Else
                Dim i As Int16 = SourceString.IndexOf("?")
                Dim j As Int16 = SourceString.IndexOf("?", i + 1)
                Dim k As Int16 = SourceString.IndexOf("?", j + 1)
                Dim chrTransEnc As Char = SourceString.Chars(j + 1)
                Select Case chrTransEnc
                    Case "B"
                        Return ConvertStringEncodingFromBase64Ex(SourceString.Substring(k + 1, SourceString.IndexOf("?", k + 1) - k - 1), SourceString.Substring(i + 1, j - i - 1))
                    Case "Q"
                        SourceString = SourceString.Replace("=", "%")                 'Replace '=' with '%'
                        Return HttpUtility.UrlDecode(SourceString.Substring(k + 1, SourceString.IndexOf("?", k + 1) - k - 1), Encoding.GetEncoding(SourceString.Substring(i + 1, j - i - 1)))
                        'Return ConvertStringEncoding(SourceString.Substring(k + 1, SourceString.IndexOf("?", k + 1) - k - 1), SourceString.Substring(i + 1, j - i - 1))
                    Case Else

                        Throw New Exception("unsupported content transfer encoding")
                End Select
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convert Base64 string into text string.
    ''' </summary>
    ''' <param name="SourceString">Base64 codes</param>
    ''' <param name="Charset">Charset string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' For string without "=?,?="(Charset information not included)
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Shared Function ConvertStringEncodingFromBase64Ex(ByVal SourceString As String, ByVal Charset As String)
        Try
            Dim enc As Encoding
            If Charset = "" Then
                enc = Encoding.Default
            Else
                enc = Encoding.GetEncoding(Charset)
            End If

            Return enc.GetString(Convert.FromBase64String(SourceString))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convert text string into Base64 string.
    ''' </summary>
    ''' <param name="SourceString">String to convert</param>
    ''' <param name="Charset">Charset string</param>
    ''' <param name="AutoWordWrap">Add a [CRLF] after each 76 chars</param>
    ''' <returns>Base64 string</returns>
    ''' <remarks>
    ''' Do not add "=?,?="(Do not add charset information with the string).
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Shared Function ConvertStringEncodingToBase64Ex(ByVal SourceString As String, ByVal Charset As String, Optional ByVal AutoWordWrap As Boolean = True) As String
        Dim enc As Encoding = Encoding.GetEncoding(Charset)
        Dim buffer() As Byte = enc.GetBytes(SourceString)
        Dim i As Int32
        Dim strContent As String = Convert.ToBase64String(buffer)
        Dim strTemp As New StringBuilder

        For i = 0 To strContent.Length \ 76 - 1
            strTemp.Append(strContent.Substring(76 * i, 76) & vbLf)
        Next
        strTemp.Append(strContent.Substring(76 * (i)))
        strContent = strTemp.ToString

        Return strContent
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convert text string into Base64 string.
    ''' </summary>
    ''' <param name="SourceString">String to convert</param>
    ''' <param name="Charset">Charset string</param>
    ''' <returns>Base64 string</returns>
    ''' <remarks>
    ''' Add "=?,?="(Add charset information with the string).
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Shared Function ConvertStringEncodingToBase64(ByVal SourceString As String, ByVal Charset As String) As String
        Try
            Dim strResult As String = "=?" & Charset & "?B?"
            strResult &= ConvertStringEncodingToBase64Ex(SourceString, Charset, False) & "?="
            Return strResult
        Catch ex As Exception
            Return ""
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Decode string from ascii encoding
    ''' </summary>
    ''' <param name="SourceString">String to decode</param>
    ''' <param name="Charset">Charset string</param>
    ''' <returns>Decoded string</returns>
    ''' <remarks>
    ''' If charset is invalid, the system default charset is used instead
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Shared Function ConvertStringEncoding(ByVal SourceString As String, ByVal Charset As String) As String

        Try
            Dim strResult As String
            Dim enc As Encoding
            'Use system default encoding for 8bit chars most of which are Chinese chars)
            If Charset = "8bit" Or Charset = "" Then
                enc = Encoding.Default
            Else
                enc = Encoding.GetEncoding(Charset)
            End If

            strResult = enc.GetString(Encoding.ASCII.GetBytes(SourceString))
            Return strResult
        Catch ex As Exception
            Return Encoding.Default.GetString(Encoding.ASCII.GetBytes(SourceString))
        End Try
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' <p>Decode the the part of the mail body which is text-based.</p>
    ''' <p>Support following Content-Transfer-Encoding method: 7bit, binary, base64, quoted-printable</p>
    ''' <p>Unsupported: 8bit</p>
    ''' </summary>
    ''' <param name="Content">Mail body content</param>
    ''' <param name="ContentTransferEncoding">Content transfer encoding description</param>
    ''' <param name="Charset">Charset description</param>
    ''' <param name="ContainsSubHeader">Indicate whether the content contains a sub header which needs to be analyzed, default is False</param>
    ''' <returns>Decoded mail body text</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Friend Function DecodeTextContent(ByVal Content As String, Optional ByVal ContentTransferEncoding As String = "", Optional ByVal Charset As String = "", Optional ByVal ContainsSubHeader As Boolean = False) As String
        Dim strTransEncoding As String
        Dim strCharset As String
        Dim intPos As Int32
        Dim strBody As String

        If ContainsSubHeader = True Then
            'This indicates there is a sub header area
            strBody = Content.Substring(Content.IndexOf(vbCrLf & vbCrLf) + 4)
        Else
            If Content.StartsWith(vbCrLf & vbCrLf) Then
                strBody = Content.Substring(4)
            Else
                strBody = Content
            End If
        End If

        If ContentTransferEncoding <> "" Then
            strTransEncoding = ContentTransferEncoding.ToLower
        Else
            strTransEncoding = GetValueString(Content, "Content-Transfer-Encoding:").ToLower()

        End If

        If Charset = "" Then
            strCharset = GetValueString(Content, "charset=", True)
        Else
            strCharset = Charset
        End If

        Select Case strTransEncoding
            Case "base64"
                strBody = ConvertStringEncodingFromBase64Ex(strBody, strCharset) & vbCrLf & vbCrLf
            Case "quoted-printable"
                strBody = strBody.Replace("=" & vbCrLf, "")         'Delete the '=' at the end of a line
                strBody = strBody.Replace("=", "%")                 'Replace '=' with '%'
                If strCharset <> "" Then
                    Try
                        strBody = HttpUtility.UrlDecode(strBody, Encoding.GetEncoding(strCharset))
                    Catch ex As Exception
                        strBody = HttpUtility.UrlDecode(strBody)
                    End Try
                Else
                    strBody = HttpUtility.UrlDecode(strBody)
                End If
            Case Else '"7bit", "8bit", "binary" or else 
                If strCharset <> "" Then
                    strBody = ConvertStringEncoding(strBody, strCharset)
                Else
                    strBody = strBody
                End If
        End Select
        Return strBody
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Save the mail to a disk file.
    ''' </summary>
    ''' <param name="FilePath">Disk file path</param>
    ''' <param name="AutoRewrite">Enable auto overwrite when file specified is existed</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub SaveToFile(ByVal FilePath As String, Optional ByVal AutoRewrite As Boolean = False)
        If File.Exists(FilePath) = True And AutoRewrite = False Then
            Throw New Exception("File already existed")
        Else

            Dim strmMail As New FileStream(FilePath, FileMode.Create)
            strmMail.Write(Encoding.ASCII.GetBytes(m_SourceCode), 0, Encoding.ASCII.GetByteCount(m_SourceCode))
            strmMail.Close()
        End If
    End Sub

    '
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Read a mail from a disk file.
    ''' </summary>
    ''' <param name="FilePath">Disk file path</param>
    ''' <param name="OnlyDecodeMailHeaderInformation">If true, it only analyze mail header section. This is used to increase performance when there only need to view mail properties, not mail content.</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub ReadFromFile(ByVal FilePath As String, Optional ByVal OnlyDecodeMailHeaderInformation As Boolean = False)
        If File.Exists(FilePath) = True Then
            Dim strmMail As New FileStream(FilePath, FileMode.Open)

            ReadFromStream(strmMail)
        Else
            Throw New Exception("File not found")
        End If

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Read a mail from a stream, file stream, memory stream or others.
    ''' </summary>
    ''' <param name="SourceStream">An instance of a stream.</param>
    ''' <param name="OnlyDecodeMailHeaderInformation">If true, it only analyze mail header section. This is used to increase performance when there only need to view mail properties, not mail content.</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub ReadFromStream(ByRef SourceStream As Stream, Optional ByVal OnlyDecodeMailHeaderInformation As Boolean = False)
        If Not SourceStream Is Nothing Then
            Dim MailData(524286) As Byte            '512KB for buffer
            Dim i As Int32
            'Set file size
            m_Size = SourceStream.Length
            'Set position 0
            SourceStream.Seek(0, SeekOrigin.Begin)
            Do
                i = SourceStream.Read(MailData, 0, MailData.Length)
                If i < MailData.Length Then         'NEED to delete the bytes "0" when buffer is not full
                    ReDim Preserve MailData(i - 1)
                End If
                m_SourceCode &= Encoding.ASCII.GetString(MailData)
            Loop While SourceStream.Position < SourceStream.Length
            SourceStream.Close()

            'Delete the end symbol <CRLF>.<CRLF> if existed
            'Here use <LF>.<CRLF> instead
            m_SourceCode = m_SourceCode.Replace(vbLf & "." & vbCrLf, "")

            Me.DecodeMail(OnlyDecodeMailHeaderInformation)
        Else
            Throw New Exception("Source stream is empty")
        End If
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Save a mail into a memory stream.
    ''' </summary>
    ''' <param name="ForceEncodeMail">Generate the mail codes again before saveing it.</param>
    ''' <returns>An instance of a MemoryStream</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function SaveToStream(ByVal ForceEncodeMail As Boolean) As MemoryStream
        If ForceEncodeMail = True Then
            EncodeMail()
        End If

        Dim mem As New MemoryStream
        mem.Write(Encoding.ASCII.GetBytes(m_SourceCode), 0, Encoding.ASCII.GetByteCount(m_SourceCode))
        Return mem

    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Add custom information in mail header.
    ''' </summary>
    ''' <param name="Key">Field key</param>
    ''' <param name="Value">Field value</param>
    ''' <remarks>
    ''' An 'X-' is added automatically to your given Key as the mail header field key.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub AddCustomHeaderField(ByVal Key As String, ByVal Value As String)
        m_CustomHeader = m_CustomHeader & "X-" & Key & ": " & Value & vbCrLf
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Get the custom information in mail header.
    ''' </summary>
    ''' <param name="Key">Field key</param>
    ''' <returns>Field value</returns>
    ''' <remarks>
    ''' Custom information is start with an 'X-' in mail header.
    ''' </remarks>
    ''' <history>
    ''' 	[Tony]	2005-12-1	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetCustomHeaderField(ByVal Key As String) As String
        Return GetValueString(m_Header, "X-" & Key & ":")
    End Function
End Class
