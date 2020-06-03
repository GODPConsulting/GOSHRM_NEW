Namespace GOSHRM.GOSHRM.BO
    Public Class clsExit
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _noticedate As Date
        Private _exitdate As Date
        Private _reason As String
        Private _exittype As String
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
        Public Property NoticeDate() As Date
            Get
                Return _noticedate
            End Get
            Set(ByVal value As Date)
                _noticedate = value
            End Set
        End Property
        Public Property ExitDate() As Date
            Get
                Return _exitdate
            End Get
            Set(ByVal value As Date)
                _exitdate = value
            End Set
        End Property
        Public Property Comment() As String
            Get
                Return _reason
            End Get
            Set(ByVal value As String)
                _reason = value
            End Set
        End Property
        Public Property ExitType() As String
            Get
                Return _exittype
            End Get
            Set(ByVal value As String)
                _exittype = value
            End Set
        End Property
#End Region
    End Class
End Namespace
