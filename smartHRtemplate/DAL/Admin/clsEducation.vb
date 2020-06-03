Namespace GOSHRM.GOSHRM.BO
    Public Class clsEducation
#Region "Private Variables"
        Private _id As String
        Private _name As String
        Private _level As Integer
        Private _desc As String

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
        
        Public Property Description() As String
            Get
                Return _desc
            End Get
            Set(ByVal value As String)
                _desc = value
            End Set
        End Property
        Public Property Order() As Integer
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
