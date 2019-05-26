Imports System.Data.SqlClient

Namespace Classes
    Public Class DataOperationsSqlServer
        Private ConnectionString As String = "Data Source=KARENS-PC;Initial Catalog=NorthWindAzureForInserts;Integrated Security=True"
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
        ''' <summary>
        ''' Load contact type reference table
        ''' </summary>
        ''' <param name="pUseSelect">True add "Select" as first time</param>
        ''' <returns></returns>
        Public Function LoadContactTypes(Optional pUseSelect As Boolean = False) As List(Of ContactType)
            Dim contactTitleList = New List(Of ContactType)
            Dim selectStatement = "SELECT ContactTypeIdentifier,ContactTitle FROM dbo.ContactType;"

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    Try
                        cmd.CommandText = selectStatement

                        cn.Open()
                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            contactTitleList.Add(
                                New ContactType() With
                                                    {
                                                    .ContactTypeIdentifier = reader.GetInt32(0),
                                                    .ContactTitle = reader.GetString(1)
                                                    })
                        End While

                    Catch ex As Exception
                        LastException = ex
                    End Try
                End Using
            End Using

            If pUseSelect Then
                contactTitleList.Insert(0, New ContactType() With {.ContactTypeIdentifier = 0, .ContactTitle = "Select"})
            End If

            Return contactTitleList

        End Function
        ''' <summary>
        ''' Load country reference table
        ''' </summary>
        ''' <param name="pUseSelect">True add "Select" as first time</param>
        ''' <returns></returns>
        Public Function LoadCountries(Optional pUseSelect As Boolean = False) As List(Of Country)
            Dim countryList = New List(Of Country)
            Dim selectStatement = "SELECT CountryIdentifier, [Name] FROM Countries;"

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    Try
                        cmd.CommandText = selectStatement

                        cn.Open()
                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            countryList.Add(
                                New Country() With
                                               {
                                               .CountryIdentifier = reader.GetInt32(0),
                                               .Name = reader.GetString(1)
                                               })
                        End While

                    Catch ex As Exception
                        LastException = ex
                    End Try
                End Using
            End Using

            If pUseSelect Then
                countryList.Insert(0, New Country() With {.CountryIdentifier = 0, .Name = "Select"})
            End If

            Return countryList

        End Function

    End Class
End Namespace