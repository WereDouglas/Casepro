namespace Casepro
{
    partial class FinancialForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinancialForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.previewdlg = new System.Windows.Forms.PrintPreviewDialog();
            this.printdoc1 = new System.Drawing.Printing.PrintDocument();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtGrid = new System.Windows.Forms.DataGridView();
            this.monthPicker = new System.Windows.Forms.DateTimePicker();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel2.Controls.Add(this.monthPicker);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Location = new System.Drawing.Point(13, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1028, 40);
            this.panel2.TabIndex = 106;
            // 
            // previewdlg
            // 
            this.previewdlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.previewdlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.previewdlg.ClientSize = new System.Drawing.Size(400, 300);
            this.previewdlg.Enabled = true;
            this.previewdlg.Icon = ((System.Drawing.Icon)(resources.GetObject("previewdlg.Icon")));
            this.previewdlg.Name = "previewdlg";
            this.previewdlg.Visible = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::Casepro.Properties.Resources.Print_24;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(938, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 26);
            this.button4.TabIndex = 104;
            this.button4.Text = "PRINT";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dtGrid);
            this.panel1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(12, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1030, 602);
            this.panel1.TabIndex = 107;
            // 
            // dtGrid
            // 
            this.dtGrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrid.ColumnHeadersVisible = false;
            this.dtGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrid.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dtGrid.Location = new System.Drawing.Point(0, 0);
            this.dtGrid.Name = "dtGrid";
            this.dtGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtGrid.RowHeadersVisible = false;
            this.dtGrid.Size = new System.Drawing.Size(1028, 600);
            this.dtGrid.TabIndex = 101;
            // 
            // monthPicker
            // 
            this.monthPicker.Location = new System.Drawing.Point(12, 8);
            this.monthPicker.Name = "monthPicker";
            this.monthPicker.Size = new System.Drawing.Size(134, 20);
            this.monthPicker.TabIndex = 105;
            this.monthPicker.CloseUp += new System.EventHandler(this.monthPicker_CloseUp);
            // 
            // FinancialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1063, 606);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FinancialForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FinancialForm";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PrintPreviewDialog previewdlg;
        private System.Drawing.Printing.PrintDocument printdoc1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtGrid;
        private System.Windows.Forms.DateTimePicker monthPicker;
    }
}