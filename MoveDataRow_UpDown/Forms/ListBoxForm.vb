Imports System.Data.OleDb
Imports MoveDataRow_UpDown.Classes

Public Class frmListBoxForm
    WithEvents bsData As New BindingSource
    Private HasChanges As Boolean = False
    Private Sub cmdMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUp.Click
        If ListBox1.SelectedIndex >= 0 Then
            ListBox1.MoveRowUp(bsData)
            HasChanges = True
        End If
    End Sub
    Private Sub cmdMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDown.Click
        If ListBox1.SelectedIndex >= 0 Then
            ListBox1.MoveRowDown(bsData)
            HasChanges = True
        End If
    End Sub
    Private Sub frmListBoxForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If HasChanges Then
            Dim ops As New DataOperationsAccess
            ops.DoListBoxUpdates(CType(bsData.DataSource, DataTable))
        End If
    End Sub
    Private Sub frmListBoxForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ops As New DataOperationsAccess
        bsData.DataSource = ops.LoadMusicalData()
        ListBox1.DisplayMember = "DisplayText"
        ListBox1.ValueMember = "Identifier"
        ListBox1.DataSource = bsData
    End Sub
End Class

