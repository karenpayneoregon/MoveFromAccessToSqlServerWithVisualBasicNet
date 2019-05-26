Imports System.Data.OleDb

Namespace Classes
    Public Class DataOperationsAccess
        Private ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=NorthWind.accdb"
        Public LastException As Exception

        Public Function LoadContactTypes() As List(Of ContactType)
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

            Return contactTitleList

        End Function

    End Class
End Namespace