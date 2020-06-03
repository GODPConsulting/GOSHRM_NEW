Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpEmergency
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _name1 As String
        Private _address1 As String
        Private _phone1 As String
        Private _relationship1 As String
        Private _name2 As String
        Private _address2 As String
        Private _phone2 As String
        Private _relationship2 As String
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
        Public Property Name1() As String
            Get
                Return _name1
            End Get
            Set(ByVal value As String)
                _name1 = value
            End Set
        End Property
        Public Property Address1() As String
            Get
                Return _address1
            End Get
            Set(ByVal value As String)
                _address1 = value
            End Set
        End Property
        Public Property Phone1() As String
            Get
                Return _phone1
            End Get
            Set(ByVal value As String)
                _phone1 = value
            End Set
        End Property
       
        Public Property RelationShip1() As String
            Get
                Return _relationship1
            End Get
            Set(ByVal value As String)
                _relationship1 = value
            End Set
        End Property
        Public Property Name2() As String
            Get
                Return _name2
            End Get
            Set(ByVal value As String)
                _name2 = value
            End Set
        End Property
        Public Property Address2() As String
            Get
                Return _address2
            End Get
            Set(ByVal value As String)
                _address2 = value
            End Set
        End Property
        Public Property Phone2() As String
            Get
                Return _phone2
            End Get
            Set(ByVal value As String)
                _phone2 = value
            End Set
        End Property

        Public Property RelationShip2() As String
            Get
                Return _relationship2
            End Get
            Set(ByVal value As String)
                _relationship2 = value
            End Set
        End Property

#End Region
    End Class
End Namespace
