Namespace GOSHRM.GOSHRM.BO
    Public Class clsApplyLeave
#Region "Private Variables"
        Private _empid As String
        Private _approver1 As String
        Private _approver2 As String
        Private _leavetype As String
        Private _leavefrom As Date
        Private _leaveto As Date
        Private _location As String
        Private _reason As String
        Private _leave As String
        Private _paydate As Date
#End Region

#Region "Public Properties"
        Public Property EmpID() As String
            Get
                Return _empid
            End Get
            Set(ByVal value As String)
                _empid = value
            End Set
        End Property
        Public Property Approver1() As String
            Get
                Return _approver1
            End Get
            Set(ByVal value As String)
                _approver1 = value
            End Set
        End Property

        Public Property LeaveType() As String
            Get
                Return _leavetype
            End Get
            Set(ByVal value As String)
                _leavetype = value
            End Set
        End Property
        Public Property LeaveFrom() As Date
            Get
                Return _leavefrom
            End Get
            Set(ByVal value As Date)
                _leavefrom = value
            End Set
        End Property
        Public Property LeaveTo() As Date
            Get
                Return _leaveto
            End Get
            Set(ByVal value As Date)
                _leaveto = value
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
        Public Property Reason() As String
            Get
                Return _reason
            End Get
            Set(ByVal value As String)
                _reason = value
            End Set
        End Property
        Public Property Leave() As String
            Get
                Return _leave
            End Get
            Set(ByVal value As String)
                _leave = value
            End Set
        End Property

        Public Property PayDate() As Date
            Get
                Return _paydate
            End Get
            Set(ByVal value As Date)
                _paydate = value
            End Set
        End Property
#End Region
    End Class

End Namespace
