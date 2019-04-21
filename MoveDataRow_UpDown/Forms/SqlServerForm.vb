Imports System.ComponentModel
Imports MoveDataRow_UpDown.Classes

Namespace Forms
    Public Class SqlServerForm
        Private dataOperations As New DataOperationsSqlServer
        Private bsData As New BindingSource
        Private HasChanges As Boolean = False
        Private Sub SqlServerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
            DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader
            DataGridView1.EnableHeadersVisualStyles = False

            Dim dt = dataOperations.LoadCustomers()

            bsData.DataSource = dt
            Label1.DataBindings.Add("Text", bsData, "CustomerIdentifier")
            DataGridView1.DataSource = bsData
            DataGridView1.CurrentCell = DataGridView1(1, 1)

            For Each Column As DataGridViewColumn In DataGridView1.Columns
                Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            Next

            DataGridView1.Columns("CompanyName").HeaderText = "Company"
            DataGridView1.Columns("ContactName").HeaderText = "Contact"
            DataGridView1.Columns("ContactTitle").HeaderText = "Title"
            cboFilter.SelectedIndex = 2
        End Sub

        Private Sub cmdMoveUp_Click(sender As Object, e As EventArgs) Handles cmdMoveUp.Click
            HasChanges = True
            DataGridView1.MoveRowUp(bsData)
        End Sub

        Private Sub cmdMoveDown_Click(sender As Object, e As EventArgs) Handles cmdMoveDown.Click
            HasChanges = True
            DataGridView1.MoveRowDown(bsData)
        End Sub

        Private Sub cmdFilter_Click(sender As Object, e As EventArgs) Handles cmdFilter.Click
            Dim CompanyID As String = CType(bsData.Current, DataRowView).Item("CustomerIdentifier").ToString

            If cboFilter.SelectedIndex = 0 Then
                bsData.Sort = "CompanyName ASC"
            ElseIf cboFilter.SelectedIndex = 1 Then
                bsData.Sort = "Identifier ASC"
            ElseIf cboFilter.SelectedIndex = 2 Then
                bsData.Sort = "RowPosition ASC"
            End If

            bsData.Position = bsData.Find("Identifier", CompanyID)

        End Sub
        Private Sub SqlServerForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
            If HasChanges Then
                Dim ops As New DataOperations
                Dim dt = CType(bsData.DataSource, DataTable)
                dataOperations.UpdatePosition(CType(bsData.DataSource, DataTable))
            End If
        End Sub
        Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
            Close()
        End Sub
    End Class
End Namespace