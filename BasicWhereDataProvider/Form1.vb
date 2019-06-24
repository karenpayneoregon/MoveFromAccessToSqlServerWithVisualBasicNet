Imports BaseExtensionsLibrary
Imports BasicWhereDataProvider.Classes
Imports DataGridViewLibrary

Public Class Form1
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim accessOperations As New DataOperationsAccess

        Dim customersFromAccessDataTable As DataTable =
                accessOperations.LoadCustomerRecordsUsingDataTable(12, "Germany")
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

        Dim sqlServerOperations As New DataOperationsSqlServer
        Dim customersFromSqlServerDataTable As DataTable =
                sqlServerOperations.LoadCustomerRecordsUsingDataTable(12, "Germany")

        customersSqlServerDataGridView.DataSource = customersFromSqlServerDataTable

        customersSqlServerDataGridView.ExpandColumns

        customersSqlServerDataGridView.Columns.
            Cast(Of DataGridViewColumn).
            ToList().
            ForEach(Sub(col)
                        col.HeaderText = col.HeaderText.SplitCamelCase()
                    End Sub)

    End Sub
End Class
