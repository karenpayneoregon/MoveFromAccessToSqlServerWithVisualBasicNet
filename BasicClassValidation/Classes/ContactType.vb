Namespace Classes
    Public Class ContactType
        Public Property ContactTypeIdentifier() As Integer
        Public Property ContactTitle() As String

        Public Overrides Function ToString() As String
            Return ContactTitle
        End Function
    End Class
End Namespace