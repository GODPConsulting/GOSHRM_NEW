Namespace GOSHRM.GOSHRM.BO
    Public Class clsJobGrade
#Region "Private Variables"
        Private _id As String
        Private _grade As String
        Private _probation As Integer
        Private _description As String
        Private _ranks As Integer
        Private _reportto As String
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
        Public Property Grade() As String
            Get
                Return _grade
            End Get
            Set(ByVal value As String)
                _grade = value
            End Set
        End Property
        Public Property Probation() As Integer
            Get
                Return _probation
            End Get
            Set(ByVal value As Integer)
                _probation = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Public Property Ranks() As Integer
            Get
                Return _ranks
            End Get
            Set(ByVal value As Integer)
                _ranks = value
            End Set
        End Property
        Public Property ReportsTo() As String
            Get
                Return _reportto
            End Get
            Set(ByVal value As String)
                _reportto = value
            End Set
        End Property
#End Region
    End Class
End Namespace
