Namespace GOSHRM.GOSHRM.BO
    Public Class clsEmpLang
#Region "Private Variables"
        Private _id As Integer
        Private _empid As String
        Private _language As String
        Private _read As String
        Private _write As String
        Private _speak As String
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
        Public Property Language() As String
            Get
                Return _language
            End Get
            Set(ByVal value As String)
                _language = value
            End Set
        End Property
        Public Property Read() As String
            Get
                Return _read
            End Get
            Set(ByVal value As String)
                _read = value
            End Set
        End Property
        Public Property Write() As String
            Get
                Return _write
            End Get
            Set(ByVal value As String)
                _write = value
            End Set
        End Property
        Public Property Speak() As String
            Get
                Return _speak
            End Get
            Set(ByVal value As String)
                _speak = value
            End Set
        End Property

#End Region
    End Class
End Namespace
