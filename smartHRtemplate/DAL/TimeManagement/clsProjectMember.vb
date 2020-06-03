Namespace GOSHRM.GOSHRM.BO
    Public Class clsProjectMember
#Region "Private Variables"
        Private _project As String
        Private _client As String
        Private _member As String
#End Region

#Region "Public Properties"
        Public Property Project() As String
            Get
                Return _project
            End Get
            Set(ByVal value As String)
                _project = value
            End Set
        End Property
        Public Property Client() As String
            Get
                Return _client
            End Get
            Set(ByVal value As String)
                _client = value
            End Set
        End Property

        Public Property Member() As String
            Get
                Return _member
            End Get
            Set(ByVal value As String)
                _member = value
            End Set
        End Property
#End Region
    End Class
End Namespace
