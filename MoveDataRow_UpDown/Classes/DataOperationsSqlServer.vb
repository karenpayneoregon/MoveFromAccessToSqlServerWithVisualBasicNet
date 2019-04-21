Imports System.Data.SqlClient
Imports BaseConnectionLibrary.ConnectionClasses

Namespace Classes
    ''' <summary>
    ''' MS-Access
    ''' Customer rather than Customers
    ''' Identifier rather then CustomerIdentifier
    ''' </summary>
    Public Class DataOperationsSqlServer
        Inherits SqlServerConnection

        ''' <summary>
        ''' DatabaseServer indicates the SQL-Server named or .\SQLEXPRESS
        ''' </summary>
        Public Sub New()
            DatabaseServer = "KARENS-PC"
            DefaultCatalog = "OrderingRows1"
        End Sub
        ''' <summary>
        ''' Load top ten customers
        ''' </summary>
        ''' <returns>DataTable with Customers data</returns>
        Public Function LoadCustomers() As DataTable

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    cmd.CommandText =
                        <SQL>
                        SELECT TOP 10 
                            CustomerIdentifier, 
                            CompanyName, 
                            ContactName, 
                            ContactTitle,
                            RowPosition
                        FROM 
                            Customers 
                        Order By RowPosition
                    </SQL>.Value

                    Dim customerDataTable As New DataTable

                    cn.Open()

                    customerDataTable.Load(cmd.ExecuteReader)

                    customerDataTable.Columns("CustomerIdentifier").ColumnMapping =
                        MappingType.Hidden

                    customerDataTable.Columns("RowPosition").ColumnMapping =
                        MappingType.Hidden

                    customerDataTable.AcceptChanges()

                    Return customerDataTable

                End Using
            End Using
        End Function
        ''' <summary>
        ''' Reorder rows based on positioning done in the user interface
        ''' </summary>
        ''' <param name="customerDataTable"></param>
        Public Sub UpdatePosition(customerDataTable As DataTable)

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}

                    cmd.CommandText =
                        <SQL>
                        UPDATE Customers 
                        SET RowPosition = @P1
                        WHERE CustomerIdentifier = @P2;
                    </SQL>.Value

                    cmd.Parameters.Add(New SqlParameter With
                        {.ParameterName = "@P1", .DbType = DbType.Int32})

                    cmd.Parameters.Add(New SqlParameter With
                        {.ParameterName = "@P2", .DbType = DbType.Int32})

                    cn.Open()

                    Dim position As Integer = 0

                    For rowIndex As Integer = 0 To customerDataTable.Rows.Count - 1

                        position = rowIndex + 1
                        cmd.Parameters("@P1").Value = position
                        cmd.Parameters("@P2").Value =
                            customerDataTable.Rows(rowIndex).Field(Of Integer)("CustomerIdentifier")

                        cmd.ExecuteNonQuery()
                    Next
                End Using
            End Using
        End Sub

    End Class
End Namespace