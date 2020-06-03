Namespace GOSHRM.GOSHRM.BO
    Public Class clsLeaveAllowanceEmployee
#Region "Private Variables"
        Private _id As String
        Private _emp As String
        Private _contribution As Double
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
        Public Property EmpID() As String
            Get
                Return _emp
            End Get
            Set(ByVal value As String)
                _emp = value
            End Set
        End Property
        Public Property Contribution() As Double
            Get
                Return _contribution
            End Get
            Set(ByVal value As Double)
                _contribution = value
            End Set
        End Property

#End Region
    End Class
End Namespace
