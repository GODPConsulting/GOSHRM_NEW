Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpTraining
#Region "Private Variables"
        Private _id As String
        Private _empid As String
        Private _training As String
        Private _status As String
        Private _rating As String
        Private _comment As String
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
        Public Property EmpID() As String
            Get
                Return _empid
            End Get
            Set(ByVal value As String)
                _empid = value
            End Set
        End Property
        Public Property Training() As String
            Get
                Return _training
            End Get
            Set(ByVal value As String)
                _training = value
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
        Public Property Rating() As String
            Get
                Return _rating
            End Get
            Set(ByVal value As String)
                _rating = value
            End Set
        End Property
        Public Property Comment() As String
            Get
                Return _comment
            End Get
            Set(ByVal value As String)
                _comment = value
            End Set
        End Property
#End Region
    End Class
End Namespace
