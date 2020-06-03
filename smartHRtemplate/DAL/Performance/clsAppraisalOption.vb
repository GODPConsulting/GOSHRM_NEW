Namespace GOSHRM.GOSHRM.BO
    Public Class clsAppraisalOption
#Region "Private Variables"
        Private _appcycle As String
        Private _mgrweight As Double
        Private _empweight As Double
        Private _visibility As String
        Private _lock As String
        Private _company As String
#End Region

#Region "Public Properties"
        Public Property AppraisalCycle() As String
            Get
                Return _appcycle

            End Get
            Set(ByVal value As String)
                _appcycle = value
            End Set
        End Property
      

        Public Property ReviewerCommentVisible() As String
            Get
                Return _visibility
            End Get
            Set(ByVal value As String)
                _visibility = value
            End Set
        End Property
        Public Property LockCurrentCycle() As String
            Get
                Return _lock
            End Get
            Set(ByVal value As String)
                _lock = value
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
