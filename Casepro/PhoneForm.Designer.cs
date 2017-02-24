namespace Casepro
{
    partial class PhoneForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.KeyPad = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.destinationIP = new System.Windows.Forms.TextBox();
            this.myIPTxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.infoTxt = new System.Windows.Forms.TextBox();
            this.KeyPad.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "IP address of your  device";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "OFFLINE";
            // 
            // KeyPad
            // 
            this.KeyPad.Controls.Add(this.infoTxt);
            this.KeyPad.Controls.Add(this.label1);
            this.KeyPad.Controls.Add(this.button2);
            this.KeyPad.Controls.Add(this.label3);
            this.KeyPad.Controls.Add(this.destinationIP);
            this.KeyPad.Controls.Add(this.myIPTxt);
            this.KeyPad.Controls.Add(this.label4);
            this.KeyPad.Location = new System.Drawing.Point(15, 15);
            this.KeyPad.Name = "KeyPad";
            this.KeyPad.Size = new System.Drawing.Size(563, 280);
            this.KeyPad.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Enter the IP address of the destination:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(204, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Call";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // destinationIP
            // 
            this.destinationIP.Location = new System.Drawing.Point(15, 29);
            this.destinationIP.Name = "destinationIP";
            this.destinationIP.Size = new System.Drawing.Size(183, 20);
            this.destinationIP.TabIndex = 6;
            // 
            // myIPTxt
            // 
            this.myIPTxt.Location = new System.Drawing.Point(15, 73);
            this.myIPTxt.Name = "myIPTxt";
            this.myIPTxt.Size = new System.Drawing.Size(183, 20);
            this.myIPTxt.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(503, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // infoTxt
            // 
            this.infoTxt.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.infoTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoTxt.Location = new System.Drawing.Point(15, 137);
            this.infoTxt.Multiline = true;
            this.infoTxt.Name = "infoTxt";
            this.infoTxt.Size = new System.Drawing.Size(187, 123);
            this.infoTxt.TabIndex = 9;
            // 
            // PhoneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(600, 331);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.KeyPad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PhoneForm";
            this.Text = "PhoneForm";
            this.KeyPad.ResumeLayout(false);
            this.KeyPad.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel KeyPad;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox destinationIP;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox myIPTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox infoTxt;
    }
}