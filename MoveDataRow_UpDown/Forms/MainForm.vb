Namespace Forms
    Public Class MainForm

        Private Sub cmdFromDatabase_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdFromDatabase.Click
            Dim f As New frmAccessForm

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