Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpEducation
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _qualification As String
        Private _institute As String
        Private _startdate As String
        Private _startyear As Integer
        Private _completedon As String
        Private _yearcomplete As Integer

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
        Public Property Qualification() As String
            Get
                Return _qualification
            End Get
            Set(ByVal value As String)
                _qualification = value
            End Set
        End Property

        Public Property Institute() As String
            Get
                Return _institute
            End Get
            Set(ByVal value As String)
                _institute = value
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
        Public Property CompletedOn() As String
            Get
                Return _completedon
            End Get
            Set(ByVal value As String)
                _completedon = value
            End Set
        End Property
        Public Property YearCompleted() As Integer
            Get
                Return _yearcomplete
            End Get
            Set(ByVal value As Integer)
                _yearcomplete = value
            End Set
        End Property
#End Region
    End Class
End Namespace
