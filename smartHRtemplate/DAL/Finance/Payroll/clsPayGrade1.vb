Namespace GOSHRM.GOSHRM.BO
    Public Class clsPayGrade1
#Region "Private Variables"
        Private _Grade As String
        Private _SalaryItem As String
        Private _Amount As Double
#End Region

#Region "Public Properties"
        Public Property Grade() As String
            Get
                Return _Grade
            End Get
            Set(ByVal value As String)
                _Grade = value
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
