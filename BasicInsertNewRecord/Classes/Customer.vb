Imports System.ComponentModel.DataAnnotations
Imports BaseClassValidator.Rules

Namespace Classes
    Public Class Customer
        ''' <summary>
        ''' Primary key
        ''' </summary>
        ''' <returns></returns>
        Public Property CustomerIdentifier As Integer
        <Required, StringLength(40)>
        Public Property CompanyName As String
        Public Property ContactName As String
        Public Property Address As String
        Public Property City As String
        Public Property Region As String
        Public Property PostalCode As String

        Public Property Country As String
        ''' <summary>
        ''' Foreign key to country table
        ''' </summary>
        ''' <returns></returns>
        <Country(SelectCountry:=0)>
        Public Property CountryIdentifier As Integer?
        Public Property Phone As String
        Public Property Fax As String
        ''' <summary>
        ''' Foreign key to ContactType table
        ''' </summary>
        ''' <returns></returns>
        <ContactType(SelectContactType:=0)>
        Public Property ContactTypeIdentifier As Integer?
        Public Property ModifiedDate As DateTime?

        Public Overrides Function ToString() As String
            Return CompanyName
        End Function
    End Class
End Namespace