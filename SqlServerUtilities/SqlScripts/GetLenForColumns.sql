DECLARE @SQL VARCHAR(MAX)
DECLARE @TableName sysname

SET @TableName = 'Customers'

SET @SQL = ''

SELECT @SQL = @SQL + 'SELECT ' + QUOTENAME(sc.name, '''') + ' AS ColumnName, ' + QUOTENAME(t.name, '''') + ' AS DataType, ' +

QUOTENAME(sc.max_length, '''') + ' AS SetLength, MAX(DATALENGTH(' + QUOTENAME(sc.name) + ')) AS MaxLength FROM '+@TableName+ char(10) +' UNION '

FROM sys.columns sc

join sys.types t on t.system_type_id = sc.system_type_id and t.name != 'sysname'

WHERE sc.OBJECT_ID = OBJECT_ID(@TableName)

SET @SQL = LEFT(@SQL, LEN(@SQL)-6)

PRINT @SQL

EXEC(@SQL)