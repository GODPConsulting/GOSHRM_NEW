Namespace GOSHRM.GOSHRM.BO
    Public Class clsJobPost
#Region "Private Variables"
        Private _id As String
        'Private _jobcode As String
        Private _jobtitle As String
        Private _jobtype As String
        Private _fieldtype As String
        Private _hiringmgr As String
        Private _jobdesc As String
        Private _education As String
        Private _experience As String
        Private _skills As String
        Private _competency As String
        Private _NoPosition As Integer
        Private _country As String
        Private _location As String
        Private _agemin As Integer
        Private _agemax As Integer
        Private _currency As String
        Private _salarymin As Integer
        Private _salarymax As Integer
        Private _status As String
        Private _closingdate As Date
        Private _mail As String
        Private _specialization As String
        Private _minExp As Integer
        Private _maxExp As Integer
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
        'Public Property JobCode() As String
        '    Get
        '        Return _jobcode
        '    End Get
        '    Set(ByVal value As String)
        '        _jobcode = value
        '    End Set
        'End Property
        Public Property JobTitle() As String
            Get
                Return _jobtitle
            End Get
            Set(ByVal value As String)
                _jobtitle = value
            End Set
        End Property
        Public Property JobType() As String
            Get
                Return _jobtype
            End Get
            Set(ByVal value As String)
                _jobtype = value
            End Set
        End Property
        Public Property HiringManager() As String
            Get
                Return _hiringmgr
            End Get
            Set(ByVal value As String)
                _hiringmgr = value
            End Set
        End Property
        Public Property JobDescription() As String
            Get
                Return _jobdesc
            End Get
            Set(ByVal value As String)
                _jobdesc = value
            End Set
        End Property
        Public Property EducationLevel() As String
            Get
                Return _education
            End Get
            Set(ByVal value As String)
                _education = value
            End Set
        End Property
        Public Property ExperienceLevel() As String
            Get
                Return _experience
            End Get
            Set(ByVal value As String)
                _experience = value
            End Set
        End Property
        Public Property Skills() As String
            Get
                Return _skills
            End Get
            Set(ByVal value As String)
                _skills = value
            End Set
        End Property
        Public Property NoOfPosition() As Integer
            Get
                Return _NoPosition
            End Get
            Set(ByVal value As Integer)
                _NoPosition = value
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
        Public Property Location() As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property
        Public Property MinAge() As Integer
            Get
                Return _agemin
            End Get
            Set(ByVal value As Integer)
                _agemin = value
            End Set
        End Property
        Public Property MaxAge() As Integer
            Get
                Return _agemax
            End Get
            Set(ByVal value As Integer)
                _agemax = value
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
        Public Property MinSalary() As Double
            Get
                Return _salarymin
            End Get
            Set(ByVal value As Double)
                _salarymin = value
            End Set
        End Property
        Public Property MaxSalary() As Double
            Get
                Return _salarymax
            End Get
            Set(ByVal value As Double)
                _salarymax = value
            End Set
        End Property
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property
        Public Property ClosingDate() As Date
            Get
                Return _closingdate
            End Get
            Set(ByVal value As Date)
                _closingdate = value
            End Set
        End Property
        Public Property SendMail() As String
            Get
                Return _mail
            End Get
            Set(ByVal value As String)
                _mail = value
            End Set
        End Property
        Public Property Specialisation() As String
            Get
                Return _specialization
            End Get
            Set(ByVal value As String)
                _specialization = value
            End Set
        End Property
        Public Property MinYearsOfExperience() As Integer
            Get
                Return _minExp
            End Get
            Set(ByVal value As Integer)
                _minExp = value
            End Set
        End Property
        Public Property MaxYearsOfExperience() As Integer
            Get
                Return _maxExp
            End Get
            Set(ByVal value As Integer)
                _maxExp = value
            End Set
        End Property
#End Region
    End Class
End Namespace
