Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpStatus
#Region "Private Variables"
        Private _id As String
        Private _name As String
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
        Public Property Desc() As String
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
