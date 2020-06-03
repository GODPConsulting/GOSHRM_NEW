Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpDependants
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _name As String
        Private _relationship As String
        Private _dob As Date
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
        Public Property DependantName() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Public Property Relationship() As String
            Get
                Return _relationship
            End Get
            Set(ByVal value As String)
                _relationship = value
            End Set
        End Property
        Public Property DateOfBirth() As Date
            Get
                Return _dob
            End Get
            Set(ByVal value As Date)
                _dob = value
            End Set
        End Property
        
#End Region
    End Class
End Namespace
