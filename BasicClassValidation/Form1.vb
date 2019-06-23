Imports BasicClassValidation.Classes
Imports BasicClassValidation.Validators
Imports BasicClassValidation.LanguageExtensions
Imports BasicClassValidation.MockData

Public Class Form1
    ''' <summary>
    ''' Example for validation.
    ''' Mocked up several behind the scene properties
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ValidatePersonButton_Click(sender As Object, e As EventArgs) Handles ValidatePersonButton.Click
        Dim birthDate = $"{birthDateMonthComboBox.Text}/{daysComboBox.Text}/{yearsComboBox.Text}"

        Dim person As New Person With {
                .Identifier = 1,
                .FirstName = firstNameTextBox.Text,
                .LastName = lastNameTextBox.Text,
                .BirthDate = birthDate.TryParse(),
                .ModifiedByUserId = 12,
                .ModifiedDate = Now
        }

        Dim validationResult As EntityValidationResult = ValidationHelper.ValidateEntity(person)

        If validationResult.HasError Then
            MessageBox.Show(validationResult.ErrorMessageList())
        Else
            MessageBox.Show("Good person")
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim contactTypes = New ContactTypeList
        Dim contactTypeList = contactTypes.List()
        Dim dateData = New DateItem

        birthDateMonthComboBox.DataSource = dateData.MonthIndices
        daysComboBox.DataSource = dateData.DaysIndices
        yearsComboBox.DataSource = dateData.YearIndices

    End Sub
End Class
