Imports System.Globalization
Imports System.Text
Imports BaseExtensionsLibrary
Imports BasicRead.Classes
Imports DataGridViewLibrary

Public Class MainForm
    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim accessOperations As New DataOperationsAccess

        Dim customersFromAccessDataTable As DataTable = accessOperations.LoadCustomerRecordsUsingDataTable()
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
        Dim customersFromSqlServerDataTable As DataTable = sqlServerOperations.LoadCustomerRecordsUsingDataTable()
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

        Dim hours = New Hours()
        Dim range = hours.Range(TimeIncrement.Quarterly).ToList().Select(Function(time) time)
        Console.WriteLine()
        timeComboBox.DataSource = range.ToList

        Dim result = Now.RoundDateToMinuteInterval(30, RoundingDirection.RoundUp)
        timeComboBox.SelectedIndex = timeComboBox.FindString(result.ToString("h:mm tt"))

        'Dim rowIndex As Integer = 1
        'Dim result = String.Join(",",
        '    customersSqlServerDataGridView.Rows(
        '        rowIndex).Cells.Cast(Of DataGridViewCell).
        '                            Select(Function(cell) CStr(cell.Value)))
        'Console.WriteLine(result)

        'Dim results = customersSqlServerDataGridView.ExportAsStringArray()
        'For rowIndex As Integer = 0 To results.Count() - 1
        '    Console.WriteLine(results(rowIndex))

        'Next

        'Dim resultDown = Now.RoundDown().ToString("h:mm tt")
        'Console.WriteLine(resultDown)
        'Dim resultUp = Now.RoundUp().ToString("h:mm tt")
        'Console.WriteLine(resultUp)
        'Dim halfResults = Now.RoundMinutes().ToString("h:mm tt")
        'Console.WriteLine(halfResults)

        'Dim result = Now.RoundDateToMinuteInterval(30, RoundingDirection.RoundUp)
        'Console.WriteLine(result.ToString("h:mm tt"))

        'Descendants(Of ComboBox).ToList().ForEach(
        '    Sub(currentComboBox)

        '        AddHandler currentComboBox.DropDown,
        '             Sub(someComboBox As Object, a As EventArgs)
        '                 MessageBox.Show(CType(someComboBox, ComboBox).Name)
        '             End Sub
        '    End Sub)


    End Sub
End Class
Public Module ExtensionsDemo
    <DebuggerStepThrough()>
    <Runtime.CompilerServices.Extension()>
    Public Function ExportAsStringArray(sender As DataGridView) As String()
        Return (
            From row In sender.Rows
            Where Not DirectCast(row, DataGridViewRow).IsNewRow
            Let rowItem = String.Join(",", Array.ConvertAll(
                DirectCast(row, DataGridViewRow).Cells.Cast(Of DataGridViewCell).ToArray,
                Function(c As DataGridViewCell)
                    Return If(c.Value Is Nothing, "", c.Value.ToString)
                End Function)) Select RowItem = rowItem).ToArray

    End Function

End Module
Public Enum RoundingDirection
    RoundUp
    RoundDown
    Round
End Enum
Public Module DateTimeExtensions
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="time">Time to work against</param>
    ''' <param name="minuteInterval">Interval to move to</param>
    ''' <param name="direction">Down, Up</param>
    ''' <returns></returns>
    <Runtime.CompilerServices.Extension>
    Public Function RoundDateToMinuteInterval(time As Date, minuteInterval As Integer, direction As RoundingDirection) As Date
        If minuteInterval = 0 Then
            Return time
        End If

        Dim interval = CDec(minuteInterval)
        Dim actMinute = CDec(time.Minute)

        If actMinute = 0.00D Then
            Return time
        End If

        Dim newMinutes As Integer = 0

        Select Case direction
            Case RoundingDirection.Round
                newMinutes = CInt(Fix(Math.Round(actMinute / interval, 0) * interval))
            Case RoundingDirection.RoundDown
                newMinutes = CInt(Fix(Math.Truncate(actMinute / interval) * interval))
            Case RoundingDirection.RoundUp
                newMinutes = CInt(Fix(Math.Ceiling(actMinute / interval) * interval))
        End Select

        ' *** strip time 
        time = time.AddMinutes(time.Minute * -1)
        time = time.AddSeconds(time.Second * -1)
        time = time.AddMilliseconds(time.Millisecond * -1)

        ' *** add new minutes back on            
        Return time.AddMinutes(newMinutes)
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function RoundDown(dateTime As Date) As Date
        Return New Date(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, (dateTime.Minute \ 15) * 15, 0)
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function RoundUp(dateTime As Date) As Date
        Return (New Date(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0)).AddMinutes(If(dateTime.Minute Mod 15 = 0, 0, 15 - dateTime.Minute Mod 15))
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function RoundMinutes(dateTime As Date) As Date
        Dim minute As Integer

        If (dateTime.Minute <= 15) OrElse ((dateTime.Minute > 30) AndAlso (dateTime.Minute <= 45)) Then
            minute = -1
        Else
            minute = +1
        End If

        Do While (dateTime.Minute <> 0) AndAlso (dateTime.Minute <> 30)
            dateTime = dateTime.AddMinutes(minute)
        Loop


        Return dateTime

    End Function

End Module
Public Module ControlExtensions
    ''' <summary>
    ''' Provides access to all controls on a form including
    ''' controls within containers e.g. panel and group-box, Panel etc.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="control"></param>
    ''' <returns></returns>
    <Runtime.CompilerServices.Extension>
    Public Iterator Function Descendants(Of T As Class)(control As Control) As IEnumerable(Of T)
        For Each child As Control In control.Controls

            Dim currentChild = TryCast(child, T)
            If currentChild IsNot Nothing Then
                Yield currentChild
            End If

            If child.HasChildren Then
                For Each descendant As T In child.Descendants(Of T)()
                    Yield descendant
                Next
            End If
        Next
    End Function
End Module