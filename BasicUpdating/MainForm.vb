Imports System.ComponentModel
Imports BaseWindowsControlLibrary
Imports BasicUpdating.Classes
''' <summary>
''' Raise Change Notifications Using a BindingSource
''' https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/raise-change-notifications--bindingsource
''' </summary>
Public Class MainForm
    WithEvents customersBindingSource As New BindingSource
    Private contactTypeList As List(Of ContactType)
    Private countryTypeList As List(Of Country)

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim ops = New DataOperations

        contactTypeList = ops.LoadContactTypes()
        customerContactTypeSqlServerComboBox.DataSource = contactTypeList
        countryTypeList = ops.LoadCountries()
        countryCodeSqlServerComboBox.DataSource = countryTypeList

        customersBindingSource.DataSource = ops.LoadCustomerRecordsUsingDataTable()
        SqlServerBindingNavigator.BindingSource = customersBindingSource

        SetupDataBindings()

        Dim TextBoxList As List(Of TextBox) = GroupBox1.Descendants(Of TextBox)().ToList()
        For Each tb As TextBox In TextBoxList
            AddHandler tb.Validated, AddressOf customerAddressSqlServerTextBox_Validated
        Next

        AddHandler customersBindingSource.ListChanged, AddressOf customersBindingSource_ListChanged

        AddHandler CType(customersBindingSource.DataSource, DataTable).RowChanged, AddressOf CustomerRowChanged
        AddHandler CType(customersBindingSource.DataSource, DataTable).RowDeleted, AddressOf CustomerRowDeleted
    End Sub
    Private Sub customersBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs)

        Text = $"{e.ListChangedType.ToString()} - {e.OldIndex} - {e.NewIndex}"
        If e.PropertyDescriptor IsNot Nothing Then
            'Console.WriteLine($"List changed: {e.PropertyDescriptor.Name}")
        End If

    End Sub
    Private Sub CustomerPositionChanged(sender As Object, e As EventArgs) Handles customersBindingSource.PositionChanged
        Dim customerRow = CType(customersBindingSource.Current, DataRowView).Row
        customerContactTypeSqlServerComboBox.SelectedIndex = contactTypeList.FindIndex(Function(contactType) contactType.ContactTypeIdentifier = customerRow.Field(Of Integer)("ContactTypeIdentifier"))
        countryCodeSqlServerComboBox.SelectedIndex = countryTypeList.FindIndex(Function(countryType) countryType.CountryIdentifier = customerRow.Field(Of Integer)("CountryIdentifier"))
    End Sub

    Private Sub SetupDataBindings()
        customerIdentifierTextBox.DataBindings.Add("Text", customersBindingSource, "CustomerIdentifier")
        CompanyNameTextBox.DataBindings.Add("Text", customersBindingSource, "CompanyName")
        FirstNameTextBox.DataBindings.Add("Text", customersBindingSource, "FirstName")
        LastNameTextBox.DataBindings.Add("Text", customersBindingSource, "LastName")
        StreetTextBox.DataBindings.Add("Text", customersBindingSource, "Street")
        CityTextBox.DataBindings.Add("Text", customersBindingSource, "City")
        PostalCodeTextBox.DataBindings.Add("Text", customersBindingSource, "PostalCode")
    End Sub

    Private Sub UpdateSqlServerRecordButton_Click(sender As Object, e As EventArgs) Handles UpdateSqlServerRecordButton.Click

    End Sub
    Private Sub customerContactTypeSqlServerComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles customerContactTypeSqlServerComboBox.SelectedIndexChanged
        If customersBindingSource.Current Is Nothing Then
            Exit Sub
        End If
        CType(customersBindingSource.Current, DataRowView).Row.SetField("ContactTypeIdentifier", CType(customerContactTypeSqlServerComboBox.SelectedItem, ContactType).ContactTypeIdentifier)
    End Sub

    Private Sub countryCodeSqlServerComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles countryCodeSqlServerComboBox.SelectedIndexChanged
        If customersBindingSource.Current Is Nothing Then
            Exit Sub
        End If
        CType(customersBindingSource.Current, DataRowView).Row.SetField("CountryIdentifier", CType(countryCodeSqlServerComboBox.SelectedItem, Country).CountryIdentifier)
    End Sub

    Private Sub customerAddressSqlServerTextBox_Validated(sender As Object, e As EventArgs)
        'Console.WriteLine(CType(sender, TextBox).Name)
    End Sub
    Private Sub CustomerRowChanged(ByVal sender As Object,
                            ByVal e As DataRowChangeEventArgs)

        Dim test = e.Row.RowState.ToString()
        Console.WriteLine("Row_Changed Event: name={0}; action={1}",
        e.Row("CompanyName"), e.Action)

        Console.WriteLine("Row_Changed Event: name={0}; action={1}",
        e.Row("FirstName"), e.Action)

    End Sub
    ''' <summary>
    ''' Not used, here to show how to access a just deleted record
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CustomerRowDeleted(sender As Object, e As DataRowChangeEventArgs)
        'Console.WriteLine("Row_Deleted Event: id={0}; action={1}", e.Row("CustomerIdentifier", DataRowVersion.Original), e.Action)
        Dim deleteCustomerIdentifier = CInt(e.Row("CustomerIdentifier", DataRowVersion.Original))
        Dim deleteCustomerName = CStr(e.Row("CompanyName", DataRowVersion.Original))

    End Sub
    ''' <summary>
    ''' Prompt user to delete current customer or not.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BindingNavigatorDeleteItem1_Click(sender As Object, e As EventArgs) Handles BindingNavigatorDeleteItem1.Click
        Dim row = CType(customersBindingSource.Current, DataRowView).Row
        If My.Dialogs.Question($"Remove '{row.Field(Of String)("CompanyName")}'") Then
            Dim ops = New DataOperations
            If Not ops.RemoveRecord(row.Field(Of Integer)("CustomerIdentifier")) Then
                MessageBox.Show(ops.LastException.Message)
            Else
                customersBindingSource.RemoveCurrent()
            End If
        End If
    End Sub
End Class
