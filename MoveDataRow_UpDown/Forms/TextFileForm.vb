Imports MoveDataRow_UpDown.Classes

''' <summary>
''' Lesser demo in regards to Form1, here we load from a TextFile.
''' Does not save ordering or sorting is not done.
''' </summary>
''' <remarks></remarks>
Public Class frmTextFileForm
    WithEvents bsData As New BindingSource
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ops As New DataOperationsAccess
        Dim dt = ops.LoadCustomersTextFileForm()

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

    End Sub
    Private Sub cmdMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUp.Click
        DataGridView1.MoveRowUp(bsData)
    End Sub
    Private Sub cmdMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDown.Click
        DataGridView1.MoveRowDown(bsData)
    End Sub
    Private Sub cmdClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Close()
    End Sub
End Class