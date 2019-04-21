Imports MoveDataRow_UpDown.Classes

''' <summary>
''' Shows how to allow a user to reposition rows loaded from a database table.
''' If the user changed the order in FormClosing we write back to the database
''' the new order.
''' </summary>
''' <remarks></remarks>
Public Class frmAccessForm
    WithEvents bsData As New BindingSource
    Private HasChanges As Boolean = False
    ''' <summary>
    ''' Save changes back to database table
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If HasChanges Then
            Dim ops As New DataOperations
            Dim dt = CType(bsData.DataSource, DataTable)
            ops.UpdatePositionMsAccess(CType(bsData.DataSource, DataTable))
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ops As New DataOperations
        DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader
        DataGridView1.EnableHeadersVisualStyles = False

        Dim dt = ops.LoadCustomersAccessForm()

        bsData.DataSource = dt
        Label1.DataBindings.Add("Text", bsData, "Identifier")
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
    Private Sub cmdMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUp.Click
        HasChanges = True
        DataGridView1.MoveRowUp(bsData)
    End Sub
    Private Sub cmdMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDown.Click
        HasChanges = True
        DataGridView1.MoveRowDown(bsData)
    End Sub
    ''' <summary>
    ''' Allows person running code to sort by one of three columns
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilter.Click
        Dim CompanyID As String = CType(bsData.Current, DataRowView).Item("Identifier").ToString

        If cboFilter.SelectedIndex = 0 Then
            bsData.Sort = "CompanyName ASC"
        ElseIf cboFilter.SelectedIndex = 1 Then
            bsData.Sort = "Identifier ASC"
        ElseIf cboFilter.SelectedIndex = 2 Then
            bsData.Sort = "RowPosition ASC"
        End If

        bsData.Position = bsData.Find("Identifier", CompanyID)

    End Sub
    ''' <summary>
    ''' Shows row number in RowHeader which allows the person running this code to track what is going on.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If Not DataGridView1.Rows(e.RowIndex).IsNewRow Then
            DataGridView1.Rows(e.RowIndex).HeaderCell.Value = CStr(e.RowIndex + 1)
        End If
    End Sub
    Private Sub cmdClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Close()
    End Sub
End Class
