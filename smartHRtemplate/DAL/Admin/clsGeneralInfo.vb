Namespace GOSHRM.GOSHRM.BO
    Public Class clsGeneralInfo
#Region "Private Variables"
        Private _organisation As String
        Private _taxid As String
        Private _regno As String
        Private _phone As String
        Private _email As String
        Private _fax As String
        Private _zip As String
        Private _address1 As String
        Private _address2 As String
        Private _city As String
        Private _state As String
        Private _country As String
        Private _note As String
        Private _currency As String
        Private _level As Integer
#End Region

#Region "Public Properties"
        Public Property Organisation() As String
            Get
                Return _organisation
            End Get
            Set(ByVal value As String)
                _organisation = value
            End Set
        End Property
        Public Property TaxID() As String
            Get
                Return _taxid
            End Get
            Set(ByVal value As String)
                _taxid = value
            End Set
        End Property

        Public Property RegistrationNo() As String
            Get
                Return _regno
            End Get
            Set(ByVal value As String)
                _regno = value
            End Set
        End Property
        Public Property Phone() As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property
        Public Property Fax() As String
            Get
                Return _fax
            End Get
            Set(ByVal value As String)
                _fax = value
            End Set
        End Property
        Public Property ZipPostCode() As String
            Get
                Return _zip
            End Get
            Set(ByVal value As String)
                _zip = value
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
        Public Property State() As String
            Get
                Return _state
            End Get
            Set(ByVal value As String)
                _state = value
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
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
            End Set
        End Property

        Public Property Currency() As String
            Get
                Return _currency
            End Get
            Set(ByVal value As String)
                _currency = value
            End Set
        End Property
        Public Property SubsidiaryLevel() As Integer
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
