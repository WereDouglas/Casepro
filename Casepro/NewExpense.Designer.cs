namespace Casepro
{
    partial class NewExpense
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
            this.print = new System.Windows.Forms.Button();
            this.Submit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.detailsTxt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.paidCbx = new System.Windows.Forms.ComboBox();
            this.fileCbx = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.balanceTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lawyerCbx = new System.Windows.Forms.ComboBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.clientCbx = new System.Windows.Forms.ComboBox();
            this.addressLbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.vatTxt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.wordsLbl = new System.Windows.Forms.Label();
            this.methodCbx = new System.Windows.Forms.ComboBox();
            this.amountTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.noLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.paymentDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logoBx = new System.Windows.Forms.PictureBox();
            this.deadlineDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.Reason = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.reasonTxt = new System.Windows.Forms.TextBox();
            this.outcomeTxt = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBx)).BeginInit();
            this.SuspendLayout();
            // 
            // print
            // 
            this.print.Location = new System.Drawing.Point(357, 597);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(75, 25);
            this.print.TabIndex = 17;
            this.print.Text = "Print";
            this.print.UseVisualStyleBackColor = true;
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(455, 597);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(75, 25);
            this.Submit.TabIndex = 16;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::Casepro.Properties.Resources.Cancel_241;
            this.button2.Location = new System.Drawing.Point(530, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 33);
            this.button2.TabIndex = 18;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.outcomeTxt);
            this.panel1.Controls.Add(this.reasonTxt);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.Reason);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.deadlineDate);
            this.panel1.Controls.Add(this.detailsTxt);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.paidCbx);
            this.panel1.Controls.Add(this.fileCbx);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.balanceTxt);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lawyerCbx);
            this.panel1.Controls.Add(this.nameLbl);
            this.panel1.Controls.Add(this.clientCbx);
            this.panel1.Controls.Add(this.addressLbl);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.vatTxt);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.wordsLbl);
            this.panel1.Controls.Add(this.methodCbx);
            this.panel1.Controls.Add(this.amountTxt);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.noLbl);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.paymentDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.logoBx);
            this.panel1.Location = new System.Drawing.Point(31, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 556);
            this.panel1.TabIndex = 19;
            // 
            // detailsTxt
            // 
            this.detailsTxt.Location = new System.Drawing.Point(114, 403);
            this.detailsTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.detailsTxt.Name = "detailsTxt";
            this.detailsTxt.Size = new System.Drawing.Size(303, 20);
            this.detailsTxt.TabIndex = 217;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(48, 405);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 216;
            this.label13.Text = "Details:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(72, 504);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 215;
            this.label8.Text = "Paid:";
            // 
            // paidCbx
            // 
            this.paidCbx.FormattingEnabled = true;
            this.paidCbx.Items.AddRange(new object[] {
            "true",
            "false"});
            this.paidCbx.Location = new System.Drawing.Point(114, 504);
            this.paidCbx.Name = "paidCbx";
            this.paidCbx.Size = new System.Drawing.Size(182, 21);
            this.paidCbx.TabIndex = 214;
            // 
            // fileCbx
            // 
            this.fileCbx.FormattingEnabled = true;
            this.fileCbx.Location = new System.Drawing.Point(114, 167);
            this.fileCbx.Name = "fileCbx";
            this.fileCbx.Size = new System.Drawing.Size(306, 21);
            this.fileCbx.TabIndex = 213;
            this.fileCbx.SelectedIndexChanged += new System.EventHandler(this.fileCbx_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 212;
            this.label5.Text = "Balance";
            // 
            // balanceTxt
            // 
            this.balanceTxt.Location = new System.Drawing.Point(114, 347);
            this.balanceTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.balanceTxt.Name = "balanceTxt";
            this.balanceTxt.Size = new System.Drawing.Size(303, 20);
            this.balanceTxt.TabIndex = 211;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 210;
            this.label1.Text = "C/O";
            // 
            // lawyerCbx
            // 
            this.lawyerCbx.FormattingEnabled = true;
            this.lawyerCbx.Location = new System.Drawing.Point(114, 373);
            this.lawyerCbx.Name = "lawyerCbx";
            this.lawyerCbx.Size = new System.Drawing.Size(303, 21);
            this.lawyerCbx.TabIndex = 209;
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.Location = new System.Drawing.Point(165, 10);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(56, 26);
            this.nameLbl.TabIndex = 208;
            this.nameLbl.Text = "INFO";
            // 
            // clientCbx
            // 
            this.clientCbx.FormattingEnabled = true;
            this.clientCbx.Location = new System.Drawing.Point(114, 139);
            this.clientCbx.Name = "clientCbx";
            this.clientCbx.Size = new System.Drawing.Size(306, 21);
            this.clientCbx.TabIndex = 207;
            this.clientCbx.SelectedIndexChanged += new System.EventHandler(this.clientCbx_SelectedIndexChanged);
            // 
            // addressLbl
            // 
            this.addressLbl.Location = new System.Drawing.Point(167, 36);
            this.addressLbl.Name = "addressLbl";
            this.addressLbl.Size = new System.Drawing.Size(253, 50);
            this.addressLbl.TabIndex = 206;
            this.addressLbl.Text = "Address";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(63, 170);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 13);
            this.label14.TabIndex = 205;
            this.label14.Text = "File";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(72, 474);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 199;
            this.label12.Text = "V.A.T";
            // 
            // vatTxt
            // 
            this.vatTxt.Location = new System.Drawing.Point(114, 471);
            this.vatTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.vatTxt.Name = "vatTxt";
            this.vatTxt.Size = new System.Drawing.Size(182, 20);
            this.vatTxt.TabIndex = 197;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 235);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 195;
            this.label10.Text = "Method:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 194;
            this.label9.Text = "Amount paid";
            // 
            // wordsLbl
            // 
            this.wordsLbl.Location = new System.Drawing.Point(114, 254);
            this.wordsLbl.Name = "wordsLbl";
            this.wordsLbl.Size = new System.Drawing.Size(306, 40);
            this.wordsLbl.TabIndex = 193;
            this.wordsLbl.Text = "words";
            // 
            // methodCbx
            // 
            this.methodCbx.FormattingEnabled = true;
            this.methodCbx.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.methodCbx.Location = new System.Drawing.Point(114, 226);
            this.methodCbx.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.methodCbx.Name = "methodCbx";
            this.methodCbx.Size = new System.Drawing.Size(306, 21);
            this.methodCbx.TabIndex = 192;
            // 
            // amountTxt
            // 
            this.amountTxt.Location = new System.Drawing.Point(114, 196);
            this.amountTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.amountTxt.Name = "amountTxt";
            this.amountTxt.Size = new System.Drawing.Size(306, 20);
            this.amountTxt.TabIndex = 162;
            this.amountTxt.TextChanged += new System.EventHandler(this.amountTxt_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 160;
            this.label7.Text = "Client";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 159;
            this.label6.Text = "Date:";
            // 
            // noLbl
            // 
            this.noLbl.AutoSize = true;
            this.noLbl.Location = new System.Drawing.Point(111, 93);
            this.noLbl.Name = "noLbl";
            this.noLbl.Size = new System.Drawing.Size(19, 13);
            this.noLbl.TabIndex = 158;
            this.noLbl.Text = "no";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(71, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 157;
            this.label4.Text = "No:";
            // 
            // paymentDate
            // 
            this.paymentDate.Location = new System.Drawing.Point(270, 93);
            this.paymentDate.Name = "paymentDate";
            this.paymentDate.Size = new System.Drawing.Size(150, 20);
            this.paymentDate.TabIndex = 156;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(165, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "EXPENSE";
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Location = new System.Drawing.Point(111, 36);
            this.label2.MaximumSize = new System.Drawing.Size(0, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 54);
            this.label2.TabIndex = 2;
            this.label2.Text = "Plot 516 and 844 Najjanankumbi-Entebbe Rd,Slightly Opposite FDC Offices,P.O.Box 7" +
    "483 Kampala-Uganda";
            // 
            // logoBx
            // 
            this.logoBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoBx.InitialImage = null;
            this.logoBx.Location = new System.Drawing.Point(10, 10);
            this.logoBx.Name = "logoBx";
            this.logoBx.Size = new System.Drawing.Size(122, 76);
            this.logoBx.TabIndex = 0;
            this.logoBx.TabStop = false;
            // 
            // deadlineDate
            // 
            this.deadlineDate.Location = new System.Drawing.Point(114, 431);
            this.deadlineDate.Name = "deadlineDate";
            this.deadlineDate.Size = new System.Drawing.Size(150, 20);
            this.deadlineDate.TabIndex = 218;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(53, 437);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 219;
            this.label11.Text = "Deadline";
            // 
            // Reason
            // 
            this.Reason.AutoSize = true;
            this.Reason.Location = new System.Drawing.Point(41, 301);
            this.Reason.Name = "Reason";
            this.Reason.Size = new System.Drawing.Size(44, 13);
            this.Reason.TabIndex = 220;
            this.Reason.Text = "Reason";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(41, 323);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 221;
            this.label16.Text = "Outcome";
            // 
            // reasonTxt
            // 
            this.reasonTxt.Location = new System.Drawing.Point(114, 297);
            this.reasonTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.reasonTxt.Name = "reasonTxt";
            this.reasonTxt.Size = new System.Drawing.Size(303, 20);
            this.reasonTxt.TabIndex = 222;
            // 
            // outcomeTxt
            // 
            this.outcomeTxt.Location = new System.Drawing.Point(114, 323);
            this.outcomeTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.outcomeTxt.Name = "outcomeTxt";
            this.outcomeTxt.Size = new System.Drawing.Size(303, 20);
            this.outcomeTxt.TabIndex = 223;
            // 
            // NewExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(570, 625);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.print);
            this.Controls.Add(this.Submit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewExpense";
            this.Text = "NewExpense";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox detailsTxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox paidCbx;
        private System.Windows.Forms.ComboBox fileCbx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox balanceTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox lawyerCbx;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.ComboBox clientCbx;
        private System.Windows.Forms.Label addressLbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox vatTxt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label wordsLbl;
        private System.Windows.Forms.ComboBox methodCbx;
        private System.Windows.Forms.TextBox amountTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label noLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker paymentDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox logoBx;
        private System.Windows.Forms.DateTimePicker deadlineDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label Reason;
        private System.Windows.Forms.TextBox reasonTxt;
        private System.Windows.Forms.TextBox outcomeTxt;
    }
}