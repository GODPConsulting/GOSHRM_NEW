Namespace GOSHRM.GOSHRM.BO
    Public Class clsPayGrade
#Region "Private Variables"
        Private _id As String
        Private _grade As String
        Private _currency As String
        Private _min As Double
        Private _max As Double
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
        Public Property Currency() As String
            Get
                Return _currency
            End Get
            Set(ByVal value As String)
                _currency = value
            End Set
        End Property
        Public Property Min() As Double
            Get
                Return _min
            End Get
            Set(ByVal value As Double)
                _min = value
            End Set
        End Property
        Public Property Max() As Double
            Get
                Return _max
            End Get
            Set(ByVal value As Double)
                _max = value
            End Set
        End Property
#End Region
    End Class
End Namespace
