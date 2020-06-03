Namespace GOSHRM.GOSHRM.BO
    Public Class clsApplyLoan
#Region "Private Variables"
        Private _loanrefno As String
        Private _empid As String
        Private _loantype As String
        Private _loandate As Date
        Private _loanamount As Double
        Private _monthlypay As Double
        Private _currency As String
        Private _annualrate As Double
        Private _IncludeInPaySlip As Boolean
        Private _RepaymentStartDate As Date
        Private _Desc As String
        Private _Level1Approver As String
        Private _Level2Approver As String
        Private _ActionLevel1 As String
        Private _StatusLevel1 As String
        Private _ActionLevel2 As String
        Private _StatusLevel2 As String
#End Region

#Region "Public Properties"
        Public Property LoanRefNo() As String
            Get
                Return _loanrefno
            End Get
            Set(ByVal value As String)
                _loanrefno = value
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
        Public Property LoanType() As String
            Get
                Return _loantype
            End Get
            Set(ByVal value As String)
                _loantype = value
            End Set
        End Property
        Public Property LoanDate() As Date
            Get
                Return _loandate
            End Get
            Set(ByVal value As Date)
                _loandate = value
            End Set
        End Property
        Public Property LoanAmount() As Double
            Get
                Return _loanamount
            End Get
            Set(ByVal value As Double)
                _loanamount = value
            End Set
        End Property
        Public Property RepaymentStartDate() As Date
            Get
                Return _RepaymentStartDate
            End Get
            Set(ByVal value As Date)
                _RepaymentStartDate = value
            End Set
        End Property
        Public Property MonthlyPay() As Double
            Get
                Return _monthlypay
            End Get
            Set(ByVal value As Double)
                _monthlypay = value
            End Set
        End Property
      
        Public Property AnnualRate() As Double
            Get
                Return _annualrate
            End Get
            Set(ByVal value As Double)
                _annualrate = value
            End Set
        End Property
       
        Public Property Description() As String
            Get
                Return _Desc
            End Get
            Set(ByVal value As String)
                _Desc = value
            End Set
        End Property
        Public Property Level1Approver() As String
            Get
                Return _Level1Approver
            End Get
            Set(ByVal value As String)
                _Level1Approver = value
            End Set
        End Property
        
#End Region
    End Class
End Namespace
