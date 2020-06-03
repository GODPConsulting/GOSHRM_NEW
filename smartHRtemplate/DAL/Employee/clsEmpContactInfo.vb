Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpContactInfo
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _address1 As String
        Private _address2 As String
        Private _city As String
        Private _country As String
        Private _postaladdr As String
        Private _mobileno As String
        Private _homephone As String
        Private _personalemail As String
        Private _workemail As String
        Private _workphone As String
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
        Public Property Address1() As String
            Get
                Return _address1
            End Get
            Set(ByVal value As String)
                _address1 = value
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
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
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
        Public Property PostalAddress() As String
            Get
                Return _postaladdr
            End Get
            Set(ByVal value As String)
                _postaladdr = value
            End Set
        End Property
        Public Property MobileNo() As String
            Get
                Return _mobileno
            End Get
            Set(ByVal value As String)
                _mobileno = value
            End Set
        End Property
        Public Property HomePhone() As String
            Get
                Return _homephone
            End Get
            Set(ByVal value As String)
                _homephone = value
            End Set
        End Property
        Public Property PersonalEmail() As String
            Get
                Return _personalemail
            End Get
            Set(ByVal value As String)
                _personalemail = value
            End Set
        End Property
        Public Property WorkEmail() As String
            Get
                Return _workemail
            End Get
            Set(ByVal value As String)
                _workemail = value
            End Set
        End Property
        Public Property WorkPhone() As String
            Get
                Return _workphone
            End Get
            Set(ByVal value As String)
                _workphone = value
            End Set
        End Property

#End Region
    End Class
End Namespace
