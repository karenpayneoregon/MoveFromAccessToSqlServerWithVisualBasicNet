Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MainForm
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.cmdFromAccessDatabase = New System.Windows.Forms.Button()
            Me.cmdFromTextFile = New System.Windows.Forms.Button()
            Me.cmdListBoxExample = New System.Windows.Forms.Button()
            Me.cmdFromSqlServerDatabase = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'cmdFromAccessDatabase
            '
            Me.cmdFromAccessDatabase.Location = New System.Drawing.Point(51, 35)
            Me.cmdFromAccessDatabase.Name = "cmdFromAccessDatabase"
            Me.cmdFromAccessDatabase.Size = New System.Drawing.Size(194, 70)
            Me.cmdFromAccessDatabase.TabIndex = 0
            Me.cmdFromAccessDatabase.Text = "MS-Access Database/DataGridView"
            Me.cmdFromAccessDatabase.UseVisualStyleBackColor = True
            '
            'cmdFromTextFile
            '
            Me.cmdFromTextFile.Location = New System.Drawing.Point(51, 187)
            Me.cmdFromTextFile.Name = "cmdFromTextFile"
            Me.cmdFromTextFile.Size = New System.Drawing.Size(194, 70)
            Me.cmdFromTextFile.TabIndex = 1
            Me.cmdFromTextFile.Text = "TextFile/DataGridView"
            Me.cmdFromTextFile.UseVisualStyleBackColor = True
            '
            'cmdListBoxExample
            '
            Me.cmdListBoxExample.Location = New System.Drawing.Point(51, 272)
            Me.cmdListBoxExample.Name = "cmdListBoxExample"
            Me.cmdListBoxExample.Size = New System.Drawing.Size(194, 70)
            Me.cmdListBoxExample.TabIndex = 2
            Me.cmdListBoxExample.Text = "MS-AccessDatabase/ListBox"
            Me.cmdListBoxExample.UseVisualStyleBackColor = True
            '
            'cmdFromSqlServerDatabase
            '
            Me.cmdFromSqlServerDatabase.Location = New System.Drawing.Point(51, 111)
            Me.cmdFromSqlServerDatabase.Name = "cmdFromSqlServerDatabase"
            Me.cmdFromSqlServerDatabase.Size = New System.Drawing.Size(194, 70)
            Me.cmdFromSqlServerDatabase.TabIndex = 3
            Me.cmdFromSqlServerDatabase.Text = "SQL-Server Database/DataGridView"
            Me.cmdFromSqlServerDatabase.UseVisualStyleBackColor = True
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(297, 366)
            Me.Controls.Add(Me.cmdFromSqlServerDatabase)
            Me.Controls.Add(Me.cmdListBoxExample)
            Me.Controls.Add(Me.cmdFromTextFile)
            Me.Controls.Add(Me.cmdFromAccessDatabase)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Name = "MainForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Demo"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents cmdFromAccessDatabase As System.Windows.Forms.Button
        Friend WithEvents cmdFromTextFile As System.Windows.Forms.Button
        Friend WithEvents cmdListBoxExample As System.Windows.Forms.Button
        Friend WithEvents cmdFromSqlServerDatabase As Button
    End Class
End Namespace