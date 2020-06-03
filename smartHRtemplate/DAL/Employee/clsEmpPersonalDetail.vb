Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpPersonalDetail
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _firstname As String
        Private _middlename As String
        Private _lastname As String
        Private _gender As String
        Private _maritalstatus As String
        Private _nationality As String
        Private _dob As Date
        Private _bloodgrp As String
        Private _stateoforigin As String
        Private _idmethod As String
        Private _idno As String
        Private _idexpirydate As Date
        Private _idissuer As String
        Private _countryofbirth As String
        Private _placeofbirth As String
        Private _hobbies As String
        Private _datejoin As Date
        Private _confirmdate As Date
        Private _terminatedate As Date
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
        Public Property FirstName() As String
            Get
                Return _firstname
            End Get
            Set(ByVal value As String)
                _firstname = value
            End Set
        End Property
        Public Property MiddleName() As String
            Get
                Return _middlename
            End Get
            Set(ByVal value As String)
                _middlename = value
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return _lastname
            End Get
            Set(ByVal value As String)
                _lastname = value
            End Set
        End Property
        Public Property Gender() As String
            Get
                Return _gender
            End Get
            Set(ByVal value As String)
                _gender = value
            End Set
        End Property
        Public Property MaritalStatus() As String
            Get
                Return _maritalstatus
            End Get
            Set(ByVal value As String)
                _maritalstatus = value
            End Set
        End Property
        Public Property Nationality() As String
            Get
                Return _nationality
            End Get
            Set(ByVal value As String)
                _nationality = value
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
        Public Property BloodGroup() As String
            Get
                Return _bloodgrp
            End Get
            Set(ByVal value As String)
                _bloodgrp = value
            End Set
        End Property
        Public Property StateOfOrigin() As String
            Get
                Return _stateoforigin
            End Get
            Set(ByVal value As String)
                _stateoforigin = value
            End Set
        End Property
        Public Property IdentityMethod() As String
            Get
                Return _idmethod
            End Get
            Set(ByVal value As String)
                _idmethod = value
            End Set
        End Property
        Public Property IDNo() As String
            Get
                Return _idno
            End Get
            Set(ByVal value As String)
                _idno = value
            End Set
        End Property
        Public Property IDExpiryDate() As Date
            Get
                Return _idexpirydate
            End Get
            Set(ByVal value As Date)
                _idexpirydate = value
            End Set
        End Property
        Public Property IDIssuer() As String
            Get
                Return _idissuer
            End Get
            Set(ByVal value As String)
                _idissuer = value
            End Set
        End Property
        Public Property CountryOfBirth() As String
            Get
                Return _countryofbirth
            End Get
            Set(ByVal value As String)
                _countryofbirth = value
            End Set
        End Property
        Public Property PlaceOfBirth() As String
            Get
                Return _placeofbirth
            End Get
            Set(ByVal value As String)
                _placeofbirth = value
            End Set
        End Property
        Public Property Hobbies() As String
            Get
                Return _hobbies
            End Get
            Set(ByVal value As String)
                _hobbies = value
            End Set
        End Property
        Public Property DateJoin() As Date
            Get
                Return _datejoin
            End Get
            Set(ByVal value As Date)
                _datejoin = value
            End Set
        End Property
        'Public Property ConfirmationDate() As Date
        '    Get
        '        Return _confirmdate
        '    End Get
        '    Set(ByVal value As Date)
        '        _confirmdate = value
        '    End Set
        'End Property
        Public Property TerminationDate() As Date
            Get
                Return _terminatedate
            End Get
            Set(ByVal value As Date)
                _terminatedate = value
            End Set
        End Property
#End Region
    End Class
End Namespace
