Imports BasicUpdating.Classes

Public Class MigrateContactsForm
    Private Sub migrate_Click(sender As Object, e As EventArgs) Handles migrateButton.Click
        MessageBox.Show("Be sure before running this it has not already been executed.")
        'Dim ops = New ContactMigrations
        'ops.MoveFromAccessToSqlServer()
    End Sub

End Class
