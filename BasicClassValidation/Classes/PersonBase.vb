Imports System.ComponentModel.DataAnnotations

Namespace Classes
    Public Class PersonBase
        ''' <summary>
        ''' Key to identifying the a specific person
        ''' </summary>
        ''' <returns></returns>
        Public Property Identifier() As Integer
        ''' <summary>
        ''' First name of the person
        ''' </summary>
        ''' <returns></returns>
        <Required(ErrorMessage:="{0} is required"), DataType(DataType.Text)>
        Public Property FirstName() As String
        ''' <summary>
        ''' Middle name or initial of the person
        ''' </summary>
        ''' <returns></returns>
        Public Property MiddleName() As String
        ''' <summary>
        ''' Last name of the person
        ''' </summary>
        ''' <returns></returns>
        <Required(ErrorMessage:="{0} is required"), DataType(DataType.Text)>
        Public Property LastName() As String
        ''' <summary>
        ''' Date of birth of the person
        ''' </summary>
        ''' <returns></returns>
        <Required(ErrorMessage:="{0} is required and valid {1}"), DataType(DataType.DateTime)>
        Public Property BirthDate() As DateTime?
        ''' <summary>
        ''' Method to calculate person's age. There is no check for BirthDate
        ''' being null as BirthDate is a required property
        ''' </summary>
        ''' <returns></returns>
        Public Function Age() As Integer
            Return Convert.ToInt32(Date.UtcNow.Date.Year - BirthDate.Value.Year)
        End Function
        ''' <summary>
        ''' Records modification date of person record
        ''' </summary>
        ''' <returns></returns>
        Public Property ModifiedDate() As DateTime
        ''' <summary>
        ''' Records person who modified the person
        ''' </summary>
        ''' <returns></returns>
        Public Property ModifiedByUserId() As Integer
    End Class
End Namespace