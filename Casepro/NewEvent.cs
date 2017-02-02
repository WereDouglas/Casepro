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
    public partial class NewEvent : Form
    {
        private User _user = new User();
        private List<User> _userList = new List<User>();
        private Client _client = new Client();
        private List<Client> _clientList = new List<Client>();

        private File _file = new File();
        private List<File> _fileList = new List<File>();
        private string id;
        public NewEvent(string fileID)
        {
            id = fileID;
            InitializeComponent();
            LoadUsers();
            LoadClients();
            LoadFiles();
            if (id == "")
            {

            }
            else
            {
                thisFile(id);
                // saveBtn.Visible = false;
                // updateBtn.Visible = true;
            }
            startMinTxt.Text = "00";
            endMinTxt.Text = "00";
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
        public void thisFile(string id)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM events WHERE id= '" + id + "'";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                try { openedDate.Text = (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)); }
                catch (InvalidCastException) { }

                try { clientCbx.Text = (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)); }
                catch (InvalidCastException) { }
                fileCbx.Text = (Reader.IsDBNull(5) ? "none" : Reader.GetString(5));
                lawyerCbx.Text = (Reader.IsDBNull(4) ? "none" : Reader.GetString(4));

                detailsTxt.Text = (Reader.IsDBNull(1) ? "none" : Reader.GetString(1));
                daysTxt.Text = (Reader.IsDBNull(11) ? "none" : Reader.GetString(11));
                priorityCbx.Text = (Reader.IsDBNull(13) ? "none" : Reader.GetString(13));

                startHrTxt.Text = Convert.ToDateTime( (Reader.IsDBNull(2) ? "none" : Reader.GetString(2))).ToString("HH");
                startMinTxt.Text = Convert.ToDateTime((Reader.IsDBNull(2) ? "none" : Reader.GetString(2))).ToString("MM");
               endHrTxt.Text = Convert.ToDateTime((Reader.IsDBNull(3) ? "none" : Reader.GetString(3))).ToString("HH");
                endMinTxt.Text = Convert.ToDateTime((Reader.IsDBNull(3) ? "none" : Reader.GetString(3))).ToString("MM");

                string notify = (Reader.IsDBNull(16) ? "false" : Reader.GetString(15));
                string court = (Reader.IsDBNull(16) ? "false" : Reader.GetString(12));

                progressTxt.Text = (Reader.IsDBNull(16) ? "none" : Reader.GetString(16));

                if (notify=="true") { notifyChk.Checked = true; }
                if (court == "true") { courtChk.Checked = true; }
               


            }
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
        string court;
        string notify;

        private void button2_Click(object sender, EventArgs e)
        {
            string ID = Guid.NewGuid().ToString();
            var start = Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "T" + this.startHrTxt.Text + ":" + startMinTxt.Text + ":00";
            var end = Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "T" + this.endHrTxt.Text + ":" + endMinTxt.Text + ":00";
            court = "false";
            notify = "false";
            if (courtChk.Checked)
            {
                court = "true";
            }
            if (notifyChk.Checked)
            {
                notify = "true";
            }
            string Query = "INSERT INTO `events`(`id`, `name`, `start`, `end`, `user`, `file`, `created`, `action`, `status`, `orgID`, `date`, `hours`, `court`, `notify`,`priority`, `sync`,`progress`,`client`) VALUES ('" + ID + "','" + this.detailsTxt.Text + "','" + start + "','" + end + "','" + lawyerCbx.Text + "','" + fileCbx.Text + "','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','create','" + progressTxt.Text + "','" + Helper.orgID + "','" + Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "','1','" + court + "','" + notify + "','" + priorityCbx.Text + "','f','" + progressTxt.Text + "','" + clientCbx.Text + "');";
            Helper.Execute(Query, DBConnect.conn);
            MessageBox.Show("Information saved");
         
           // var request = (HttpWebRequest)WebRequest.Create(Helper.msgUrl);
            //request.GetResponse();           
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewEvent_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fileID = Guid.NewGuid().ToString();
            string Query = "DELETE from events WHERE id ='" + id + "'";
            Helper.Execute(Query, DBConnect.conn);
            MessageBox.Show("Information deleted");           
            this.Close();

        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            var start = Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "T" + this.startHrTxt.Text + ":" + startMinTxt.Text + ":00";
            var end = Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "T" + this.endHrTxt.Text + ":" + endMinTxt.Text + ":00";

            court = "false";
            notify = "false";
            if (courtChk.Checked)
            {
                court = "true";
            }
            if (notifyChk.Checked)
            {
                notify = "true";
            }

            string fileID = Guid.NewGuid().ToString();
            string Query = "UPDATE `events` SET `name`='" + this.detailsTxt.Text + "',`start`='" + start + "',`end`='" + end + "',`user`='" + lawyerCbx.Text + "',`file`='" + fileCbx.Text + "',`action`='update',`status`='true',`date`='"+ Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "',`hours`='"+daysTxt.Text+ "',`court`='" + court + "',`priority`='" + priorityCbx.Text + "',`sync`='f',`notify`='" + notify + "',`progress`='" + progressTxt.Text + "',`client`='" + clientCbx.Text + "' WHERE id ='" + id + "'";
            Helper.Execute(Query, DBConnect.conn);
           
            this.Close();
        }
    }
}
