Imports System.Text.RegularExpressions

Public Module StringExtensions
    <Runtime.CompilerServices.Extension>
    Public Function SplitCamelCase(sender As String) As String
        Return Regex.Replace(Regex.Replace(sender, "(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), "(\p{Ll})(\P{Ll})", "$1 $2")
    End Function
End Module
