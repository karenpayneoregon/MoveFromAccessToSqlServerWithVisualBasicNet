Imports System.Globalization
Imports System.IO
Imports System.Text
Imports BaseExtensionsLibrary
Imports BasicRead.Classes
Imports DataGridViewLibrary

Public Class MainForm
    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim accessOperations As New DataOperationsAccess

        Dim customersFromAccessDataTable As DataTable =
                accessOperations.LoadCustomerRecordsUsingDataTable()

        If accessOperations.LastException Is Nothing Then
            ' If no run time exceptions populate DataGridView with the DataTable
            customersAccessDataGridView.DataSource = customersFromAccessDataTable

            ' Expand all columns so all data is visible
            customersAccessDataGridView.ExpandColumns

            ' Split column headers where a field name is PostalCode make it Postal Code.
            customersAccessDataGridView.Columns.
                Cast(Of DataGridViewColumn).
                ToList().
                ForEach(Sub(col)
                            col.HeaderText = col.HeaderText.SplitCamelCase()
                        End Sub)
        Else
            MessageBox.Show(accessOperations.LastException.Message)
        End If

        Dim sqlServerOperations As New DataOperationsSqlServer
        Dim customersFromSqlServerDataTable As DataTable =
                sqlServerOperations.LoadCustomerRecordsUsingDataTable()

        If sqlServerOperations.LastException Is Nothing Then

            customersSqlServerDataGridView.DataSource = customersFromSqlServerDataTable

            customersSqlServerDataGridView.ExpandColumns

            customersSqlServerDataGridView.Columns.
                Cast(Of DataGridViewColumn).
                ToList().
                ForEach(Sub(col)
                            col.HeaderText = col.HeaderText.SplitCamelCase()
                        End Sub)


        Else
            MessageBox.Show(accessOperations.LastException.Message)
        End If

    End Sub

    Private Sub closeApplicationButton_Click(sender As Object, e As EventArgs) Handles closeApplicationButton.Click
        Close()
    End Sub
End Class
