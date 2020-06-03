Namespace GOSHRM.GOSHRM.BO
    Public Class clsLeaveType
#Region "Private Variables"
        Private _id As String
        Private _name As String
        Private _leavePerYear As Integer
        Private _empcanapply As String
        Private _mailnotification As String
        Private _leaveaccrued As String
        Private _leavecarriedforward As String
        Private _pcarriedforward As Integer
        Private _availabilityperiod As Integer
        Private _gender As String
        Private _probation As String
        Private _payable As String
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
        Public Property LeavePerYear() As Integer
            Get
                Return _leavePerYear
            End Get
            Set(ByVal value As Integer)
                _leavePerYear = value
            End Set
        End Property
        Public Property EmployeeCanApplyForLeave() As String
            Get
                Return _empcanapply
            End Get
            Set(ByVal value As String)
                _empcanapply = value
            End Set
        End Property
        Public Property SendMailNotification() As String
            Get
                Return _mailnotification
            End Get
            Set(ByVal value As String)
                _mailnotification = value
            End Set
        End Property
        Public Property LeaveAccrued() As String
            Get
                Return _leaveaccrued
            End Get
            Set(ByVal value As String)
                _leaveaccrued = value
            End Set
        End Property
        Public Property LeaveCarriedForward() As String
            Get
                Return _leavecarriedforward
            End Get
            Set(ByVal value As String)
                _leavecarriedforward = value
            End Set
        End Property

        Public Property PercentageOfLeaveCarriedForward() As Integer
            Get
                Return _pcarriedforward
            End Get
            Set(ByVal value As Integer)
                _pcarriedforward = value
            End Set
        End Property
        Public Property CarriedForwardLeaveAvailabilityPeriod() As Integer
            Get
                Return _availabilityperiod
            End Get
            Set(ByVal value As Integer)
                _availabilityperiod = value
            End Set
        End Property
        Public Property Gender() As String
            Get
                Return _gender
            End Get
            Set(ByVal value As String)
                _gender = value
            End Set
        End Property
        Public Property EligibleAtProbation() As String
            Get
                Return _probation
            End Get
            Set(ByVal value As String)
                _probation = value
            End Set
        End Property
        Public Property AllowancePayable() As String
            Get
                Return _payable
            End Get
            Set(ByVal value As String)
                _payable = value
            End Set
        End Property
#End Region
    End Class
End Namespace
