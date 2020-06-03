Namespace GOSHRM.GOSHRM.BO
    Public Class clsPayrollOption
#Region "Private Variables"
        Private _company As String
        Private _autoapprove As String
        Private _autoemailpayslip As String
        Private _perdaysalaryadjustment As String
        Private _currency As String
        Private _payslipapprovers As Integer
        Private _amount As Double
        Private _salarybasedattendance As String
        Private _payovertime As String
        Private _overtimeindex As Double
#End Region

#Region "Public Properties"
        Public Property Company() As String
            Get
                Return _company

            End Get
            Set(ByVal value As String)
                _company = value
            End Set
        End Property
        Public Property AutoApprovePayslip() As String
            Get
                Return _autoapprove

            End Get
            Set(ByVal value As String)
                _autoapprove = value
            End Set
        End Property
        Public Property AutoMailPayslip() As String
            Get
                Return _autoemailpayslip
            End Get
            Set(ByVal value As String)
                _autoemailpayslip = value
            End Set
        End Property

        Public Property PerDaySalaryAdjustment() As String
            Get
                Return _perdaysalaryadjustment
            End Get
            Set(ByVal value As String)
                _perdaysalaryadjustment = value
            End Set
        End Property

        Public Property Currency() As String
            Get
                Return _currency
            End Get
            Set(ByVal value As String)
                _currency = value
            End Set
        End Property
        Public Property PayslipApprovers() As Integer
            Get
                Return _payslipapprovers
            End Get
            Set(ByVal value As Integer)
                _payslipapprovers = value
            End Set
        End Property
        Public Property MinAmountForApproval() As Double
            Get
                Return _amount
            End Get
            Set(ByVal value As Double)
                _amount = value
            End Set
        End Property
        Public Property SalaryBasedOnAttendance() As String
            Get
                Return _salarybasedattendance
            End Get
            Set(ByVal value As String)
                _salarybasedattendance = value
            End Set
        End Property
        Public Property PayOvertime() As String
            Get
                Return _payovertime
            End Get
            Set(ByVal value As String)
                _payovertime = value
            End Set
        End Property
        Public Property OvertimeIndex() As Double
            Get
                Return _overtimeindex
            End Get
            Set(ByVal value As Double)
                _overtimeindex = value
            End Set
        End Property
#End Region
    End Class
End Namespace
