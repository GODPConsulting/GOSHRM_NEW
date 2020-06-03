Namespace GOSHRM.GOSHRM.BO
    Public Class clsClient
#Region "Private Variables"
        Private _id As String
        Private _name As String

        Private _contactno As String
        Private _contactemail As String
        Private _companyurl As String
        Private _address As String
        Private _detail As String
        Private _contactdate As Date

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
        Public Property ContactNo() As String
            Get
                Return _contactno
            End Get
            Set(ByVal value As String)
                _contactno = value
            End Set
        End Property
        Public Property ContactEmail() As String
            Get
                Return _contactemail
            End Get
            Set(ByVal value As String)
                _contactemail = value
            End Set
        End Property
        Public Property CompanyURL() As String
            Get
                Return _companyurl
            End Get
            Set(ByVal value As String)
                _companyurl = value
            End Set
        End Property
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property
        Public Property Detail() As String
            Get
                Return _detail
            End Get
            Set(ByVal value As String)
                _detail = value
            End Set
        End Property
        Public Property FirstContactDate() As Date
            Get
                Return _contactdate
            End Get
            Set(ByVal value As Date)
                _contactdate = value
            End Set
        End Property
#End Region
    End Class
End Namespace
