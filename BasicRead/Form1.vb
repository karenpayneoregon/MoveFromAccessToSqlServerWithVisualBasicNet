Imports System.Data.SqlClient

Public Class Form1
    Private ConnectionString As String = "TODO"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim selectStatement = "SELECT Codearticle " &
                              "FROM DetailReceptionFrs " &
                              "WHERE Codearticle = @Codearticle"

        Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
            Using cmd As New SqlCommand With {.Connection = cn}

                cmd.CommandText = selectStatement

                cmd.Parameters.Add(New SqlParameter() With
                        {
                            .ParameterName = "@Codearticle",
                            .DbType = DbType.String
                         })

                cn.Open()

                For rowIndex As Integer = 0 To DataGridView1.Rows.Count - 1

                    cmd.Parameters("@Codearticle").Value =
                        CStr(DataGridView1.Rows(rowIndex).Cells("Article").Value)

                    Dim reader = cmd.ExecuteReader()

                    If reader.HasRows Then
                        ' Form2.Show
                        Exit Sub
                    Else

                        DataGridView1.Rows(rowIndex).DefaultCellStyle.BackColor = Color.Red
                    End If

                    If Not reader.IsClosed Then
                        reader.Close()
                    End If

                Next
            End Using
        End Using
    End Sub
End Class