using Casepro.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
    public partial class NewDoc : Form
    {
        private User _user = new User();
        private List<User> _userList = new List<User>();
        private Client _client = new Client();
        private List<Client> _clientList = new List<Client>();

        private File _file = new File();
        private List<File> _fileList = new List<File>();
        public NewDoc(string id)
        {
            InitializeComponent();
            LoadUsers();
            LoadClients();
            LoadFiles();
        }
        public void LoadUsers()
        {
            _userList.Clear();

            // connect to database  

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT userID, orgID, name, email, password, designation, status, contact, image, address, category, created,sync, charge, supervisor FROM users";
            connection.Open();
            Reader = command.ExecuteReader();


            while (Reader.Read())
            {
                User _user = new User();
                try { _user.UserID = Reader.GetString(0); }
                catch (InvalidCastException) { }
                try { _user.Name = Reader.GetString(2); }
                catch (InvalidCastException) { }

                try { _user.Email = Reader.GetString(3); }
                catch (InvalidCastException) { }
                try { _user.Image = Reader.GetString(8); }
                catch (InvalidCastException) { }


                _userList.Add(_user);
            }
            lawyerCbx.DataSource = _userList;
            lawyerCbx.DisplayMember = "name";
            connection.Close();
        }
        public void LoadClients()
        {
            _clientList.Clear();

            // connect to database  

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT clientID, orgID, name, email, contact, status, image, address, created, action, lawyer, registration, password FROM client";
            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                Client _client = new Client();
                try { _client.ClientID = Reader.GetString(0); }
                catch (InvalidCastException) { }
                try { _client.Name = Reader.GetString(2); }
                catch (InvalidCastException) { }

                try { _client.Email = Reader.GetString(3); }
                catch (InvalidCastException) { }
                try { _client.Image = Reader.GetString(8); }
                catch (InvalidCastException) { }

                _clientList.Add(_client);
            }
            clientCbx.DataSource = _clientList;
            clientCbx.DisplayMember = "name";
            connection.Close();

        }
        public void LoadFiles()
        {
            _userList.Clear();

            // connect to database  

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM file";
            connection.Open();
            Reader = command.ExecuteReader();


            while (Reader.Read())
            {
                File _file = new File();
                try { _file.FileID = Reader.GetString(0); }
                catch (InvalidCastException) { }
                try { _file.Name = Reader.GetString(11); }
                catch (InvalidCastException) { }




                _fileList.Add(_file);
            }
            fileCbx.DataSource = _fileList;
            fileCbx.DisplayMember = "name";
            connection.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.*";
            if (open.ShowDialog() == DialogResult.OK)
            {               
                // image file path
                fileUrlTxtBx.Text = open.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string docID = Guid.NewGuid().ToString();
            string image = @""+ fileUrlTxtBx.Text;
            if (System.IO.File.Exists(image))
            {
                try
                {
                    string filepath = image;
                    string localPath = new Uri(filepath).LocalPath;
                    string remotePath = Helper.uploadUrl;

                    WebClient theClient = new WebClient();
                    byte[] responseArray = theClient.UploadFile(remotePath, filepath);
                    Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
                    theClient.Dispose();
                }
                catch (Exception c)
                {

                }
            }
            try
            {
                string Query = "INSERT INTO document(documentID, orgID, fileID, name, client, details,fileUrl, created, action,lawyer, sync,note, sizes) VALUES ('" + docID + "','A3CEA444-1F39-4F91-955D-0CA57E3C7962','" + this.fileCbx.Text + "','" + image + "','" + clientCbx.Text + "','" + this.fileCbx.Text + "','" + fileUrlTxtBx.Text + "','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','create','" + this.lawyerCbx.Text + "','f','" + noteTxt.Text + "','2.0');";
                MySqlConnection MyConn2 = new MySqlConnection(DBConnect.conn);

                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Information saved");

                MyConn2.Close();

                DocumentForm frm = new DocumentForm();
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewDoc_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
