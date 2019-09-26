Imports System.Data.OleDb

Public Class DataOperations
    Private ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;" &
                                         "Data Source=Database1.accdb"
    Public LastException As Exception

    Public Sub ReadData()

        Dim selectStatement =
                "SELECT ID, DocumentNumber, DocumentDate, StoreNumber, shortName, ItemCode, Item, Quantity " &
                "FROM ModifiedTable;"

        Dim dt As New DataTable

        Using cn As New OleDbConnection With {.ConnectionString = ConnectionString}
            Using cmd As New OleDbCommand With {.Connection = cn, .CommandText = selectStatement}
                cn.Open()
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Dim GroupResults = dt.AsEnumerable.GroupBy(
            Function(row) row.Field(Of Double)("DocumentNumber")) _
                .Select(Function(group) New GroupData With
                           {
                               .DocumentNumber = group.Key,
                               .DataRow = group.Select(Function(x) x),
                               .Data = group
                           })

        For Each group In GroupResults

            Console.WriteLine($"Doc number: {group.DocumentNumber} Count: {group.DataRow.Count()}")

            For Each dRow As DataRow In group.DataRow
                Console.WriteLine($"     {dRow.Field(Of String)("Item"),-40}{dRow.Field(Of Double)("Quantity")}")
            Next

        Next

    End Sub
End Class
Public Class GroupData
    Public Property DocumentNumber() As Double
    Public Property Data As IGrouping(Of Double, DataRow)
    Public Property DataRow() As IEnumerable(Of DataRow)
End Class