Imports BasicInsertNewRecord.Classes

Public Class MainForm
    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        LoadReferenceTable()
    End Sub
    Public Sub LoadReferenceTable()
        Dim accessOps = New DataOperationsAccess
        customerContactTypeAccessComboBox.DataSource = accessOps.LoadContactTypes(True)
        countryCodeAccessComboBox.DataSource = accessOps.LoadCountries(True)
    End Sub
End Class
