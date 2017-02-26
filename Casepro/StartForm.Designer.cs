namespace Casepro
{
    partial class StartForm
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
            this.calendar1 = new WindowsFormsCalendar.Calendar();
            this.monthView1 = new WindowsFormsCalendar.MonthView();
            this.uploadTxt = new System.Windows.Forms.TextBox();
            this.eventLbl1 = new System.Windows.Forms.Label();
            this.fileLbl1 = new System.Windows.Forms.Label();
            this.clientLbl1 = new System.Windows.Forms.Label();
            this.documentLbl1 = new System.Windows.Forms.Label();
            this.orgLbl1 = new System.Windows.Forms.Label();
            this.nameLbl1 = new System.Windows.Forms.Label();
            this.contactLbl1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // calendar1
            // 
            this.calendar1.AllowDrop = true;
            this.calendar1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.calendar1.Font = new System.Drawing.Font("Calibri", 9F);
            this.calendar1.ItemsBackgroundColor = System.Drawing.Color.Red;
            this.calendar1.ItemsFont = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendar1.ItemsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.calendar1.Location = new System.Drawing.Point(3, 3);
            this.calendar1.MaximumFullDays = 15;
            this.calendar1.Name = "calendar1";
            this.calendar1.Size = new System.Drawing.Size(933, 575);
            this.calendar1.TabIndex = 1;
            this.calendar1.Text = "calendar1";
            this.calendar1.TimeScale = WindowsFormsCalendar.CalendarTimeScale.SixtyMinutes;
            this.calendar1.LoadItems += new WindowsFormsCalendar.Calendar.CalendarLoadEventHandler(this.calendar1_LoadItems);
            // 
            // monthView1
            // 
            this.monthView1.ArrowsColor = System.Drawing.SystemColors.Window;
            this.monthView1.ArrowsSelectedColor = System.Drawing.Color.Gold;
            this.monthView1.DayBackgroundColor = System.Drawing.Color.Empty;
            this.monthView1.DayGrayedText = System.Drawing.SystemColors.GrayText;
            this.monthView1.DaySelectedBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.monthView1.DaySelectedColor = System.Drawing.SystemColors.WindowText;
            this.monthView1.DaySelectedTextColor = System.Drawing.SystemColors.HighlightText;
            this.monthView1.ItemPadding = new System.Windows.Forms.Padding(2);
            this.monthView1.Location = new System.Drawing.Point(957, 15);
            this.monthView1.MonthTitleColor = System.Drawing.SystemColors.ActiveCaption;
            this.monthView1.MonthTitleColorInactive = System.Drawing.SystemColors.InactiveCaption;
            this.monthView1.MonthTitleTextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.monthView1.MonthTitleTextColorInactive = System.Drawing.SystemColors.InactiveCaptionText;
            this.monthView1.Name = "monthView1";
            this.monthView1.SelectionMode = WindowsFormsCalendar.MonthViewSelection.Week;
            this.monthView1.Size = new System.Drawing.Size(223, 147);
            this.monthView1.TabIndex = 2;
            this.monthView1.Text = "monthView1";
            this.monthView1.TodayBorderColor = System.Drawing.Color.Maroon;
            this.monthView1.SelectionChanged += new System.EventHandler(this.monthView1_SelectionChanged);
            // 
            // uploadTxt
            // 
            this.uploadTxt.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.uploadTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uploadTxt.Location = new System.Drawing.Point(15, 596);
            this.uploadTxt.Multiline = true;
            this.uploadTxt.Name = "uploadTxt";
            this.uploadTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uploadTxt.Size = new System.Drawing.Size(936, 50);
            this.uploadTxt.TabIndex = 3;
            this.uploadTxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // eventLbl1
            // 
            this.eventLbl1.AutoSize = true;
            this.eventLbl1.BackColor = System.Drawing.Color.MistyRose;
            this.eventLbl1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventLbl1.Location = new System.Drawing.Point(978, 194);
            this.eventLbl1.Name = "eventLbl1";
            this.eventLbl1.Size = new System.Drawing.Size(45, 26);
            this.eventLbl1.TabIndex = 4;
            this.eventLbl1.Text = "info";
            this.eventLbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileLbl1
            // 
            this.fileLbl1.AutoSize = true;
            this.fileLbl1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.fileLbl1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileLbl1.Location = new System.Drawing.Point(978, 267);
            this.fileLbl1.Name = "fileLbl1";
            this.fileLbl1.Size = new System.Drawing.Size(45, 26);
            this.fileLbl1.TabIndex = 5;
            this.fileLbl1.Text = "info";
            this.fileLbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clientLbl1
            // 
            this.clientLbl1.AutoSize = true;
            this.clientLbl1.BackColor = System.Drawing.Color.LightSalmon;
            this.clientLbl1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientLbl1.Location = new System.Drawing.Point(978, 230);
            this.clientLbl1.Name = "clientLbl1";
            this.clientLbl1.Size = new System.Drawing.Size(45, 26);
            this.clientLbl1.TabIndex = 6;
            this.clientLbl1.Text = "info";
            this.clientLbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // documentLbl1
            // 
            this.documentLbl1.AutoSize = true;
            this.documentLbl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.documentLbl1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentLbl1.Location = new System.Drawing.Point(978, 302);
            this.documentLbl1.Name = "documentLbl1";
            this.documentLbl1.Size = new System.Drawing.Size(45, 26);
            this.documentLbl1.TabIndex = 7;
            this.documentLbl1.Text = "info";
            this.documentLbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orgLbl1
            // 
            this.orgLbl1.AutoSize = true;
            this.orgLbl1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orgLbl1.Location = new System.Drawing.Point(981, 494);
            this.orgLbl1.Name = "orgLbl1";
            this.orgLbl1.Size = new System.Drawing.Size(42, 15);
            this.orgLbl1.TabIndex = 9;
            this.orgLbl1.Text = "label1";
            // 
            // nameLbl1
            // 
            this.nameLbl1.AutoSize = true;
            this.nameLbl1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl1.Location = new System.Drawing.Point(980, 527);
            this.nameLbl1.Name = "nameLbl1";
            this.nameLbl1.Size = new System.Drawing.Size(42, 15);
            this.nameLbl1.TabIndex = 10;
            this.nameLbl1.Text = "label1";
            // 
            // contactLbl1
            // 
            this.contactLbl1.AutoSize = true;
            this.contactLbl1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contactLbl1.Location = new System.Drawing.Point(980, 555);
            this.contactLbl1.Name = "contactLbl1";
            this.contactLbl1.Size = new System.Drawing.Size(42, 15);
            this.contactLbl1.TabIndex = 11;
            this.contactLbl1.Text = "label1";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGreen;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(981, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.calendar1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 581);
            this.panel1.TabIndex = 13;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(981, 340);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(159, 118);
            this.pictureBox5.TabIndex = 8;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1192, 649);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.contactLbl1);
            this.Controls.Add(this.nameLbl1);
            this.Controls.Add(this.orgLbl1);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.documentLbl1);
            this.Controls.Add(this.clientLbl1);
            this.Controls.Add(this.fileLbl1);
            this.Controls.Add(this.eventLbl1);
            this.Controls.Add(this.uploadTxt);
            this.Controls.Add(this.monthView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.Leave += new System.EventHandler(this.StartForm_Leave);
            this.Validated += new System.EventHandler(this.StartForm_Validated);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WindowsFormsCalendar.Calendar calendar1;
        private WindowsFormsCalendar.MonthView monthView1;
        private System.Windows.Forms.TextBox uploadTxt;
        private System.Windows.Forms.Label eventLbl1;
        private System.Windows.Forms.Label fileLbl1;
        private System.Windows.Forms.Label clientLbl1;
        private System.Windows.Forms.Label documentLbl1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label orgLbl1;
        private System.Windows.Forms.Label nameLbl1;
        private System.Windows.Forms.Label contactLbl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}