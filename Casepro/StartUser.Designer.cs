namespace Casepro
{
    partial class StartUser
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.confirmTxtBx = new System.Windows.Forms.TextBox();
            this.passwordTxtBx = new System.Windows.Forms.TextBox();
            this.addressTxtBx = new System.Windows.Forms.TextBox();
            this.contactTxtBx = new System.Windows.Forms.TextBox();
            this.designationCbx = new System.Windows.Forms.ComboBox();
            this.emailTxtBx = new System.Windows.Forms.TextBox();
            this.nameTxtBx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::Casepro.Properties.Resources.Cancel_24;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(198, 338);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 29);
            this.button2.TabIndex = 97;
            this.button2.Text = "Cancel";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::Casepro.Properties.Resources.Save_24__1_;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(288, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 29);
            this.button1.TabIndex = 96;
            this.button1.Text = "Save";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(78, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 14);
            this.label10.TabIndex = 111;
            this.label10.Text = "Confirm password:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 14);
            this.label8.TabIndex = 110;
            this.label8.Text = "System access password:";
            // 
            // confirmTxtBx
            // 
            this.confirmTxtBx.Location = new System.Drawing.Point(198, 226);
            this.confirmTxtBx.Name = "confirmTxtBx";
            this.confirmTxtBx.PasswordChar = '*';
            this.confirmTxtBx.Size = new System.Drawing.Size(170, 22);
            this.confirmTxtBx.TabIndex = 109;
            this.confirmTxtBx.UseSystemPasswordChar = true;
            // 
            // passwordTxtBx
            // 
            this.passwordTxtBx.Location = new System.Drawing.Point(198, 191);
            this.passwordTxtBx.Name = "passwordTxtBx";
            this.passwordTxtBx.PasswordChar = '*';
            this.passwordTxtBx.Size = new System.Drawing.Size(170, 22);
            this.passwordTxtBx.TabIndex = 108;
            this.passwordTxtBx.UseSystemPasswordChar = true;
            // 
            // addressTxtBx
            // 
            this.addressTxtBx.Location = new System.Drawing.Point(198, 272);
            this.addressTxtBx.Multiline = true;
            this.addressTxtBx.Name = "addressTxtBx";
            this.addressTxtBx.Size = new System.Drawing.Size(170, 50);
            this.addressTxtBx.TabIndex = 107;
            // 
            // contactTxtBx
            // 
            this.contactTxtBx.Location = new System.Drawing.Point(198, 151);
            this.contactTxtBx.Name = "contactTxtBx";
            this.contactTxtBx.Size = new System.Drawing.Size(170, 22);
            this.contactTxtBx.TabIndex = 106;
            // 
            // designationCbx
            // 
            this.designationCbx.FormattingEnabled = true;
            this.designationCbx.Items.AddRange(new object[] {
            "Partner",
            "Associate",
            "Contract",
            "Of counsel",
            "Clerk",
            "Paralegal",
            "Administrator",
            "Other"});
            this.designationCbx.Location = new System.Drawing.Point(198, 116);
            this.designationCbx.Name = "designationCbx";
            this.designationCbx.Size = new System.Drawing.Size(170, 22);
            this.designationCbx.TabIndex = 105;
            // 
            // emailTxtBx
            // 
            this.emailTxtBx.Location = new System.Drawing.Point(198, 86);
            this.emailTxtBx.Name = "emailTxtBx";
            this.emailTxtBx.Size = new System.Drawing.Size(170, 22);
            this.emailTxtBx.TabIndex = 104;
            // 
            // nameTxtBx
            // 
            this.nameTxtBx.Location = new System.Drawing.Point(198, 56);
            this.nameTxtBx.Name = "nameTxtBx";
            this.nameTxtBx.Size = new System.Drawing.Size(170, 22);
            this.nameTxtBx.TabIndex = 103;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.TabIndex = 102;
            this.label5.Text = "Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 14);
            this.label4.TabIndex = 101;
            this.label4.Text = "Contact:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 14);
            this.label3.TabIndex = 100;
            this.label3.Text = "Designation:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 99;
            this.label2.Text = "E-mail:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 98;
            this.label1.Text = "Name:";
            // 
            // StartUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(411, 380);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.confirmTxtBx);
            this.Controls.Add(this.passwordTxtBx);
            this.Controls.Add(this.addressTxtBx);
            this.Controls.Add(this.contactTxtBx);
            this.Controls.Add(this.designationCbx);
            this.Controls.Add(this.emailTxtBx);
            this.Controls.Add(this.nameTxtBx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartUser";
            this.Text = "StartUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox confirmTxtBx;
        private System.Windows.Forms.TextBox passwordTxtBx;
        private System.Windows.Forms.TextBox addressTxtBx;
        private System.Windows.Forms.TextBox contactTxtBx;
        private System.Windows.Forms.ComboBox designationCbx;
        private System.Windows.Forms.TextBox emailTxtBx;
        private System.Windows.Forms.TextBox nameTxtBx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}