'https://stackoverflow.com/questions/17469349/mapping-columns-in-a-datatable-to-a-sql-table-with-sqlbulkcopy/31909560
Imports System.Data.SqlClient

Public Class NameOfYourClassGoesHere
    Private ConnectionString As String =
                "Data Source=TODO;Initial Catalog=TODO;Integrated Security=True"
    Public LastException As Exception
    Public HasException As Boolean
    Public ReadOnly Property IsSuccessful() As Boolean
        Get
            Return HasException = False
        End Get
    End Property

    Public Sub UpdateDriversLicence(dt As DataTable)
        Try
            Using cn As New SqlConnection(ConnectionString)
                cn.Open()
                Using sbc As New SqlBulkCopy(cn)
                    sbc.AutoMapColumns(dt)
                    sbc.DestinationTableName = "TODO"
                    sbc.WriteToServer(dt)
                End Using
            End Using
        Catch ex As Exception
            HasException = True
            LastException = ex
        End Try

    End Sub
End Class
Public Module SqlExtensions
    ''' <summary>
    ''' Used to map columns where the ordinal position of columns
    ''' match between the DataTable and the SQL-Server Table.
    ''' </summary>
    ''' <param name="sbc"></param>
    ''' <param name="dt"></param>
    <Runtime.CompilerServices.Extension>
    Public Sub AutoMapColumns(sbc As SqlBulkCopy, dt As DataTable)
        For Each column As DataColumn In dt.Columns
            sbc.ColumnMappings.Add(column.ColumnName, column.ColumnName)
        Next
    End Sub
End Module