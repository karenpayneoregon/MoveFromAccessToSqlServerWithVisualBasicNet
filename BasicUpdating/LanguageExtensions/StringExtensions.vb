Imports System.Text.RegularExpressions

Namespace LanguageExtensions

    Module StringExtensions
        ''' <summary>
        ''' Given a string with upper and lower cased letters separate them before each upper cased characters
        ''' </summary>
        ''' <param name="sender">String to work against</param>
        ''' <returns>String with spaces between upper-case letters</returns>
        <Runtime.CompilerServices.Extension>
        Public Function SplitCamelCase(sender As String) As String
            Return Regex.Replace(Regex.Replace(sender, "(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), "(\p{Ll})(\P{Ll})", "$1 $2")
        End Function
        ''' <summary>
        ''' Remove any and all double spaces
        ''' </summary>
        ''' <param name="sender">String to work against</param>
        ''' <returns>string with no double spaces</returns>
        <Runtime.CompilerServices.Extension>
        Public Function RemoveDoubleSpaces(sender As String) As String
            Dim options = RegexOptions.None
            Dim regex As New Regex("[ ]{2,}", options)

            Return regex.Replace(sender, " ")
        End Function
        <Runtime.CompilerServices.Extension>
        Public Function RemoveIndentifier(sender As String) As String
            Return sender.Replace("Identifier", "")
        End Function
    End Module
End Namespace