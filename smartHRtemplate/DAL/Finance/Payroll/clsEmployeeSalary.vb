Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmployeeSalary
#Region "Private Variables"
        Private _Employee As String
        Private _SalaryItem As String
        Private _Amount As Double
#End Region

#Region "Public Properties"
        Public Property Employee() As String
            Get
                Return _Employee
            End Get
            Set(ByVal value As String)
                _Employee = value
            End Set
        End Property
        Public Property SalaryItem() As String
            Get
                Return _SalaryItem
            End Get
            Set(ByVal value As String)
                _SalaryItem = value
            End Set
        End Property
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property

#End Region
    End Class
End Namespace
