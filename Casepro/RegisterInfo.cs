using Ozeki.Network;
using Ozeki.Network.Nat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
    public partial class RegisterInfo : Form
    {
        public RegisterInfo()
        {
            InitializeComponent();
        }

        public bool IsRegRequired = false;
        public String displayname;
        public String username;
        public String registername;
        public String regpass;
        public String domainhost;
        public int port;
        public NatTraversalMethod natTraversal = 0;


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IsRegRequired = !IsRegRequired;
        }

      

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                displayname = textBox1.Text;
                username = textBox2.Text;
                registername = textBox3.Text;
                regpass = textBox9.Text;
                domainhost = textBox4.Text + "." + textBox5.Text + "." + textBox6.Text + "." + textBox7.Text;
                port = Int32.Parse(textBox8.Text);
                switch (comboBox1.SelectedIndex)
                {
                    case -1:
                    case 0:
                        natTraversal = NatTraversalMethod.None;
                        break;
                    case 1:
                        natTraversal = NatTraversalMethod.STUN;
                        break;
                    case 2:
                        natTraversal = NatTraversalMethod.TURN;
                        break;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("The port number you set is invalid, please set a valid port number.\n {0}", ex.Message), string.Empty, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }




    }
}

