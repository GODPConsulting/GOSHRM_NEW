Namespace GOSHRM.GOSHRM.BO
    Public Class clsHolidays
#Region "Private Variables"
        Private _id As String
        Private _name As String
        Private _period As String
        Private _date As Date
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
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Public Property Period() As String
            Get
                Return _period
            End Get
            Set(ByVal value As String)
                _period = value
            End Set
        End Property

        Public Property HDate() As Date
            Get
                Return _date
            End Get
            Set(ByVal value As Date)
                _date = value
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
