Namespace GOSHRM.GOSHRM.BO
    Public Class clsLeaveRule
#Region "Private Variables"
        Private _id As String
        Private _leavetype As String
        Private _grade As String
        Private _empstatus As String
        Private _leavePerYear As Integer
        Private _empcanapply As String
        Private _leaveaccrued As String
        Private _leavecarriedforward As String
        Private _pcarriedforward As Integer
        Private _availabilityperiod As Integer
        Private _minservice As Integer
        Private _maxservice As Integer
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
        Public Property LeaveType() As String
            Get
                Return _leavetype
            End Get
            Set(ByVal value As String)
                _leavetype = value
            End Set
        End Property
        Public Property Grade() As String
            Get
                Return _grade
            End Get
            Set(ByVal value As String)
                _grade = value
            End Set
        End Property
        Public Property EmploymentStatus() As String
            Get
                Return _empstatus
            End Get
            Set(ByVal value As String)
                _empstatus = value
            End Set
        End Property
        Public Property LeavePerYear() As Integer
            Get
                Return _leavePerYear
            End Get
            Set(ByVal value As Integer)
                _leavePerYear = value
            End Set
        End Property
        Public Property MinMthService() As Integer
            Get
                Return _minservice
            End Get
            Set(ByVal value As Integer)
                _minservice = value
            End Set
        End Property
        Public Property MaxMthService() As Integer
            Get
                Return _maxservice
            End Get
            Set(ByVal value As Integer)
                _maxservice = value
            End Set
        End Property
#End Region
    End Class
End Namespace
