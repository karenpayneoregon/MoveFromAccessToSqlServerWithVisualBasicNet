Namespace Forms
    Public Class MainForm
        ''' <summary>
        ''' Shows move/up down rows for MS-Access
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub cmdFromAccessDatabase_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdFromAccessDatabase.Click
            Dim f As New frmAccessForm

            Try
                f.ActiveControl = f.DataGridView1
                f.ShowDialog()
            Finally
                f.Dispose()
            End Try
        End Sub
        ''' <summary>
        '''  Shows move/up down rows for SQL-Server
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub cmdFromSqlServerDatabase_Click(sender As Object, e As EventArgs) Handles cmdFromSqlServerDatabase.Click

            Dim f As New SqlServerForm

            Try
                f.ActiveControl = f.DataGridView1
                f.ShowDialog()
            Finally
                f.Dispose()
            End Try
        End Sub
        Private Sub cmdFromTextFile_Click(sender As Object, e As EventArgs) Handles cmdFromTextFile.Click
            Dim f As New frmTextFileForm

            Try
                f.ActiveControl = f.DataGridView1
                f.ShowDialog()
            Finally
                f.Dispose()
            End Try
        End Sub

        Private Sub cmdListBoxExample_Click(sender As Object, e As EventArgs) Handles cmdListBoxExample.Click
            Dim f As New frmListBoxForm

            Try
                f.ActiveControl = f.ListBox1
                f.ShowDialog()
            Finally
                f.Dispose()
            End Try
        End Sub
    End Class

End Namespace