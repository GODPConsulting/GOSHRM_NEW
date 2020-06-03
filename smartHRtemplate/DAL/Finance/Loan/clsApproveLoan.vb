Namespace GOSHRM.GOSHRM.BO
    Public Class clsApproveLoan
#Region "Private Variables"
        Private _refno As String
        Private _action As String
        Private _status As String
        Private _status2 As String
        Private _amount As Double
        Private _interestrate As Double
        Private _marketrate As Double

#End Region

#Region "Public Properties"
        Public Property LoanRefNo() As String
            Get
                Return _refno
            End Get
            Set(ByVal value As String)
                _refno = value
            End Set
        End Property
        Public Property Level1_Approval_Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        Public Property Finance_Approval_Status() As String
            Get
                Return _status2
            End Get
            Set(ByVal value As String)
                _status2 = value
            End Set
        End Property
        Public Property LoanAmount() As Double
            Get
                Return _amount
            End Get
            Set(ByVal value As Double)
                _amount = value
            End Set
        End Property
        Public Property InterestRate() As Double
            Get
                Return _interestrate
            End Get
            Set(ByVal value As Double)
                _interestrate = value
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
#End Region
    End Class
End Namespace
