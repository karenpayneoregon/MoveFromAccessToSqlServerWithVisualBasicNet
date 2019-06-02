Imports System.Text.RegularExpressions

Namespace LanguageExtensions

    Public Module StringExtensions
        <Runtime.CompilerServices.Extension>
        Public Function SplitCamelCase(sender As String) As String
            Return Regex.Replace(Regex.Replace(sender, "(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), "(\p{Ll})(\P{Ll})", "$1 $2")
        End Function
        ''' <summary>
        ''' Convert to date or return Nothing
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        <Runtime.CompilerServices.Extension>
        Public Function TryParse(ByVal text As String) As Date?
            Dim dateValue As Date
            Return If(Date.TryParse(text, dateValue), dateValue, CType(Nothing, Date?))
        End Function
    End Module
End Namespace