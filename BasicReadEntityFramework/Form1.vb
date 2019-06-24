Imports BaseExtensionsLibrary
Imports BasicReadEntityFramework.Classes
Imports Equin.ApplicationFramework

Public Class Form1
    ''' <summary>
    ''' Provides sort, filtering of data source
    ''' https://github.com/waynebloss/BindingListView
    ''' </summary>
    Private customerView As BindingListView(Of CustomerEntity)

    ''' <summary>
    ''' Read data using entity framework, hide primary key
    ''' columns and split camel case header text in the
    ''' DataGridView.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Dim ops As New DataOperations
        customerView = New BindingListView(Of CustomerEntity)(ops.ReadCustomers())

        DataGridView1.DataSource = customerView

        DataGridView1.Columns.Cast(Of DataGridViewColumn).Where(Function(column) column.Name.Contains("Identifier")).ToList().
            ForEach(Sub(col)
                        col.Visible = False
                    End Sub)

        For Each column As DataGridViewColumn In DataGridView1.Columns
            column.HeaderText = column.HeaderText.SplitCamelCase
        Next

    End Sub
End Class