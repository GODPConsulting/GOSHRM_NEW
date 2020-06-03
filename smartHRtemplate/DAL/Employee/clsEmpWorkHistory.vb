Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpWorkHistory
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _grade As String
        Private _jobtitle As String
        Private _supervisor As String
        Private _supervisor2 As String
        Private _coach As String
        Private _officelevel As String
        Private _office As String
        Private _jobstatus As String
        Private _country As String
        Private _location As String
        Private _startdate As String
        Private _startyear As Integer
        Private _enddate As String
        Private _endyear As Integer
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
        Public Property JobGrade() As String
            Get
                Return _grade
            End Get
            Set(ByVal value As String)
                _grade = value
            End Set
        End Property

        Public Property JobTitle() As String
            Get
                Return _jobtitle
            End Get
            Set(ByVal value As String)
                _jobtitle = value
            End Set
        End Property
        Public Property Supervisor() As String
            Get
                Return _supervisor
            End Get
            Set(ByVal value As String)
                _supervisor = value
            End Set
        End Property
        Public Property SupervisorII() As String
            Get
                Return _supervisor2
            End Get
            Set(ByVal value As String)
                _supervisor2 = value
            End Set
        End Property
        Public Property Coach() As String
            Get
                Return _coach
            End Get
            Set(ByVal value As String)
                _coach = value
            End Set
        End Property
        Public Property OfficeLevel() As String
            Get
                Return _officelevel
            End Get
            Set(ByVal value As String)
                _officelevel = value
            End Set
        End Property
        Public Property Office() As String
            Get
                Return _office
            End Get
            Set(ByVal value As String)
                _office = value
            End Set
        End Property
        Public Property JobStatus() As String
            Get
                Return _jobstatus
            End Get
            Set(ByVal value As String)
                _jobstatus = value
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
    
        Public Property StartDate() As String
            Get
                Return _startdate
            End Get
            Set(ByVal value As String)
                _startdate = value
            End Set
        End Property
        Public Property StartYear() As Integer
            Get
                Return _startyear
            End Get
            Set(ByVal value As Integer)
                _startyear = value
            End Set
        End Property
        Public Property EndDate() As String
            Get
                Return _enddate
            End Get
            Set(ByVal value As String)
                _enddate = value
            End Set
        End Property
        Public Property EndYear() As Integer
            Get
                Return _endyear
            End Get
            Set(ByVal value As Integer)
                _endyear = value
            End Set
        End Property
#End Region
    End Class
End Namespace
