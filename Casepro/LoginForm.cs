using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
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
            lblStatus.Text = "";
            LoadSettings();
            SendMail();
            autocomplete();
           

        }
        string IP;
        private void SendMail()
        {

            if (Helper.IsInternetAvailable())
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(Helper.msgUrl);
                    request.GetResponse();
                    lblStatus.Text = lblStatus.Text + " Reminder email sent \n";
                }
                catch
                {
                    lblStatus.Text = lblStatus.Text + " Error sending mail \n";

                }
            }
            else
            {

                lblStatus.Text = lblStatus.Text + " No internet connection \n";
            }

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
                                  db = person.Element("db").Value,
                                  dbusername = person.Element("username").Value,
                                  dbpwd = person.Element("password").Value,
                              };

                foreach (var server in servers)
                {
                    Helper.serverName = server.Name;
                    if (IPAddressCheck(Helper.serverName) != "")
                    {
                        Helper.serverIP = IPAddressCheck(Helper.serverName);
                        lblStatus.Text = lblStatus.Text + IPAddressCheck(Helper.serverName);
                        IP = IPAddressCheck(Helper.serverName);
                    }
                    else
                    {
                        MessageBox.Show("Please start the server");
                        //return;
                    }
                    Helper.port = server.Port;
                    Helper.db = server.db;
                    Helper.dbusername = server.dbusername;
                    Helper.dbpwd = server.dbpwd;

                    Helper.fileUrl = "http://" + Helper.serverIP + "/caseprofessionals/files/";
                    Helper.imageUrl = "http://" + Helper.serverIP + "/caseprofessionals/uploads/";
                    Helper.uploadUrl = "http://" + Helper.serverIP + "/caseprofessionals/uploads/uploads.php";
                    Helper.RemoteUploadUrl = "http://caseprofessional.org/uploads/uploads.php";
                    Helper.msgUrl = "http://" + Helper.serverIP + "/caseprofessionals/index.php/message/event";
                }
                // MessageBox.Show(Helper.serverIP);
                if (TestServerConnection())
                {
                    lblStatus.Text = lblStatus.Text + (" Server connected you can continue to login");
                    lblStatus.ForeColor = Color.Green;


                }
                else
                {
                    //ServerForm frm = new ServerForm();                   
                    //frm.Show();
                    //this.Hide();

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
        private void autocomplete()
        {

            AutoCompleteStringCollection AutoItem = new AutoCompleteStringCollection();

            DataTable dt = new DataTable();

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;

            command.CommandText = "SELECT contact FROM users";
            try
            {
                connection.Open();
            }
            catch (Exception c)
            {

                MessageBox.Show("Please start the server " + " Details :\n" + c.Message);
                return;

            }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                AutoItem.Add((Reader.IsDBNull(0) ? "" : Reader.GetString(0)));
            }
            contactTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            contactTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            contactTxt.AutoCompleteCustomSource = AutoItem;


        }
        private String IPAddressCheck(string HostName)
        {
            //var ip = System.Net.Dns.GetHostEntry("JacksLaptop");
            //  string ipStrings = ip.AddressList[0].ToString();
            //var host =;
            var IPAddr = Dns.GetHostEntry(HostName);
            IPAddress ipString = null;

            foreach (var IP in IPAddr.AddressList)
            {
                if (IPAddress.TryParse(IP.ToString(), out ipString) && IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    break;
                }
            }
            // Helper.serverIP = ipString.ToString();
            return ipString.ToString();
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
                lblStatus.Text = lblStatus.Text + " Local server connection successful ";
                lblStatus.ForeColor = Color.Green;
                return true;
            }
            catch (Exception c)
            {
                lblStatus.Text = ("You are not able to connect to the server contact the administrator for further assistance/n" + c.Message);
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Please start the server " + " Details :\n" + c.Message);
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loginBtn.Visible = false;
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            if (contactTxt.Text == "" || passwordTxt.Text == "")
            {

                MessageBox.Show("Insert login credentials");
                loginBtn.Visible = true;
                return;
            }
            command.CommandText = "SELECT * FROM users WHERE contact = '" + contactTxt.Text + "' AND password = '" + Helper.MD5Hash(passwordTxt.Text) + "'";
            try
            {
                connection.Open();
            }
            catch {

                MessageBox.Show("Error connecting to server !");
            }
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
            connection.Close();
            MySqlConnection connection2 = new MySqlConnection(DBConnect.conn);
            MySqlCommand command2 = connection2.CreateCommand();
            MySqlDataReader Reader2;
            command2.CommandText = "SELECT * FROM org WHERE orgID = '" + Helper.orgID + "'";
            connection2.Open();
            Reader2 = command2.ExecuteReader();
            while (Reader2.Read())
            {
                Helper.logo = (Reader2.IsDBNull(9) ? "none" : Reader2.GetString(9));
                Helper.address = (Reader2.IsDBNull(6) ? "none" : Reader2.GetString(6));
                Helper.orgName = (Reader2.IsDBNull(1) ? "none" : Reader2.GetString(1));
                Helper.code = (Reader2.IsDBNull(5) ? "none" : Reader2.GetString(5));

            }
            connection2.Close();
            if (string.IsNullOrEmpty(Helper.contact) || string.IsNullOrEmpty(Helper.orgID))
            {
                MessageBox.Show("Access denied");
                loginBtn.Visible = true;
            }
            else
            {

                loginBtn.Visible = false;
                MainForm frm = new MainForm();
                frm.Show();
                this.Hide();
            }
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn.PerformClick();
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

        private void button3_Click(object sender, EventArgs e)
        {
            //   string query = @"CREATE TABLE first (" + a.Columns[0] + " int(20) NOT NULL auto_increment, " + a.Columns[1].ToString() + " varchar(100) NOT NULL default, PRIMARY KEY (" + a.Columns[0] + ")";
            ////   string Query = "INSERT INTO users(userID, orgID, name, email, password, designation, status, contact, image, address, category, created, sync, charge, supervisor) VALUES ('" + userID + "','" + Helper.orgID + "','" + this.nameTxtBx.Text + "','" + this.emailTxtBx.Text + "','" + Helper.MD5Hash(this.passwordTxtBx.Text) + "','" + this.designationCbx.Text + "','" + this.statusCbx.Text + "','" + this.contactTxtBx.Text + "','" + userID.Trim() + ".jpg" + "','" + this.addressTxtBx.Text + "','staff','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','f','" + this.chargeTxtBx.Text + "','" + supervisorCbx.Text + "');";
            //   Helper.Execute(query, DBConnect.conn);
            //   MessageBox.Show("Information saved");

            ServerForm frm = new ServerForm();
            frm.Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // string path = "\\\\"+Helper.serverIP+"\\ShareName\\targetnewfolder";
            // System.IO.Directory.CreateDirectory(path);
            //System.IO.Directory.GetDirectories(path);



        }
        private void jobsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
   

    }
}
