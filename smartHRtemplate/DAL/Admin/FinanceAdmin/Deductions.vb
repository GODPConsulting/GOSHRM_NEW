Namespace GOSHRM.GOSHRM.BO
    Public Class clsDeductions
#Region "Private Variables"
        Private _id As String
        Private _title As String
        Private _deductiontype As String
        Private _amount As Double
        Private _date As Date
        Private _exempt As Boolean
        Private _type As String
        Private _note As String
        Private _active As Boolean
        Private _order As String
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
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property
        Public Property DeductionType() As String
            Get
                Return _deductiontype
            End Get
            Set(ByVal value As String)
                _deductiontype = value
            End Set
        End Property
        Public Property Amount() As Double
            Get
                Return _amount
            End Get
            Set(ByVal value As Double)
                _amount = value
            End Set
        End Property
        Public Property DateOfDeduction() As Date
            Get
                Return _date
            End Get
            Set(ByVal value As Date)
                _date = value
            End Set
        End Property
        Public Property ExemptSomeEmployees() As Boolean
            Get
                Return _exempt
            End Get
            Set(ByVal value As Boolean)
                _exempt = value
            End Set
        End Property
        Public Property InputType() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
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
        Public Property Active() As Boolean
            Get
                Return _active
            End Get
            Set(ByVal value As Boolean)
                _active = value
            End Set
        End Property
        Public Property Order() As Integer
            Get
                Return _order
            End Get
            Set(ByVal value As Integer)
                _order = value
            End Set
        End Property
#End Region
    End Class
End Namespace
