Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpSkill
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _skill As String
 
#End Region

#Region "Public Properties"
        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property
        Public Property EmpID() As String
            Get
                Return _empid
            End Get
            Set(ByVal value As String)
                _empid = value
            End Set
        End Property
        Public Property Skill() As String
            Get
                Return _skill
            End Get
            Set(ByVal value As String)
                _skill = value
            End Set
        End Property

#End Region
    End Class
End Namespace
