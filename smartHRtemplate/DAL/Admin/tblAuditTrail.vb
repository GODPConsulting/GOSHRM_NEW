Namespace GOSHRM.GOSHRM.BO
    Public Class tblAuditTrail
#Region "Private Variables"
        Private _username As String
        Private _operation As String
        Private _page As String
        Private _ipaddress As String
        Private _hostname As String
        Private _processtime As DateTime
        Private _oldvalue As String
        Private _newvalue As String
#End Region

#Region "Public Properties"
        Public Property ActionBy() As String
            Get
                Return _username
            End Get
            Set(ByVal value As String)
                _username = value
            End Set
        End Property
        Public Property Action() As String
            Get
                Return _operation
            End Get
            Set(ByVal value As String)
                _operation = value
            End Set
        End Property
        Public Property Page() As String
            Get
                Return _page
            End Get
            Set(ByVal value As String)
                _page = value
            End Set
        End Property
        Public Property IPAddress() As String
            Get
                Return _ipaddress
            End Get
            Set(ByVal value As String)
                _ipaddress = value
            End Set
        End Property
        Public Property HostName() As String
            Get
                Return _hostname
            End Get
            Set(ByVal value As String)
                _hostname = value
            End Set
        End Property
        Public Property ProcessTime() As DateTime
            Get
                Return _processtime
            End Get
            Set(ByVal value As DateTime)
                _processtime = value
            End Set
        End Property
        Public Property OldValue() As String
            Get
                Return _oldvalue
            End Get
            Set(ByVal value As String)
                _oldvalue = value
            End Set
        End Property

        Public Property NewValue() As String
            Get
                Return _newvalue
            End Get
            Set(ByVal value As String)
                _newvalue = value
            End Set
        End Property
#End Region
    End Class
End Namespace
