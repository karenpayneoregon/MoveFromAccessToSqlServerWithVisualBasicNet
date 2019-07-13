namespace SqlServerDeleteReorderExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.EraseCurrentRowButton = new System.Windows.Forms.Button();
            this.ResetManualKeysButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(294, 450);
            this.dataGridView1.TabIndex = 0;
            // 
            // EraseCurrentRowButton
            // 
            this.EraseCurrentRowButton.Location = new System.Drawing.Point(307, 26);
            this.EraseCurrentRowButton.Name = "EraseCurrentRowButton";
            this.EraseCurrentRowButton.Size = new System.Drawing.Size(154, 23);
            this.EraseCurrentRowButton.TabIndex = 1;
            this.EraseCurrentRowButton.Text = "Erase Data";
            this.EraseCurrentRowButton.UseVisualStyleBackColor = true;
            this.EraseCurrentRowButton.Click += new System.EventHandler(this.EraseCurrentRowButton_Click);
            // 
            // ResetManualKeysButton
            // 
            this.ResetManualKeysButton.Location = new System.Drawing.Point(307, 67);
            this.ResetManualKeysButton.Name = "ResetManualKeysButton";
            this.ResetManualKeysButton.Size = new System.Drawing.Size(154, 23);
            this.ResetManualKeysButton.TabIndex = 2;
            this.ResetManualKeysButton.Text = "Reset manual";
            this.ResetManualKeysButton.UseVisualStyleBackColor = true;
            this.ResetManualKeysButton.Click += new System.EventHandler(this.ResetManualKeysButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 450);
            this.Controls.Add(this.ResetManualKeysButton);
            this.Controls.Add(this.EraseCurrentRowButton);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button EraseCurrentRowButton;
        private System.Windows.Forms.Button ResetManualKeysButton;
    }
}

