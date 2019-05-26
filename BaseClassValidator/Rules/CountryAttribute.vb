Imports System.ComponentModel.DataAnnotations

Namespace Rules

    <AttributeUsage(AttributeTargets.Field Or AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)>
    Public Class CountryAttribute
        Inherits ValidationAttribute

        Public Property SelectCountry() As Integer

        Public Overrides Function IsValid(ByVal value As Object) As Boolean

            If CInt(value) = 0 Then
                Return False
            End If

            Return True
        End Function
    End Class
End Namespace