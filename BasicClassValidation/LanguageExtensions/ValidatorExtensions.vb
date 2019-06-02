Imports System.ComponentModel.DataAnnotations
Imports System.Text
Imports BasicClassValidation.Validators

Namespace LanguageExtensions
    Public Module ValidatorExtensions
        ''' <summary>
        ''' Separates tokens with a space e.g. ContactName becomes Contact Name
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <returns></returns>
        <Runtime.CompilerServices.Extension>
        Public Function SanitizedErrorMessage(sender As ValidationResult) As String
            Return sender.ErrorMessage.SplitCamelCase()
        End Function
        ''' <summary>
        ''' Place all validation messages into a string with each validation message on one line
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <returns></returns>
        <Runtime.CompilerServices.Extension>
        Public Function ErrorMessageList(sender As EntityValidationResult) As String
            Dim sb As New StringBuilder
            sb.AppendLine("Validation issues")
            For Each errorItem As ValidationResult In sender.Errors
                sb.AppendLine(errorItem.SanitizedErrorMessage)
            Next

            Return sb.ToString()

        End Function
    End Module
End Namespace