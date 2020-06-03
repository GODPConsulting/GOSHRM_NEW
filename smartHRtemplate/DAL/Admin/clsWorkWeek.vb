Namespace GOSHRM.GOSHRM.BO
    Public Class clsWorkWeek
#Region "Private Variables"
        Private _id As String
        Private _day As String
        Private _status As String
        Private _country As String
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
        Public Property Day() As String
            Get
                Return _day
            End Get
            Set(ByVal value As String)
                _day = value
            End Set
        End Property
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        Public Property Country() As String
            Get
                Return _country
            End Get
            Set(ByVal value As String)
                _country = value
            End Set
        End Property

#End Region
    End Class
End Namespace
