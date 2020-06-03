Namespace GOSHRM.GOSHRM.BO
    Public Class clsProject
#Region "Private Variables"
        Private _id As String
        Private _name As String
        Private _client As String
        Private _teamlead As String
        Private _projectmanager As String
        Private _status As String
        Private _detail As String
        Private _startdate As Date
        Private _expectenddate As Date
        Private _enddate As Date
#End Region

#Region "Public Properties"
        Public Property id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
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
        Public Property Client() As String
            Get
                Return _client
            End Get
            Set(ByVal value As String)
                _client = value
            End Set
        End Property
        Public Property ProjectManager() As String
            Get
                Return _projectmanager
            End Get
            Set(ByVal value As String)
                _projectmanager = value
            End Set
        End Property
        Public Property TeamLead() As String
            Get
                Return _teamlead
            End Get
            Set(ByVal value As String)
                _teamlead = value
            End Set
        End Property
        Public Property Detail() As String
            Get
                Return _detail
            End Get
            Set(ByVal value As String)
                _detail = value
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
        Public Property StartDate() As Date
            Get
                Return _startdate
            End Get
            Set(ByVal value As Date)
                _startdate = value
            End Set
        End Property
        Public Property ExpectedEndDate() As Date
            Get
                Return _expectenddate
            End Get
            Set(ByVal value As Date)
                _expectenddate = value
            End Set
        End Property
        Public Property EndDate() As Date
            Get
                Return _enddate
            End Get
            Set(ByVal value As Date)
                _enddate = value
            End Set
        End Property
#End Region
    End Class
End Namespace
