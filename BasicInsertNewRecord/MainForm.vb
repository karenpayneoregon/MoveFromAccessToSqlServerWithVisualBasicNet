Imports System.Text
Imports BaseClassValidator.Validators
Imports BaseWindowsControlLibrary
Imports BasicInsertNewRecord.Classes
Imports BasicInsertNewRecord.LanguageExtensions

Public Class MainForm
    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        LoadReferenceTable()
    End Sub
    ''' <summary>
    ''' Load references table for both Access and SQL-Server
    ''' </summary>
    Public Sub LoadReferenceTable()

        Dim accessOps = New DataOperationsAccess
        customerContactTypeAccessComboBox.DataSource = accessOps.LoadContactTypes(True)
        countryCodeAccessComboBox.DataSource = accessOps.LoadCountries(True)

        Dim sqlServerOps = New DataOperationsSqlServer
        customerContactTypeSqlServerComboBox.DataSource = sqlServerOps.LoadContactTypes(True)
        countryCodeSqlServerComboBox.DataSource = sqlServerOps.LoadCountries(True)

        '
        ' Here if the user name is Karens we are ready for inserts, otherwise
        ' controls must be populated.
        '
        If Environment.UserName = "Karens" Then
            customerContactTypeAccessComboBox.SelectedIndex = 3
            countryCodeAccessComboBox.SelectedIndex = 3
            customerContactTypeSqlServerComboBox.SelectedIndex = 3
            countryCodeSqlServerComboBox.SelectedIndex = 3
        Else
            Descendants(Of TextBox).ToList().ForEach(Sub(control) control.Text = "")
        End If

    End Sub
    ''' <summary>
    ''' Insert new record, return new primary key.
    ''' Validation is performed, in this case on CompanyName, country
    ''' and contact type
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub InsertNewSqlServerRecordButton_Click(sender As Object, e As EventArgs) _
        Handles InsertNewSqlServerRecordButton.Click

        Dim contactTypeIdentifier = CType(customerContactTypeSqlServerComboBox.SelectedItem, ContactType).
                ContactTypeIdentifier
        Dim countryIdentifier = CType(countryCodeSqlServerComboBox.SelectedItem, Country).
                CountryIdentifier

        Dim customer As New Customer With
                {
                    .CompanyName = customerNameSqlServerTextBox.Text,
                    .ContactTypeIdentifier = contactTypeIdentifier,
                    .CountryIdentifier = countryIdentifier,
                    .ContactFirstName = customerContactFirstNameSqlServerTextBox.Text,
                    .ContactLastName = customerContactLastNameSqlServerTextBox.Text,
                    .Address = customerAddressSqlServerTextBox.Text,
                    .City = customerCitySqlServerTextBox.Text,
                    .PostalCode = customerPostalSqlServerTextBox.Text
                }


        Dim validationResult = ValidationHelper.ValidateEntity(customer)

        If validationResult.HasError Then
            Dim sb As New StringBuilder

            sb.AppendLine("Validation issues")
            For Each errorItem In validationResult.Errors
                sb.AppendLine(errorItem.ErrorMessage.RemoveIndentifier.SplitCamelCase().RemoveDoubleSpaces)
            Next

            MessageBox.Show(sb.ToString())

            Exit Sub

        End If

        Dim sqlServerOps = New DataOperationsSqlServer
        sqlServerOps.InsertRecord(customer)
        '
        ' If the insert is successful show the new key, if failed display the error message.
        '
        If sqlServerOps.IsSuccessful Then
            customerIdentifierSqlServerTextBox.Text = customer.CustomerIdentifier.ToString()
        Else
            MessageBox.Show($"Failed to insert record{Environment.NewLine}{sqlServerOps.LastException.Message}")
        End If


    End Sub
End Class
