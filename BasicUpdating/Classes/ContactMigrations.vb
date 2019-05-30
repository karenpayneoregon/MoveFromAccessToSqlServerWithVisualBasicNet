Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports BaseConnectionLibrary.Classes

Namespace Classes
    ''' <summary>
    ''' Note BaseExceptionProperties is not used but kept here in the
    ''' event there were problems not seen in SSMS (see comments below)
    ''' </summary>
    Public Class ContactMigrations
        Inherits BaseExceptionProperties

        Private AccessConnectionString As String =
                    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=NorthWind.accdb"

        Private SqlServerConnectionString As String =
                    "Data Source=KARENS-PC;Initial Catalog=NorthWindAzureForInserts;Integrated Security=True"
        ''' <summary>
        ''' No frills method to move contact name from MS-Access database Customers table to
        ''' a new SQL-Server table were afterwards in this case there may need to be manual 
        ''' edits as some contact names have a middle name or a two word last name.
        ''' 
        ''' Note there is a connection for both databases and the names of the connections are
        ''' important to keep things easy to understand and not mess things up and the same
        ''' goes for the command objects.
        ''' 
        ''' Also note for the insert operation parameters are add once rather than use
        ''' AddWithValue, this is the best way to perform this operation were may new developers
        ''' tend to use AddWithValue, clear parameters and add them back in or even worst recreate
        ''' the connection and entire command for each iteration of the while statement which sure
        ''' works but is extra work in the end.
        ''' </summary>
        Public Sub MoveFromAccessToSqlServer()

            Dim accessCustomerContactsSelectStatement = "SELECT ContactName FROM Customers;"
            Dim sqlServerInsertContactStatement = "INSERT INTO dbo.Contacts (FirstName,LastName) VALUES (@FirstName,@LastName)"

            Using cnAccess As New OleDbConnection With {.ConnectionString = AccessConnectionString}
                Using cmdAccess As New OleDbCommand With {.Connection = cnAccess, .CommandText = accessCustomerContactsSelectStatement}

                    cnAccess.Open()
                    Dim reader = cmdAccess.ExecuteReader()

                    Using cnServer As New SqlConnection With {.ConnectionString = SqlServerConnectionString}
                        Using cmdServer As New SqlCommand With {.Connection = cnServer, .CommandText = sqlServerInsertContactStatement}

                            cmdServer.Parameters.Add(New SqlParameter() With {.ParameterName = "@FirstName", .DbType = DbType.String})
                            cmdServer.Parameters.Add(New SqlParameter() With {.ParameterName = "@LastName", .DbType = DbType.String})

                            cnServer.Open()

                            While reader.Read()
                                Dim parts = reader.GetString(0).Split(" "c)
                                cmdServer.Parameters("@FirstName").Value = parts(0)
                                If parts.Length > 2 Then
                                    cmdServer.Parameters("@LastName").Value = $"{parts(1)} {parts(2)}"
                                Else
                                    cmdServer.Parameters("@LastName").Value = parts(1)
                                End If

                                '
                                ' No need to check result, do that in SSMS
                                ' If there is an issue, low chance we can visually see in SSMS
                                '
                                cmdServer.ExecuteNonQuery()

                            End While
                        End Using
                    End Using
                End Using
            End Using
        End Sub
    End Class
End Namespace