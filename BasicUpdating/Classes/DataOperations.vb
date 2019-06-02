Imports System.Data.SqlClient

Namespace Classes
    ''' <summary>
    ''' This class provides the basics to add a new customer record. In a future part of  this
    ''' series there will be several improvements ranging from how the connection string is created
    ''' to better exception handling
    ''' </summary>
    Public Class DataOperations
        ''' <summary>
        ''' Data Source must match your server name of if using Express edition .\SQLEXPRESS
        ''' </summary>
        Private ConnectionString As String = "Data Source=KARENS-PC;Initial Catalog=NorthWindAzureForInserts;Integrated Security=True"

        Public LastException As Exception
        Public HasException As Boolean
        Public ReadOnly Property IsSuccessful() As Boolean
            Get
                Return HasException = False
            End Get
        End Property
        ''' <summary>
        ''' Insert a new customer record
        ''' </summary>
        ''' <param name="pCustomer">Customer instance</param>
        Public Sub InsertRecord(pCustomer As Customer)

            HasException = False

            ' statement was created in SSMS excluding the ending select which returns the new primary key
            Dim insertStatement =
                    "INSERT INTO dbo.Customers " &
                    "(CompanyName,ContactName,[Address],City,PostalCode,CountryIdentifier,ContactTypeIdentifier) " &
                    "VALUES (@CompanyName,@ContactName,@Street,@City,@PostalCode,@CountryIdentifier,@ContactTypeIdentifier);" &
                    "SELECT CAST(scope_identity() AS int);"


            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn, .CommandText = insertStatement}

                    cmd.Parameters.AddWithValue("@CompanyName", pCustomer.CompanyName)
                    cmd.Parameters.AddWithValue("@ContactName", pCustomer.ContactName)
                    cmd.Parameters.AddWithValue("@Street", pCustomer.Address)
                    cmd.Parameters.AddWithValue("@City", pCustomer.City)
                    cmd.Parameters.AddWithValue("@PostalCode", pCustomer.PostalCode)
                    cmd.Parameters.AddWithValue("@CountryIdentifier", pCustomer.CountryIdentifier)
                    cmd.Parameters.AddWithValue("@ContactTypeIdentifier", pCustomer.ContactTypeIdentifier)

                    Try

                        cn.Open()

                        pCustomer.CustomerIdentifier = CInt(cmd.ExecuteScalar())

                    Catch ex As Exception
                        HasException = True
                        LastException = ex
                    End Try
                End Using
            End Using
        End Sub
        Public Function LoadCustomerRecordsUsingDataTable() As DataTable

            Dim selectStatement =
                    <SQL>
                        SELECT Cust.CustomerIdentifier ,
                               Cust.CompanyName ,
                               Cust.ContactId ,
                               Contacts.FirstName ,
                               Contacts.LastName ,
                               Cust.ContactTypeIdentifier ,
                               CT.ContactTitle ,
                               Cust.[Address] AS Street ,
                               Cust.City ,
                               Cust.PostalCode ,
                               Cust.CountryIdentifier ,
                               Countries.Name AS CountryName ,
                               Cust.ModifiedDate
                        FROM   dbo.Customers AS Cust
                               INNER JOIN dbo.ContactType AS CT ON Cust.ContactTypeIdentifier = CT.ContactTypeIdentifier
                               INNER JOIN dbo.Contacts ON Cust.ContactId = Contacts.ContactId
                               INNER JOIN dbo.Countries ON Cust.CountryIdentifier = Countries.CountryIdentifier;
                    </SQL>.Value

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
                contactTitleList.Insert(0,
                                        New ContactType() With
                                           {
                                           .ContactTypeIdentifier = 0,
                                           .ContactTitle = "Select"
                                           })
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
                countryList.Insert(0,
                                   New Country() With
                                      {
                                      .CountryIdentifier = 0,
                                      .Name = "Select"
                                      })
            End If

            Return countryList

        End Function
        ''' <summary>
        ''' Load contact names from reference table
        ''' </summary>
        ''' <returns></returns>
        Public Function LoadContacts() As List(Of Contact)
            Dim countryList = New List(Of Contact)
            Dim selectStatement = "SELECT ContactId,FirstName,LastName FROM dbo.Contacts;"

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    Try
                        cmd.CommandText = selectStatement

                        cn.Open()

                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            countryList.Add(
                                New Contact() With
                                               {
                                               .ContactId = reader.GetInt32(0),
                                               .FirstName = reader.GetString(1),
                                               .LastName = reader.GetString(2)
                                               })
                        End While

                    Catch ex As Exception
                        LastException = ex
                    End Try
                End Using
            End Using

            Return countryList

        End Function

    End Class
End Namespace