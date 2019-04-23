Public Enum TimeIncrement
    Hourly
    Quarterly
    HalfHour
End Enum
Public Class Hours
    Public Function Range(Optional ByVal pTimeIncrement As TimeIncrement = TimeIncrement.Hourly) As String()

        Const timeHhMmTtformat As String = "h:mm tt"

        Dim hours As IEnumerable(Of Date) =
                Enumerable.Range(0, 24).Select(Function(index) (Date.MinValue.AddHours(index)))

        Dim timeList = New List(Of String)()

        For Each dateTime In hours

            timeList.Add(dateTime.ToString(timeHhMmTtformat))

            If pTimeIncrement = TimeIncrement.Quarterly Then
                timeList.Add(dateTime.AddMinutes(15).ToString(timeHhMmTtformat))
                timeList.Add(dateTime.AddMinutes(30).ToString(timeHhMmTtformat))
                timeList.Add(dateTime.AddMinutes(45).ToString(timeHhMmTtformat))
            ElseIf pTimeIncrement = TimeIncrement.HalfHour Then
                timeList.Add(dateTime.AddMinutes(30).ToString(timeHhMmTtformat))
            End If
        Next

        Return timeList.ToArray()

    End Function

End Class
