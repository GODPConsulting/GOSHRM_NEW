Namespace GOSHRM.GOSHRM.BO
    Public Class clsCompanyStructure
#Region "Private Variables"
        Private _id As String
        Private _name As String
        Private _type As String
        Private _head As String
        Private _country As String
        Private _parent As String
        Private _address As String
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
        Public Property Type() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property
        Public Property Head() As String
            Get
                Return _head
            End Get
            Set(ByVal value As String)
                _head = value
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
        Public Property Parent() As String
            Get
                Return _parent
            End Get
            Set(ByVal value As String)
                _parent = value
            End Set
        End Property
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

#End Region
    End Class
End Namespace
