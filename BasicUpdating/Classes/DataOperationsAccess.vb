Imports System.Data.OleDb

Namespace Classes
    Public Class DataOperationsAccess
        Private ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=NorthWind.accdb"
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
        ''' <remarks>
        ''' To get the new primary key first a INSERT statement inserts data
        ''' then an additional SELECT statement is required to get the new primary
        ''' key. With SQL-Server both the INSERT and SELECT are done together, 
        ''' cmd.ExecuteScalar() is used in SQL-Server to get the new key while
        ''' for this operation cmd.ExecuteNonQuery() is done followed by cmd.ExecuteScalar()
        ''' </remarks>
        Public Sub InsertRecord(pCustomer As Customer)

            HasException = False

            Dim insertStatement = "INSERT INTO Customers " &
                                  "(CompanyName,ContactName,[Address],City,PostalCode,CountryIdentifier,ContactTypeIdentifier) " &
                                  "VALUES (@CompanyName,@ContactName,@Street,@City,@PostalCode,@CountryIdentifier,@ContactTypeIdentifier);"


            Using cn As New OleDbConnection With {.ConnectionString = ConnectionString}
                Using cmd As New OleDbCommand With {.Connection = cn, .CommandText = insertStatement}

                    cmd.Parameters.AddWithValue("@CompanyName", pCustomer.CompanyName)
                    cmd.Parameters.AddWithValue("@ContactName", pCustomer.ContactName)
                    cmd.Parameters.AddWithValue("@Street", pCustomer.Address)
                    cmd.Parameters.AddWithValue("@City", pCustomer.City)
                    cmd.Parameters.AddWithValue("@PostalCode", pCustomer.PostalCode)
                    cmd.Parameters.AddWithValue("@CountryIdentifier", pCustomer.CountryIdentifier)
                    cmd.Parameters.AddWithValue("@ContactTypeIdentifier", pCustomer.ContactTypeIdentifier)

                    Try

                        cn.Open()

                        Dim result = cmd.ExecuteNonQuery()
                        If result = 1 Then
                            cmd.CommandText = "Select @@Identity"
                            pCustomer.CustomerIdentifier = CInt(cmd.ExecuteScalar())
                        Else
                            pCustomer.CustomerIdentifier = -1
                        End If

                    Catch ex As Exception
                        HasException = True
                        LastException = ex
                    End Try
                End Using
            End Using
        End Sub
        ''' <summary>
        ''' Load contact type reference table
        ''' </summary>
        ''' <param name="pUseSelect">True add "Select" as first time</param>
        ''' <returns></returns>
        Public Function LoadContactTypes(Optional pUseSelect As Boolean = False) As List(Of ContactType)
            Dim contactTitleList = New List(Of ContactType)
            Dim selectStatement = "SELECT CT.ContactTypeIdentifier, CT.ContactTitle FROM ContactType AS CT;"

            Using cn As New OleDbConnection With {.ConnectionString = ConnectionString}
                Using cmd As New OleDbCommand With {.Connection = cn}
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
            Dim selectStatement = "SELECT CountryIdentifier, Country FROM Countries;"

            Using cn As New OleDbConnection With {.ConnectionString = ConnectionString}
                Using cmd As New OleDbCommand With {.Connection = cn}
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