Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text

Public Module DataGridViewExtensions
    <Extension()>
    Public Sub ExportRows(sender As DataGridView, fileName As String, ByVal Optional delimiter As String = ",")
        If sender.RowCount > 0 Then
            Dim sb = New StringBuilder()
            Dim headers = sender.Columns.Cast(Of DataGridViewColumn)()
            sb.AppendLine(String.Join(delimiter, headers.Select(Function(column) column.HeaderText)))

            For Each row As DataGridViewRow In sender.Rows

                If Not row.IsNewRow = True Then
                    Dim cells = row.Cells.Cast(Of DataGridViewCell)()
                    sb.AppendLine(String.Join(delimiter, cells.Select(Function(cell) cell.Value)))
                End If
            Next

            File.WriteAllText(fileName, sb.ToString())
        End If
    End Sub
End Module
