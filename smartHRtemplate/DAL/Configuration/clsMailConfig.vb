Namespace GOSHRM.GOSHRM.BO
    Public Class clsMailConfig
#Region "Private Variables"
        Private _senderemail As String
        Private _sendername As String
        Private _smtphost As String
        Private _smtpport As Integer
        Private _useauthen As String
        Private _smtpuser As String
        Private _password As String
        Private _type As String

#End Region

#Region "Public Properties"
        Public Property SenderEmail() As String
            Get
                Return _senderemail

            End Get
            Set(ByVal value As String)
                _senderemail = value
            End Set
        End Property
        Public Property SenderName() As String
            Get
                Return _sendername
            End Get
            Set(ByVal value As String)
                _sendername = value
            End Set
        End Property

        Public Property SMTPHost() As String
            Get
                Return _smtphost
            End Get
            Set(ByVal value As String)
                _smtphost = value
            End Set
        End Property

        Public Property SMTPPort() As Integer
            Get
                Return _smtpport
            End Get
            Set(ByVal value As Integer)
                _smtpport = value
            End Set
        End Property
        Public Property UseSMTPAuthentication() As String
            Get
                Return _useauthen
            End Get
            Set(ByVal value As String)
                _useauthen = value
            End Set
        End Property

        Public Property SMTPUser() As String
            Get
                Return _smtpuser
            End Get
            Set(ByVal value As String)
                _smtpuser = value
            End Set
        End Property

        Public Property SMTPPassword() As String
            Get
                Return _password
            End Get
            Set(ByVal value As String)
                _password = value
            End Set
        End Property

        Public Property ConnectionType() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property

#End Region
    End Class
End Namespace
