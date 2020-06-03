Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpCertificate
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _certification As String
        Private _institute As String
        Private _dategranted As String
        Private _expirydate As String
        Private _yeargranted As Integer
        Private _expiryyear As Integer
#End Region

#Region "Public Properties"
        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
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
        Public Property Certification() As String
            Get
                Return _certification
            End Get
            Set(ByVal value As String)
                _certification = value
            End Set
        End Property

        Public Property Institute() As String
            Get
                Return _institute
            End Get
            Set(ByVal value As String)
                _institute = value
            End Set
        End Property
        Public Property DateGranted() As String
            Get
                Return _dategranted
            End Get
            Set(ByVal value As String)
                _dategranted = value
            End Set
        End Property
        Public Property YearGranted() As Integer
            Get
                Return _yeargranted
            End Get
            Set(ByVal value As Integer)
                _yeargranted = value
            End Set
        End Property
        Public Property Expirydate() As String
            Get
                Return _expirydate
            End Get
            Set(ByVal value As String)
                _expirydate = value
            End Set
        End Property
        Public Property ExpiryYear() As Integer
            Get
                Return _expiryyear
            End Get
            Set(ByVal value As Integer)
                _expiryyear = value
            End Set
        End Property
        
#End Region
    End Class
End Namespace
