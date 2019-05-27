Imports System.Data.SqlClient
Imports BaseConnectionLibrary.ConnectionClasses

''' <summary>
''' Contains helper code not intended for running in an application
''' but instead provides quick access to table information when coding.
''' </summary>
Public Class DataOperations
    Inherits SqlServerConnection
    Public Function DatabaseTableStatistics() As DataTable
        DatabaseServer = "KARENS-PC"
        DefaultCatalog = "NorthWindAzureForInserts"
        Dim tableName As String = "Customers"

        mHasException = False

        Dim selectStatement =
                <SQL>
                SELECT syso.name [Table],
                      sysc.name [Field], 
                      sysc.colorder [FieldOrder], 
                      syst.name [DataType], 
                      sysc.[length] [Length], 
                      sysc.prec [Precision], 
                CASE WHEN sysc.scale IS null THEN '-' ELSE sysc.scale END [Scale], 
                CASE WHEN sysc.isnullable = 1 THEN 'True' ELSE 'False' END [AllowNulls?], 
                CASE WHEN sysc.[status] = 128 THEN 'True' ELSE 'False' END [Identity?], 
                CASE WHEN sysc.colstat = 1 THEN 'True' ELSE 'False' END [PrimaryKey?],
                CASE WHEN fkc.parent_object_id is null THEN 'False' ELSE 'True' END [ForeignKey?], 
                CASE WHEN fkc.parent_object_id is null THEN '-' ELSE obj.name  END [RelatedTable],
                CASE WHEN ep.value is NULL THEN '-' ELSE CAST(ep.value as NVARCHAR(500)) END [Description]
                FROM [sys].[sysobjects] AS syso
                JOIN [sys].[syscolumns] AS sysc on syso.id = sysc.id
                LEFT JOIN [sys].[systypes] AS syst ON sysc.xtype = syst.xtype and syst.name != 'sysname'
                LEFT JOIN [sys].[foreign_key_columns] AS fkc on syso.id = fkc.parent_object_id and sysc.colid = fkc.parent_column_id    
                LEFT JOIN [sys].[objects] AS obj ON fkc.referenced_object_id = obj.[object_id]
                LEFT JOIN [sys].[extended_properties] AS ep ON syso.id = ep.major_id and sysc.colid = ep.minor_id and ep.name = 'MS_Description'
                WHERE syso.type = 'U' AND  syso.name != 'sysdiagrams'
                ORDER BY [Table], FieldOrder, Field
                </SQL>.Value

        Dim dtResults = New DataTable
        Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
            Using cmd As New SqlCommand With {.Connection = cn, .CommandText = selectStatement}
                Try
                    cn.Open()
                    dtResults.Load(cmd.ExecuteReader())
                Catch ex As Exception
                    mHasException = True
                    mLastException = ex
                End Try
            End Using
        End Using

        Return dtResults

    End Function

End Class
