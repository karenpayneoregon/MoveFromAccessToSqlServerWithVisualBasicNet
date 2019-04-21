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
            Me.cmdFromDatabase = New System.Windows.Forms.Button()
            Me.cmdFromTextFile = New System.Windows.Forms.Button()
            Me.cmdListBoxExample = New System.Windows.Forms.Button()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'cmdFromDatabase
            '
            Me.cmdFromDatabase.Location = New System.Drawing.Point(51, 35)
            Me.cmdFromDatabase.Name = "cmdFromDatabase"
            Me.cmdFromDatabase.Size = New System.Drawing.Size(194, 70)
            Me.cmdFromDatabase.TabIndex = 0
            Me.cmdFromDatabase.Text = "MS-Access Database/DataGridView"
            Me.cmdFromDatabase.UseVisualStyleBackColor = True
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
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(51, 111)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(194, 70)
            Me.Button1.TabIndex = 3
            Me.Button1.Text = "SQL-Server Database/DataGridView"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(297, 366)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.cmdListBoxExample)
            Me.Controls.Add(Me.cmdFromTextFile)
            Me.Controls.Add(Me.cmdFromDatabase)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Name = "MainForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Demo"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents cmdFromDatabase As System.Windows.Forms.Button
        Friend WithEvents cmdFromTextFile As System.Windows.Forms.Button
        Friend WithEvents cmdListBoxExample As System.Windows.Forms.Button
        Friend WithEvents Button1 As Button
    End Class
End Namespace