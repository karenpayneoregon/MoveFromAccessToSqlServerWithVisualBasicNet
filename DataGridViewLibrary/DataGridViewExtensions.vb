Imports System.Windows.Forms

Public Module DataGridViewExtensions

    <DebuggerHidden()>
    <Runtime.CompilerServices.Extension()>
    Public Sub ExpandColumns(sender As DataGridView)
        For Each col As DataGridViewColumn In sender.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next
    End Sub
End Module
