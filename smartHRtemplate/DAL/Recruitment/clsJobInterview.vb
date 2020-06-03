Namespace GOSHRM.GOSHRM.BO
    Public Class clsJobInterview
#Region "Private Variables"
        Private _id As String
        Private _jobtitle As String
        Private _intdate As Date
        Private _intTime As String
        Private _venue As String
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

        Public Property JobPost() As String
            Get
                Return _jobtitle
            End Get
            Set(ByVal value As String)
                _jobtitle = value
            End Set
        End Property

        Public Property InterviewDate() As Date
            Get
                Return _intdate
            End Get
            Set(ByVal value As Date)
                _intdate = value
            End Set
        End Property
        Public Property InterviewTime() As String
            Get
                Return _intTime
            End Get
            Set(ByVal value As String)
                _intTime = value
            End Set
        End Property
        Public Property Venue() As String
            Get
                Return _venue
            End Get
            Set(ByVal value As String)
                _venue = value
            End Set
        End Property
#End Region
    End Class
End Namespace
