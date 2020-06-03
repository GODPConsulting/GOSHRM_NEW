Namespace GOSHRM.GOSHRM.BO
    Public Class AppUser
#Region "Private Variables"
        Private _userid As String
        Private _name As String
        Private _role As String
        Private _status As String
        Private _isemployee As String
        Private _email As String
        Private _password As String
        Private _isAdmin As Boolean
        Private _isHR As Boolean
        Private _isFinance As Boolean
        Private _empid As String
        Private _access As String
#End Region

#Region "Public Properties"
        Public Property Userid() As String
            Get
                Return _userid
            End Get
            Set(ByVal value As String)
                _userid = value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Public Property Role() As String
            Get
                Return _role
            End Get
            Set(ByVal value As String)
                _role = value
            End Set
        End Property
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property
        Public Property EMail() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property
        Public Property IsEmployee() As String
            Get
                Return _isemployee
            End Get
            Set(ByVal value As String)
                _isemployee = value
            End Set
        End Property
        'Public Property Password() As String
        '    Get
        '        Return _password
        '    End Get
        '    Set(ByVal value As String)
        '        _password = value
        '    End Set
        'End Property
        Public Property IsSuperAdmin() As Boolean
            Get
                Return _isAdmin
            End Get
            Set(ByVal value As Boolean)
                _isAdmin = value
            End Set
        End Property
        Public Property IsHRManager() As Boolean
            Get
                Return _isHR
            End Get
            Set(ByVal value As Boolean)
                _isHR = value
            End Set
        End Property
        Public Property IsFinance() As Boolean
            Get
                Return _isFinance
            End Get
            Set(ByVal value As Boolean)
                _isFinance = value
            End Set
        End Property
        Public Property EmpID() As String
            Get
                Return _empid
            End Get
            Set(ByVal value As String)
                _empid = value
            End Set
        End Property
        Public Property AccessLevel() As String
            Get
                Return _access
            End Get
            Set(ByVal value As String)
                _access = value
            End Set
        End Property
#End Region
    End Class
End Namespace
