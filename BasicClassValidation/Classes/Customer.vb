Imports System.ComponentModel.DataAnnotations
Imports BasicClassValidation.Rules
Namespace Classes
    Public Class Customer
        ''' <summary>
        ''' Primary key
        ''' </summary>
        ''' <returns></returns>
        Public Property CustomerIdentifier As Integer
        ''' <summary>
        ''' Since CompanyName is a required field in the database this rule
        ''' Indicates it's required, max string length is the max length in the
        ''' table and min length simply forces more than several characters.
        ''' 
        ''' The ErrorMessage {0} is replaced with the field name and {1} and {2}
        ''' represent max and min field length.
        ''' </summary>
        ''' <returns></returns>
        <StringLength(72, MinimumLength:=10, ErrorMessage:="{0} cannot be longer than {1} characters and less than {2} characters")>
        Public Property CompanyName As String
        ''' <summary>
        ''' Used to move from a poorly designed database table
        ''' which should have a table for contacts. This will be
        ''' handled in a later part of this series.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property ContactName As String
            Get
                Return $"{ContactFirstName} {ContactLastName}"
            End Get
        End Property
        Public Property ContactFirstName As String
        Public Property ContactLastName As String
        Public Property Address As String
        Public Property City As String
        Public Property PostalCode As String
        Public Property Country As String
        ''' <summary>
        ''' Foreign key to country table
        ''' Here SelectCountry is not acceptable as it
        ''' represents the first element in a ComboBox "Select"
        ''' </summary>
        ''' <returns></returns>
        <Country(SelectCountry:=0, ErrorMessage:="Must select a country")>
        Public Property CountryIdentifier As Integer?
        Public Property Phone As String
        ''' <summary>
        ''' Password
        ''' </summary>
        ''' <returns></returns>
        Public Property Password() As String
        ''' <summary>
        ''' Used to confirm password which requires two TextBox controls
        ''' </summary>
        ''' <returns></returns>
        <Compare("Customer.Password", ErrorMessage:="The fields Password and Password Confirmation should be equals")>
        Public Property PasswordConfirmation() As String
        ''' <summary>
        ''' Foreign key to ContactType table
        ''' </summary>
        ''' <returns></returns>
        <ContactType(SelectContactType:=0, ErrorMessage:="Must select a contact type")>
        Public Property ContactTypeIdentifier As Integer?
        Public Property ModifiedDate As DateTime?
        ''' <summary>
        ''' Used for DisplayMember in a control along with
        ''' use for debugging sessions.
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            Return CompanyName
        End Function
    End Class
End Namespace