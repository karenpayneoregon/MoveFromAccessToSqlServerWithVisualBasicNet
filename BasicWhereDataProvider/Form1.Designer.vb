<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.customersAccessDataGridView = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.customersSqlServerDataGridView = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.closeApplicationButton = New System.Windows.Forms.Button()
        CType(Me.customersAccessDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.customersSqlServerDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'customersAccessDataGridView
        '
        Me.customersAccessDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.customersAccessDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.customersAccessDataGridView.Location = New System.Drawing.Point(3, 16)
        Me.customersAccessDataGridView.Name = "customersAccessDataGridView"
        Me.customersAccessDataGridView.Size = New System.Drawing.Size(484, 210)
        Me.customersAccessDataGridView.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.customersSqlServerDataGridView)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 246)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(487, 229)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SQL-Server"
        '
        'customersSqlServerDataGridView
        '
        Me.customersSqlServerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.customersSqlServerDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.customersSqlServerDataGridView.Location = New System.Drawing.Point(3, 16)
        Me.customersSqlServerDataGridView.Name = "customersSqlServerDataGridView"
        Me.customersSqlServerDataGridView.Size = New System.Drawing.Size(481, 210)
        Me.customersSqlServerDataGridView.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.customersAccessDataGridView)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(490, 229)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MS-Access"
        '
        'closeApplicationButton
        '
        Me.closeApplicationButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.closeApplicationButton.Location = New System.Drawing.Point(424, 490)
        Me.closeApplicationButton.Name = "closeApplicationButton"
        Me.closeApplicationButton.Size = New System.Drawing.Size(75, 23)
        Me.closeApplicationButton.TabIndex = 6
        Me.closeApplicationButton.Text = "Exit"
        Me.closeApplicationButton.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(515, 525)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.closeApplicationButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comparing using parameters for SQL statements"
        CType(Me.customersAccessDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.customersSqlServerDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents customersAccessDataGridView As DataGridView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents customersSqlServerDataGridView As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents closeApplicationButton As Button
End Class
