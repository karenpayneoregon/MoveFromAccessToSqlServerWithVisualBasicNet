Namespace Classes
    Public Class Contact
        Public Property ContactId() As Integer
        Public Property FirstName() As String
        Public Property LastName() As String
        Public ReadOnly Property FullName() As String
            Get
                Return $"{FirstName} {LastName}"
            End Get
        End Property
    End Class
End Namespace