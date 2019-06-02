Namespace MockData
    Public Class DateItem
        Public ReadOnly Property MonthIndexs As List(Of String)
            Get

                Dim months = Enumerable.Range(1, 12).
                        Select(Function(month) month.ToString()).ToList()

                months.Insert(0, "")
                Return months
            End Get
        End Property

        Public ReadOnly Property DaysIndexs As List(Of String)
            Get

                Dim days = Enumerable.Range(1, 31).
                        Select(Function(month) month.ToString()).ToList()

                days.Insert(0, "")
                Return days
            End Get
        End Property

        Public ReadOnly Property YearIndies As List(Of String)
            Get

                Dim upperYear = Now.Year - 1945
                Dim years = Enumerable.Range(1945, (Now.Year - 1945) + 1).
                        Select(Function(month) month.ToString()).ToList()

                years.Insert(0, "")
                Return years
            End Get
        End Property
    End Class
End Namespace