Namespace GOSHRM.GOSHRM.BO
    Public Class clsPrivilege
#Region "Private Variables"
        Private _create As String
        Private _update As String
        Private _delete As String
        Private _read As String
#End Region

#Region "Public Properties"
        Public Property Read() As String
            Get
                Return _read
            End Get
            Set(ByVal value As String)
                _read = value
            End Set
        End Property
        Public Property Create() As String
            Get
                Return _create
            End Get
            Set(ByVal value As String)
                _create = value
            End Set
        End Property
        Public Property Update() As String
            Get
                Return _update
            End Get
            Set(ByVal value As String)
                _update = value
            End Set
        End Property
        Public Property Delete() As String
            Get
                Return _delete
            End Get
            Set(ByVal value As String)
                _delete = value
            End Set
        End Property
#End Region
    End Class

End Namespace

