Namespace GOSHRM.GOSHRM.BO
    Public Class clsAppraisalCycle
#Region "Private Variables"
        Private _id As String
        Private _start As Date
        Private _end As Date
        Private _due As Date
        Private _compet As Double
        Private _goal As Double
        Private _quest As Double
        Private _status As String
        Private _year As Integer
        Private _reviewer As Double
        Private _reviewee As Double
        Private _reviewer2 As Double
        Private _360 As Double
        Private _company As String
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
        Public Property StartPeriod() As Date
            Get
                Return _start
            End Get
            Set(ByVal value As Date)
                _start = value
            End Set
        End Property
        Public Property EndPeriod() As Date
            Get
                Return _end
            End Get
            Set(ByVal value As Date)
                _end = value
            End Set
        End Property

        Public Property DueDate() As Date
            Get
                Return _due
            End Get
            Set(ByVal value As Date)
                _due = value
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

        Public Property ReviewYear() As Integer
            Get
                Return _year
            End Get
            Set(ByVal value As Integer)
                _year = value
            End Set
        End Property

        Public Property ReviewerWeight() As Double
            Get
                Return _reviewer
            End Get
            Set(ByVal value As Double)
                _reviewer = value
            End Set
        End Property

        Public Property Reviewer2Weight() As Double
            Get
                Return _reviewer2
            End Get
            Set(ByVal value As Double)
                _reviewer2 = value
            End Set
        End Property

        Public Property RevieweeWeight() As Double
            Get
                Return _reviewee
            End Get
            Set(ByVal value As Double)
                _reviewee = value
            End Set
        End Property

        Public Property Company() As String
            Get
                Return _company
            End Get
            Set(ByVal value As String)
                _company = value
            End Set
        End Property
#End Region
    End Class
End Namespace
