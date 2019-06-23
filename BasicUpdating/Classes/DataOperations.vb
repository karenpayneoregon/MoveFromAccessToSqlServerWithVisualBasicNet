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
        Public Function CustomerSelectStatement() As String
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
                               INNER JOIN dbo.Countries ON Cust.CountryIdentifier = Countries.CountryIdentifier
                    </SQL>.Value
            Return selectStatement
        End Function
        Public Function LoadCustomerRecordsUsingDataTable() As DataTable
            Dim customerDataTable = New DataTable

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    Try
                        cmd.CommandText = CustomerSelectStatement()

                        cn.Open()

                        customerDataTable.Load(cmd.ExecuteReader())

                        customerDataTable.Columns("CustomerIdentifier").ColumnMapping = MappingType.Hidden
                        customerDataTable.Columns("ContactTypeIdentifier").ColumnMapping = MappingType.Hidden
                        customerDataTable.Columns("ModifiedDate").ColumnMapping = MappingType.Hidden
                    Catch ex As Exception
                        HasException = True
                        LastException = ex
                    End Try
                End Using
            End Using

            Return customerDataTable

        End Function
        Public ReadOnly Property UpdateStatement() As String
            Get
                Return _
                <SQL>
                    UPDATE [dbo].[Customers]
                       SET [CompanyName] = @CompanyName
                          ,[ContactId] = @ContactId
                          ,[Address] = @Street
                          ,[City] = @City
                          ,[PostalCode] = @PostalCode
                          ,[CountryIdentifier] = @CountryIdentifier
                          ,[ContactTypeIdentifier] = @ContactTypeIdentifier
                     WHERE CustomerIdentifier = @CustomerIdentifier
                </SQL>.Value
            End Get
        End Property
        Public Function UpdateContactRecord(pContact As Contact) As UpdateResult
            HasException = False
            Dim selectStatement = "SELECT COUNT(ContactId) FROM NorthWindAzureForInserts.dbo.Contacts WHERE ContactId = @ContactId"

            Dim updateStatement = "UPDATE dbo.Contacts SET FirstName = @FirstName,LastName = @LastName WHERE ContactId = @ContactId"

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    cmd.CommandText = selectStatement

                    Try

                        cn.Open()

                        cmd.Parameters.AddWithValue("@ContactId", pContact.ContactId)
                        Dim count = CInt(cmd.ExecuteScalar())

                        If count = 1 Then

                            cmd.Parameters.Clear()

                            cmd.Parameters.AddWithValue("@FirstName", pContact.FirstName)
                            cmd.Parameters.AddWithValue("@LastName", pContact.LastName)
                            cmd.Parameters.AddWithValue("@ContactId", pContact.ContactId)

                            cmd.CommandText = updateStatement

                            Dim test = cmd.ExecuteNonQuery()

                            Return UpdateResult.Success

                        Else
                            Return UpdateResult.NotFound
                        End If
                    Catch ex As Exception
                        HasException = True
                        LastException = ex
                        Return UpdateResult.Failed
                    End Try
                End Using
            End Using
        End Function
        Public Function UpdateCurrentCustomerRecord(pDataRow As DataRow) As UpdateResult
            HasException = False

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    Try

                        ' no parameter, if this were a web app we need a parameter to prevent SQL injection
                        cmd.CommandText = $"{CustomerSelectStatement()} WHERE Cust.CustomerIdentifier = {pDataRow.Field(Of Integer)("CustomerIdentifier")}"

                        cn.Open()
                        Dim reader = cmd.ExecuteReader()

                        If reader.HasRows Then

                            reader.Read()

                            '
                            ' Compare current record in the caller (form) to the current record
                            ' in the database.
                            '
                            Dim values(reader.FieldCount - 1) As Object
                            Dim fieldCount As Integer = reader.GetValues(values)
                            Dim readValues = String.Join(",", values)
                            Dim currentValues = String.Join(",", pDataRow.ItemArray)

                            If String.Join(",", values) <> String.Join(",", pDataRow.ItemArray) Then

                                reader.Close()

                                cmd.CommandText = UpdateStatement()

                                cmd.Parameters.AddWithValue("@CompanyName", pDataRow.Field(Of String)("CompanyName"))

                                cmd.Parameters.AddWithValue("@ContactId", pDataRow.Field(Of Integer)("ContactId"))
                                cmd.Parameters.AddWithValue("@Street", pDataRow.Field(Of String)("Street"))
                                cmd.Parameters.AddWithValue("@City", pDataRow.Field(Of String)("City"))
                                cmd.Parameters.AddWithValue("@PostalCode", pDataRow.Field(Of String)("PostalCode"))
                                cmd.Parameters.AddWithValue("@CountryIdentifier", pDataRow.Field(Of Integer)("CountryIdentifier"))
                                cmd.Parameters.AddWithValue("@ContactTypeIdentifier", pDataRow.Field(Of Integer)("ContactTypeIdentifier"))
                                cmd.Parameters.AddWithValue("@CustomerIdentifier", pDataRow.Field(Of Integer)("CustomerIdentifier"))

                                cmd.ExecuteNonQuery()
                                Return UpdateResult.Success
                            Else
                                Return UpdateResult.NoChanges
                            End If
                        Else
                            Return UpdateResult.NotFound
                        End If
                    Catch ex As Exception
                        HasException = True
                        LastException = ex
                        Return UpdateResult.Failed
                    End Try
                End Using
            End Using
        End Function
        ''' <summary>
        ''' Remove Customer record by customer identifier, in this case
        ''' from a prompt when pressing the delete button in a BindingNavigator
        ''' </summary>
        ''' <param name="pCustomerIdentifier"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' In this case when dealing with child tables, orders and order details
        ''' the delete rules must be set to cascading. This is done easy by creating
        ''' a database diagram in SSMS, select the relations and then properties,
        ''' set the delete rule to cascade.
        ''' </remarks>
        Public Function RemoveRecord(pCustomerIdentifier As Integer) As Boolean
            HasException = False

            Dim selectStatement = "DELETE FROM dbo.Customers WHERE CustomerIdentifier = @CustomerIdentifier"

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    Try
                        cmd.CommandText = selectStatement
                        cmd.Parameters.AddWithValue("@CustomerIdentifier", pCustomerIdentifier)
                        cn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        HasException = True
                        LastException = ex
                    End Try
                End Using
            End Using

            Return IsSuccessful
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