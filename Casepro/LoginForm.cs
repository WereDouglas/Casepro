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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LoadSettings();
        }
        private void LoadSettings()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load("LocalXMLFile.xml");
                var servers = from person in xmlDoc.Descendants("Server")
                              select new
                              {
                                  Name = person.Element("Name").Value,
                                  Ip = person.Element("Ip").Value,
                                  Port = person.Element("Port").Value,
                              };

                foreach (var server in servers)
                {
                    Helper.serverName = server.Name;
                    Helper.serverIP = server.Ip;
                    Helper.port = server.Port;

                    Helper.fileUrl = "http://" + server.Ip + "/caseprofessionals/files/";
                    Helper.imageUrl = "http://" + server.Ip + "/caseprofessionals/uploads/";
                    Helper.uploadUrl = "http://" + server.Ip + "/caseprofessionals/uploads/uploads.php";
                    Helper.RemoteUploadUrl = "http://caseprofessional.org/uploads/uploads.php";
                    Helper.msgUrl = "http://" + server.Ip + "/caseprofessionals/index.php/message/event";
                }
                // MessageBox.Show(Helper.serverIP);
                if (TestServerConnection())
                {
                    lblStatus.Text = ("Server connected   you can continue to login");
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    ServerForm frm = new ServerForm();                   
                    frm.Show();
                    this.Hide();

                    lblStatus.Text = ("You are not able to connect to the server contact the administrator for further assistance");
                    lblStatus.ForeColor = Color.Red;

                }
                
            }
            catch
            {

                ServerForm frm = new ServerForm();
                frm.Show();
               this.Hide();
            }


        }
        private static bool _exiting;

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm or no?", "Are you sure you want to exit the application ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                // this.Close(); // you don't need that, it's already closing
                Application.Exit();
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

        private void button2_Click(object sender, EventArgs e)
        {
            loginBtn.Visible = false;
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            if (contactTxt.Text=="" || passwordTxt.Text =="")
            {
               
                MessageBox.Show("Insert login credentials");
                loginBtn.Visible = true;
                return;
            }
            command.CommandText = "SELECT * FROM users WHERE contact = '"+contactTxt.Text+"' AND password = '"+Helper.MD5Hash(passwordTxt.Text)+"'";
            connection.Open();
            Reader = command.ExecuteReader();
            
                while (Reader.Read())
                {
                    if ((Reader.IsDBNull(7) ? "" : Reader.GetString(7)) == "")
                    {
                        MessageBox.Show("Access denied");
                        loginBtn.Visible = true;
                }
                    else
                    {
                        Helper.username = (Reader.IsDBNull(2) ? "none" : Reader.GetString(2));
                        Helper.contact = (Reader.IsDBNull(7) ? "none" : Reader.GetString(7));
                        Helper.designation = (Reader.IsDBNull(5) ? "none" : Reader.GetString(5));
                        Helper.email = (Reader.IsDBNull(3) ? "none" : Reader.GetString(3));
                        Helper.image = (Reader.IsDBNull(8) ? "none" : Reader.GetString(8));
                        Helper.orgID = (Reader.IsDBNull(1) ? "none" : Reader.GetString(1));
                        
                        loginBtn.Visible = false;
                    }
                }
            if (Helper.contact != "" && Helper.orgID !="")
            {
                loginBtn.Visible = false;
                MainForm frm = new MainForm();
                frm.Show();
                this.Hide();
            }


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OrganisationForm frm = new OrganisationForm();
            frm.Show();
            this.Hide();
        }

        private void LoginForm_Activated(object sender, EventArgs e)
        {
          //  LoadSettings();
        }

        private void passwordTxt_Leave(object sender, EventArgs e)
        {
          //  button2_Click(null, null);
        }

        private void LoginForm_MouseLeave(object sender, EventArgs e)
        {
            
        }
    }
}
