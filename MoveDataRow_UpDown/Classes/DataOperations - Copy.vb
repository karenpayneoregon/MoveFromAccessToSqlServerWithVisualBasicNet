﻿Imports System.Data.OleDb
Imports System.IO


Namespace Classes

    Public Class DataOperations
        Inherits BaseConnectionLibrary.ConnectionClasses.SqlServerConnection
        ''' <summary>
        ''' Two forms use this connection string
        ''' </summary>
        ''' <remarks></remarks>
        Public BuilderAccdb As New OleDbConnectionStringBuilder With
            {
            .Provider = "Microsoft.ACE.OLEDB.12.0",
            .DataSource = Path.Combine(Application.StartupPath, "Database1.accdb")
            }
        Public Function LoadMusicalData() As DataTable

            Dim dt As New DataTable

            Using cn As New OleDbConnection With {.ConnectionString = BuilderAccdb.ConnectionString}
                Using cmd As New OleDbCommand With
                    {
                    .CommandText =
                        <SQL>
                        SELECT 
                            Identifier, 
                            DisplayText, 
                            DisplayIndex 
                        FROM 
                            Music 
                        Order By DisplayIndex
                    </SQL>.Value,
                    .Connection = cn
                    }

                    cn.Open()

                    dt.Load(cmd.ExecuteReader)
                    dt.AcceptChanges()

                End Using
            End Using

            Return dt

        End Function
        Public Function LoadCustomersAccessForm() As DataTable
            Using cn As New OleDbConnection With
                {
                .ConnectionString = BuilderAccdb.ConnectionString
                }
                Using cmd As New OleDbCommand With {.Connection = cn}
                    cmd.CommandText =
                        <SQL>
                        SELECT TOP 10 
                            Identifier, 
                            CompanyName, 
                            ContactName, 
                            ContactTitle,
                            RowPosition
                        FROM 
                            Customer 
                        Order By RowPosition
                    </SQL>.Value

                    Dim dt As New DataTable

                    cn.Open()

                    dt.Load(cmd.ExecuteReader)

                    dt.Columns("Identifier").ColumnMapping = MappingType.Hidden
                    dt.Columns("RowPosition").ColumnMapping = MappingType.Hidden

                    dt.AcceptChanges()

                    Return dt

                End Using
            End Using
        End Function
        Public Function LoadCustomersSqlServerForm() As DataTable
            Using cn As New OleDbConnection With
                {
                .ConnectionString = BuilderAccdb.ConnectionString
                }
                Using cmd As New OleDbCommand With {.Connection = cn}
                    cmd.CommandText =
                        <SQL>
                        SELECT TOP 10 
                            CustomerIdentifier, 
                            CompanyName, 
                            ContactName, 
                            ContactTitle,
                            RowPosition
                        FROM 
                            Customer 
                        Order By RowPosition
                    </SQL>.Value

                    Dim dt As New DataTable
                    cn.Open()
                    dt.Load(cmd.ExecuteReader)
                    dt.Columns("CustomerIdentifier").ColumnMapping = MappingType.Hidden
                    dt.Columns("RowPosition").ColumnMapping = MappingType.Hidden
                    dt.AcceptChanges()

                    Return dt

                End Using
            End Using
        End Function
        Public Sub UpdatePositionMsAccess(dt As DataTable)
            Using cn As New OleDbConnection With
                {
                .ConnectionString = BuilderAccdb.ConnectionString
                }
                Using cmd As New OleDbCommand With {.Connection = cn}
                    cmd.CommandText =
                        <SQL>
                        UPDATE Customer 
                        SET Customer.RowPosition = @P1
                        WHERE (((Customer.Identifier)=@P2));
                    </SQL>.Value

                    cmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@P1", .OleDbType = OleDbType.Integer})
                    cmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@P2", .OleDbType = OleDbType.Integer})
                    cn.Open()

                    Dim Position As Integer = 0

                    For rowIndex As Integer = 0 To dt.Rows.Count - 1
                        Position = rowIndex + 1
                        cmd.Parameters("@P1").Value = Position
                        cmd.Parameters("@P2").Value = dt.Rows(rowIndex).Field(Of Integer)("Identifier")
                        cmd.ExecuteNonQuery()
                    Next
                End Using
            End Using
        End Sub
        Public Sub UpdatePositionSqlServer(dt As DataTable)
            Using cn As New OleDb.OleDbConnection With
                {
                .ConnectionString = BuilderAccdb.ConnectionString
                }
                Using cmd As New OleDbCommand With {.Connection = cn}
                    cmd.CommandText =
                        <SQL>
                        UPDATE Customer 
                        SET Customer.RowPosition = @P1
                        WHERE CustomerIdentifier=@P2;
                    </SQL>.Value

                    cmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@P1", .OleDbType = OleDbType.Integer})
                    cmd.Parameters.Add(New OleDbParameter With {.ParameterName = "@P2", .OleDbType = OleDbType.Integer})

                    cn.Open()

                    Dim Position As Integer = 0
                    For rowIndex As Integer = 0 To dt.Rows.Count - 1
                        Position = rowIndex + 1
                        cmd.Parameters("@P1").Value = Position
                        cmd.Parameters("@P2").Value = dt.Rows(rowIndex).Field(Of Integer)("CustomerIdentifier")
                        cmd.ExecuteNonQuery()
                    Next
                End Using
            End Using
        End Sub
        ''' <summary>
        ''' Used to update DisplayIndex for all rows
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DoListBoxUpdates(dt As DataTable)
            Using cn As New OleDbConnection With {.ConnectionString = BuilderAccdb.ConnectionString}

                cn.Open()

                Using cmd As New OleDbCommand With {.Connection = cn}
                    cmd.CommandText =
                        <SQL>
                        Update Music
                        SET DisplayIndex=P1
                        WHERE Identifier=P2
                    </SQL>.Value

                    Dim P1 As New OleDbParameter With {.DbType = DbType.Int32}
                    Dim P2 As New OleDbParameter With {.DbType = DbType.Int32}

                    cmd.Parameters.AddRange(New OleDbParameter() {P1, P2})

                    For rowIndex As Integer = 0 To dt.Rows.Count - 1
                        P1.Value = dt.Rows(rowIndex).Item("DisplayIndex")
                        P2.Value = dt.Rows(rowIndex).Item("Identifier")
                        cmd.ExecuteNonQuery()
                    Next
                End Using
            End Using
        End Sub
        Public Function LoadCustomersTextFileForm() As DataTable
            Dim dt As New DataTable

            dt.Columns.Add(New DataColumn With {.ColumnName = "Identifier", .DataType = GetType(String),
                              .ColumnMapping = MappingType.Hidden})

            dt.Columns.Add(New DataColumn With {.ColumnName = "CompanyName", .DataType = GetType(String)})
            dt.Columns.Add(New DataColumn With {.ColumnName = "ContactName", .DataType = GetType(String)})
            dt.Columns.Add(New DataColumn With {.ColumnName = "ContactTitle", .DataType = GetType(String)})

            Dim Lines = IO.File.ReadAllLines(Path.Combine(Application.StartupPath, "Data.txt"))

            For Each line In Lines
                dt.Rows.Add(line.Split(",".ToCharArray))
            Next

            Return dt

        End Function
    End Class
End Namespace