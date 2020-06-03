Namespace GOSHRM.GOSHRM.BO
    Public Class clsApproveLeave
#Region "Private Variables"
        Private _refno As String
        Private _action As String
        Private _status As String
        Private _status2 As String
#End Region

#Region "Public Properties"
        Public Property RefNo() As String
            Get
                Return _refno
            End Get
            Set(ByVal value As String)
                _refno = value
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
        Public Property Status2() As String
            Get
                Return _status2
            End Get
            Set(ByVal value As String)
                _status2 = value
            End Set
        End Property
        
#End Region
    End Class
End Namespace
