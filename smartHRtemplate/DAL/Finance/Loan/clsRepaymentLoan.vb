Namespace GOSHRM.GOSHRM.BO
    Public Class clsRepaymentLoan
#Region "Private Variables"
        Private _loanrefno As String
        Private _empid As String
        Private _PaymentDate As Date
        Private _Payment As Double
        Private _InterestRate As Double
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
        Public Property PaymentDate() As Date
            Get
                Return _PaymentDate
            End Get
            Set(ByVal value As Date)
                _PaymentDate = value
            End Set
        End Property
        Public Property Payment() As Double
            Get
                Return _Payment
            End Get
            Set(ByVal value As Double)
                _Payment = value
            End Set
        End Property
        Public Property InterestRate() As Double
            Get
                Return _InterestRate
            End Get
            Set(ByVal value As Double)
                _InterestRate = value
            End Set
        End Property
        
#End Region
    End Class
End Namespace
