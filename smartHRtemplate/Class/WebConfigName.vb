
Imports System.Configuration
Namespace GOSHRM.GOSHRM.DAT
    Public Class WebConfigName
        Public Shared ReadOnly Property ConnectionString() As String
            Get
                Return ConfigurationManager.ConnectionStrings("GOSHRMConnectionString").ToString
            End Get
        End Property
        Public Shared ReadOnly Property CloudConnString() As String
            Get
                Return ConfigurationManager.ConnectionStrings("CloudGOSHRMConnString").ToString
            End Get
        End Property

    End Class
End Namespace

