using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Casepro
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadSettings();

        }
        private void LoadSettings()
        {
            


            // MessageBox.Show(Helper.serverIP);
            if (TestServerConnection())
            {
                lblStatus.Text = ("You are connected  to the server ");
                lblStatus.ForeColor = Color.Green;
                HomeForm frms = new HomeForm();
                frms.MdiParent = this;
                frms.Dock = DockStyle.Fill;
                frms.Show();

            }
            else
            {
                ServerForm frm = new ServerForm();
                frm.Show();
                lblStatus.Text = ("You are not able to connect to the server contact the administrator for further assistance");
                lblStatus.ForeColor = Color.Red;

            }
            if (TestOnlineServerConnection())
            {
                onlineLbl.Text = ("connection successful to online server");
                onlineLbl.ForeColor = Color.Green;
            }
            else
            {
                onlineLbl.Text = ("You are not able to connect to the online server contact the administrator for further assistance");
                onlineLbl.ForeColor = Color.Red;
            }




        }
        private bool TestServerConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(DBConnect.conn);
                MySqlCommand command = connection.CreateCommand();
                // MySqlDataReader Reader;
                command.CommandText = "SELECT * FROM events WHERE sync ='f' ;";
                connection.Open();
                connection.Close();
                lblStatus.Text = ("Local server connection successful");
                lblStatus.ForeColor = Color.Green;
                return true;
            }
            catch
            {
                lblStatus.Text = ("You are not able to connect to the server contact the administrator for further assistance");
                lblStatus.ForeColor = Color.Red;
                return false;
            }
        }
        private bool TestOnlineServerConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(DBConnect.remoteConn);
                MySqlCommand command = connection.CreateCommand();

                connection.Open();
                connection.Close();
                onlineLbl.Text = ("connection successful to online server");
                onlineLbl.ForeColor = Color.Green;
                return true;
            }
            catch
            {
                onlineLbl.Text = ("You are not able to connect to the online server contact the administrator for further assistance");
                onlineLbl.ForeColor = Color.Red;
                return false;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {


        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            try
            {
                UserForm frm = new UserForm();
                frm.MdiParent = this;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FileForm frm = new FileForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();

        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            HomeForm frm = new HomeForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ClientForm frm = new ClientForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            EventForm frm = new EventForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DocumentForm frm = new DocumentForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            SettingForm frm = new SettingForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerForm frm = new ServerForm();
            frm.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.Show();
            this.Hide();
        }

        private void companyProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           // this.Hide();
        }

        private void companyProfileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OrganisationForm frm = new OrganisationForm();
            frm.Show();
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            FeesForm frm = new FeesForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            DisForm frm = new DisForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ExpenseForm frm = new ExpenseForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
