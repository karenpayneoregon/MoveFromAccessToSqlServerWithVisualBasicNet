﻿Imports System.Data.SqlClient

Namespace Classes
    Public Class DataOperationsSqlServer
        Private ConnectionString As String = "Data Source=KARENS-PC;Initial Catalog=NorthWindAzure;Integrated Security=True"
        Public LastException As Exception
        Public Function LoadCustomerRecordsUsingDataTable(pContactTypeIdentifier As Integer, pCountry As String) As DataTable

            Dim selectStatement =
                    <SQL>
                        SELECT C.CustomerIdentifier ,
                               C.CompanyName ,
                               C.ContactName ,
                               C.ContactTypeIdentifier ,
                               CT.ContactTitle ,
                               C.Country
                        FROM   Customers AS C
                               INNER JOIN ContactType AS CT ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                        WHERE  ( C.ContactTypeIdentifier = @ContactTypeIdentifier )
                               AND ( C.Country = @Country );
                     </SQL>.Value

            Dim customerDataTable = New DataTable

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    Try
                        cmd.CommandText = selectStatement
                        cmd.Parameters.AddWithValue("@ContactTypeIdentifier", pContactTypeIdentifier)
                        cmd.Parameters.AddWithValue("@Country", pCountry)
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