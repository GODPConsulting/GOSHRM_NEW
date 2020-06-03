Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpTraningApproval
#Region "Private Variables"
        Private _id As Integer
        Private _hodstatus As String
        Private _coachstatus As String
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
        
        Public Property HODStatus() As String
            Get
                Return _hodstatus
            End Get
            Set(ByVal value As String)
                _hodstatus = value
            End Set
        End Property

        Public Property CoachStatus() As String
            Get
                Return _coachstatus
            End Get
            Set(ByVal value As String)
                _coachstatus = value
            End Set
        End Property
        
#End Region
    End Class
End Namespace
