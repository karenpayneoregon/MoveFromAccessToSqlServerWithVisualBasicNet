<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MigrateContactsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.migrateButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'migrateButton
        '
        Me.migrateButton.Location = New System.Drawing.Point(13, 21)
        Me.migrateButton.Name = "migrateButton"
        Me.migrateButton.Size = New System.Drawing.Size(172, 23)
        Me.migrateButton.TabIndex = 0
        Me.migrateButton.Text = "Test run get first last names"
        Me.migrateButton.UseVisualStyleBackColor = True
        '
        'MigrateContactsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(208, 92)
        Me.Controls.Add(Me.migrateButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MigrateContactsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Contact migration"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents migrateButton As Button
End Class
