Imports System.Data.OleDb

Namespace Classes
    Public Class DataOperationsAccess
        Private ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=NorthWind.accdb"
        Public LastException As Exception

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