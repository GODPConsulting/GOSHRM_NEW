Namespace GOSHRM.GOSHRM.BO
    Public Class clsCourse
#Region "Private Variables"
        Private _id As String
        Private _code As String
        Private _name As String
        Private _objective As String
        Private _status As String
        Private _category As String
        Private _currency As String
        Private _cost As Double
        'Private _coordinator As String
        'Private _trainer As String
        'Private _trainerdetail As String
        'Private _paymenttype As String
        'Private _currency As String
        'Private _cost As Double
        'Private _status As String
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
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
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
        Public Property Objective() As String
            Get
                Return _objective
            End Get
            Set(ByVal value As String)
                _objective = value
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
        Public Property Cost() As Double
            Get
                Return _cost
            End Get
            Set(ByVal value As Double)
                _cost = value
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

#End Region
    End Class
End Namespace
