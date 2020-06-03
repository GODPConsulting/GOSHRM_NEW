Namespace GOSHRM.GOSHRM.BO
    Public Class clsLeavePeriod
#Region "Private Variables"
        Private _id As String
        Private _name As String
        Private _periodstart As Date
        Private _periodend As Date
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
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Public Property PeriodStart() As Date
            Get
                Return _periodstart
            End Get
            Set(ByVal value As Date)
                _periodstart = value
            End Set
        End Property

        Public Property PeriodEnd() As Date
            Get
                Return _periodend
            End Get
            Set(ByVal value As Date)
                _periodend = value
            End Set
        End Property

#End Region
    End Class
End Namespace
