Imports System.Data.SqlClient

Namespace Classes
    Public Class DataOperationsSqlServer
        Private ConnectionString As String = "Data Source=KARENS-PC;Initial Catalog=NorthWindAzure;Integrated Security=True"
        Public LastException As Exception
        Public Function LoadCustomerRecordsUsingDataTable() As DataTable

            Dim selectStatement =
                    "SELECT Cust.CustomerIdentifier, CT.ContactTypeIdentifier, Cust.CompanyName, " &
                    "Cust.ContactName, CT.ContactTitle, Cust.Address AS Street, Cust.City, " &
                    "Cust.PostalCode, Cust.Country, Cust.Phone, Cust.ModifiedDate " &
                    "FROM Customers AS Cust INNER JOIN ContactType AS CT ON " &
                    "Cust.ContactTypeIdentifier = CT.ContactTypeIdentifier;"

            Dim customerDataTable = New DataTable

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    Try
                        cmd.CommandText = selectStatement

                        cn.Open()

                        customerDataTable.Load(cmd.ExecuteReader())

                        customerDataTable.Columns("CustomerIdentifier").ColumnMapping = MappingType.Hidden
                        customerDataTable.Columns("ContactTypeIdentifier").ColumnMapping = MappingType.Hidden
                        customerDataTable.Columns("ModifiedDate").ColumnMapping = MappingType.Hidden

                    Catch ex As Exception
                        LastException = ex
                    End Try
                End Using
            End Using

            Return customerDataTable
        End Function

    End Class
End Namespace