Namespace GOSHRM.GOSHRM.BO
    Public Class clsStructureDefn
#Region "Private Variables"
        Private _id As String
        Private _defn As String
        Private _desc As String
        Private _level As Integer
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
        Public Property Definition() As String
            Get
                Return _defn
            End Get
            Set(ByVal value As String)
                _defn = value
            End Set
        End Property
        Public Property Desc() As String
            Get
                Return _desc
            End Get
            Set(ByVal value As String)
                _desc = value
            End Set
        End Property
        Public Property Level() As Integer
            Get
                Return _level
            End Get
            Set(ByVal value As Integer)
                _level = value
            End Set
        End Property
#End Region
    End Class
End Namespace
