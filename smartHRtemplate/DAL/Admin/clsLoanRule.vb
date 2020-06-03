Namespace GOSHRM.GOSHRM.BO
    Public Class clsLoanRule
#Region "Private Variables"
        Private _id As String
        Private _loantype As String
        Private _jobgrade As String
        Private _status As String
        Private _rulename As String
        Private _rate As Double
        Private _marketrate As Double
        Private _eir As Double
        Private _amount As Double

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
        Public Property RuleName() As String
            Get
                Return _rulename
            End Get
            Set(ByVal value As String)
                _rulename = value
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
      
        Public Property EmploymentStatus() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property
        Public Property InterestRate() As Double
            Get
                Return _rate
            End Get
            Set(ByVal value As Double)
                _rate = value
            End Set
        End Property
        Public Property MarketRate() As Double
            Get
                Return _marketrate
            End Get
            Set(ByVal value As Double)
                _marketrate = value
            End Set
        End Property
       
        Public Property Amount() As Double
            Get
                Return _amount
            End Get
            Set(ByVal value As Double)
                _amount = value
            End Set
        End Property
#End Region
    End Class
End Namespace
