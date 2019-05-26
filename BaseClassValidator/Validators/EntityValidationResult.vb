Imports System.ComponentModel.DataAnnotations

Namespace Validators
    Public Class EntityValidationResult
        Private privateErrors As IList(Of ValidationResult)
        Public Property Errors() As IList(Of ValidationResult)
            Get
                Return privateErrors
            End Get
            Private Set(value As IList(Of ValidationResult))
                privateErrors = value
            End Set
        End Property
        Public ReadOnly Property HasError() As Boolean
            Get
                Return Errors.Count > 0
            End Get
        End Property

        Public Sub New(Optional ByVal errors As IList(Of ValidationResult) = Nothing)
            Me.Errors = If(errors, New List(Of ValidationResult)())
        End Sub
    End Class
End Namespace