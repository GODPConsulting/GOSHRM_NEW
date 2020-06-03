Namespace GOSHRM.GOSHRM.BO
    Public Class NotificationData
        Public EmbeddedTitleIcon As String
        Public NotificationTitle As String
        Public EmbeddedContentIcon As String
        Public NotificationText As String

        Public Sub New(embeddedTitleIcon As String, notificationTitle As String, embeddedContentIcon As String, notificationText As String)
            Me.EmbeddedTitleIcon = embeddedTitleIcon
            Me.NotificationTitle = notificationTitle
            Me.EmbeddedContentIcon = embeddedContentIcon
            Me.NotificationText = notificationText
        End Sub
    End Class
End Namespace
