Namespace GOSHRM.GOSHRM.BO
    Public Class clsMonthlyStructure
#Region "Private Variables"
        Private _id As String
        Private _item As String
        Private _taxable As String
        Private _itemtype As String
        Private _figure As Double
        Private _active As Boolean
        Private _order As Integer
        Private _attend As String
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
        Public Property PaySlipItem() As String
            Get
                Return _item
            End Get
            Set(ByVal value As String)
                _item = value
            End Set
        End Property
        Public Property IsTaxable() As String
            Get
                Return _taxable
            End Get
            Set(ByVal value As String)
                _taxable = value
            End Set
        End Property
        Public Property ItemType() As String
            Get
                Return _itemtype
            End Get
            Set(ByVal value As String)
                _itemtype = value
            End Set
        End Property
        Public Property Figure() As Double
            Get
                Return _figure
            End Get
            Set(ByVal value As Double)
                _figure = value
            End Set
        End Property
        Public Property Order() As Integer
            Get
                Return _order
            End Get
            Set(ByVal value As Integer)
                _order = value
            End Set
        End Property
        Public Property Active() As Boolean
            Get
                Return _active
            End Get
            Set(ByVal value As Boolean)
                _active = value
            End Set
        End Property
        Public Property AttendanceBased() As String
            Get
                Return _attend
            End Get
            Set(ByVal value As String)
                _attend = value
            End Set
        End Property
#End Region
    End Class
End Namespace
