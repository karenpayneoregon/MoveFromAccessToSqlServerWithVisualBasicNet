Namespace Classes
    ''' <summary>
    ''' Contains two methods for moving DataRows up/down. 
    ''' You could easily tweak the code to work for say a ListBox.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module DataGridViewMoveRowsUpDown
        ''' <summary>
        ''' Determine if a BindingSource is valid for move up or down methods
        ''' </summary>
        ''' <param name="bindingSource"></param>
        <DebuggerStepThrough()>
        <Runtime.CompilerServices.Extension()>
        Private Sub IsValid(bindingSource As BindingSource)
            If bindingSource.DataSource IsNot Nothing Then
                If Not bindingSource.DataSource.GetType() Is GetType(DataTable) Then
                    Throw New Exception("DataSource must be a DataTable")
                End If
            Else
                Throw New Exception("DataSource must be set to a populated DataTable")
            End If
        End Sub
        ''' <summary>
        ''' Move row up one
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="bindingSource"></param>
        <DebuggerStepThrough()>
        <Runtime.CompilerServices.Extension()>
        Public Sub MoveRowUp(sender As DataGridView, bindingSource As BindingSource)

            bindingSource.IsValid()

            If Not String.IsNullOrWhiteSpace(bindingSource.Sort) Then
                bindingSource.Sort = ""
            End If

            Dim CurrentColumnIndex As Integer = sender.CurrentCell.ColumnIndex
            Dim NewIndex = CInt(IIf(bindingSource.Position = 0, 0, bindingSource.Position - 1))

            Dim dt = CType(bindingSource.DataSource, DataTable)

            Dim RowToMove As DataRow = DirectCast(bindingSource.Current, DataRowView).Row
            Dim NewRow As DataRow = dt.NewRow

            NewRow.ItemArray = RowToMove.ItemArray
            dt.Rows.RemoveAt(bindingSource.Position)
            dt.Rows.InsertAt(NewRow, NewIndex)

            dt.AcceptChanges()

            bindingSource.Position = NewIndex
            sender.CurrentCell = sender(CurrentColumnIndex, NewIndex)

        End Sub
        ''' <summary>
        ''' Move row down 1
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="bindingSource"></param>
        <DebuggerStepThrough()>
        <Runtime.CompilerServices.Extension()>
        Public Sub MoveRowDown(sender As DataGridView, bindingSource As BindingSource)

            bindingSource.IsValid()

            If Not String.IsNullOrWhiteSpace(bindingSource.Sort) Then
                bindingSource.Sort = ""
            End If

            Dim CurrentColumnIndex As Integer = sender.CurrentCell.ColumnIndex
            Dim UpperLimit As Integer = bindingSource.Count - 1
            Dim NewIndex As Integer = CInt(IIf(bindingSource.Position + 1 >= UpperLimit,
                                               UpperLimit, bindingSource.Position + 1))

            Dim dt = CType(bindingSource.DataSource, DataTable)
            Dim RowToMove As DataRow = DirectCast(bindingSource.Current, DataRowView).Row
            Dim NewRow As DataRow = dt.NewRow

            NewRow.ItemArray = RowToMove.ItemArray
            dt.Rows.RemoveAt(bindingSource.Position)
            dt.Rows.InsertAt(NewRow, NewIndex)

            dt.AcceptChanges()

            bindingSource.Position = NewIndex
            sender.CurrentCell = sender(CurrentColumnIndex, NewIndex)

        End Sub

        <DebuggerStepThrough()>
        <Runtime.CompilerServices.Extension()>
        Public Sub MoveRowUp(sender As ListBox, bindingSource As BindingSource)

            bindingSource.IsValid()

            If Not String.IsNullOrWhiteSpace(bindingSource.Sort) Then
                bindingSource.Sort = ""
            End If

            Dim DisplayText As String = sender.Text
            Dim SelectedIndex As Integer = bindingSource.Position
            Dim NewIndex As Integer = CInt(IIf(bindingSource.Position = 0, 0,
                                               bindingSource.Position - 1))

            Dim dt = CType(bindingSource.DataSource, DataTable)

            Dim RowToMove As DataRow = DirectCast(bindingSource.Current, DataRowView).Row
            Dim NewRow As DataRow = dt.NewRow

            NewRow.ItemArray = RowToMove.ItemArray
            dt.Rows.RemoveAt(SelectedIndex)
            dt.Rows.InsertAt(NewRow, NewIndex)

            dt.AcceptChanges()
            bindingSource.Position = bindingSource.Find(sender.DisplayMember, DisplayText)

            For rowIndex As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(rowIndex).Item(2) = rowIndex
            Next

        End Sub
        <DebuggerStepThrough()>
        <Runtime.CompilerServices.Extension()>
        Public Sub MoveRowDown(sender As ListBox, bindingSource As BindingSource)

            bindingSource.IsValid()

            If Not String.IsNullOrWhiteSpace(bindingSource.Sort) Then
                bindingSource.Sort = ""
            End If

            Dim DisplayText As String = sender.Text

            Dim SelectIndex As Integer = bindingSource.Position

            Dim UpperLimit As Integer = bindingSource.Count - 1
            Dim NewIndex As Integer = CInt(IIf(bindingSource.Position + 1 >= UpperLimit,
                                               UpperLimit, bindingSource.Position + 1))

            Dim dt = CType(bindingSource.DataSource, DataTable)

            Dim RowToMove As DataRow = DirectCast(bindingSource.Current, DataRowView).Row
            Dim NewRow As DataRow = dt.NewRow

            NewRow.ItemArray = RowToMove.ItemArray
            dt.Rows.RemoveAt(SelectIndex)
            dt.Rows.InsertAt(NewRow, NewIndex)

            dt.AcceptChanges()
            bindingSource.Position = bindingSource.Find(sender.DisplayMember, DisplayText)

            For rowIndex As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(rowIndex).Item(2) = rowIndex
            Next

        End Sub
    End Module
End Namespace