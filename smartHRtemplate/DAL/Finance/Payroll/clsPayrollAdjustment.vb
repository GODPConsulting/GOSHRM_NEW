Namespace GOSHRM.GOSHRM.BO
    Public Class clsPayrollAdjustment
#Region "Private Variables"
        Private _employee As String
        Private _description As String
        Private _adjtype As String
        Private _amount As Double
        Private _paydate As Date
        Private _taxable As String
        Private _note As String
        Private _approvalstatus As String

#End Region

#Region "Public Properties"
        Public Property Employee() As String
            Get
                Return _Employee
            End Get
            Set(ByVal value As String)
                _Employee = value
            End Set
        End Property
        Public Property PayrollDesc() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property
        Public Property Adjustment() As String
            Get
                Return _adjtype
            End Get
            Set(ByVal value As String)
                _adjtype = value
            End Set
        End Property
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
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
        Public Property Taxable() As String
            Get
                Return _taxable
            End Get
            Set(ByVal value As String)
                _taxable = value
            End Set
        End Property
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
            End Set
        End Property
        Public Property ApprovalStatus() As String
            Get
                Return _approvalstatus
            End Get
            Set(ByVal value As String)
                _approvalstatus = value
            End Set
        End Property
        
#End Region
    End Class
End Namespace
