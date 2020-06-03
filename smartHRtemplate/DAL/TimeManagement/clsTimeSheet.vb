Namespace GOSHRM.GOSHRM.BO
    Public Class clsTimeSheet
#Region "Private Variables"
        Private _id As String
        Private _empid As String
        Private _project As String
        Private _activity As String
        Private _activitydate As Date
        Private _activityenddate As Date
        Private _starttime As String
        Private _endtime As String
        Private _note As String
        Private _duration As Double
        Private _status As String
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

        Public Property Employee() As String
            Get
                Return _empid
            End Get
            Set(ByVal value As String)
                _empid = value
            End Set
        End Property
        Public Property Project() As String
            Get
                Return _project
            End Get
            Set(ByVal value As String)
                _project = value
            End Set
        End Property
        Public Property Activity() As String
            Get
                Return _activity
            End Get
            Set(ByVal value As String)
                _activity = value
            End Set
        End Property
        Public Property ActivityDate() As Date
            Get
                Return _activitydate
            End Get
            Set(ByVal value As Date)
                _activitydate = value
            End Set
        End Property
        Public Property ActivityEndDate() As Date
            Get
                Return _activityenddate
            End Get
            Set(ByVal value As Date)
                _activityenddate = value
            End Set
        End Property
        Public Property StartTime() As String
            Get
                Return _starttime
            End Get
            Set(ByVal value As String)
                _starttime = value
            End Set
        End Property
        Public Property EndTime() As String
            Get
                Return _endtime
            End Get
            Set(ByVal value As String)
                _endtime = value
            End Set
        End Property
        Public Property Duration() As Double
            Get
                Return _duration
            End Get
            Set(ByVal value As Double)
                _duration = value
            End Set
        End Property
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
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
#End Region
    End Class
End Namespace
