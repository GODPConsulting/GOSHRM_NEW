Namespace GOSHRM.GOSHRM.BO
    Public Class clsRole
#Region "Private Variables"
        Private _role As String
        Private _roletype As String
        Private _desc As String
#End Region

#Region "Public Properties"
        Public Property Role() As String
            Get
                Return _role
            End Get
            Set(ByVal value As String)
                _role = value
            End Set
        End Property
        Public Property RoleType() As String
            Get
                Return _roletype
            End Get
            Set(ByVal value As String)
                _roletype = value
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
#End Region
    End Class
End Namespace
