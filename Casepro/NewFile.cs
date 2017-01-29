using Casepro.Model;
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

namespace Casepro
{
    public partial class NewFile : Form
    {
        private User _user = new User();
        private List<User> _userList = new List<User>();

        private Client _client = new Client();
        private List<Client> _clientList = new List<Client>();
        private string id;
        public NewFile(string fileID)
        {
            id = fileID;
            InitializeComponent();
            LoadUsers();
            LoadClients();
            saveBtn.Visible = true;

            if (id == "")
            {
               saveBtn.Visible = true;
                updateBtn.Visible = false;
            }
            else
            {
                thisFile(id);
               // saveBtn.Visible = false;
                updateBtn.Visible = true;
            }
            
            //  MessageBox.Show(id);

        }
        public void thisFile(string id)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM file WHERE fileID= '" + id + "'";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                try { nameTxt.Text = Reader.GetString(11); }
                catch (InvalidCastException) { }

                try { caseTxt.Text = Reader.GetString(5); }
                catch (InvalidCastException) { }
                subjectTxt.Text = Reader.GetString(8);
                typeCbx.Text = Reader.GetString(7);
                try
                {
                    openedDate.Text = Reader.GetString(18);
                }
                catch { }
                lawCbx.Text = Reader.GetString(10);
                citationTxt.Text = Reader.GetString(9);
                descriptionTxt.Text = Reader.GetString(6);
                clientCbx.Text = Reader.GetString(2);
                lawyerCbx.Text = Reader.GetString(4);
                try
                {
                    noLbl.Text = Reader.GetString(5);
                }
                catch { }
                try
                {
                    contactTxt.Text = Reader.GetString(3);
                }
                catch { }
                try
                {
                    contactpersonTxt.Text = Reader.GetString(20);
                }
                catch { }
                try
                {
                    contactTxt.Text = Reader.GetString(21);
                }
                catch { }
                dueDate.Text = Reader.GetString(19);



            }
            connection.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {

            string fileID = Guid.NewGuid().ToString();

            string Query = "INSERT INTO file(fileID,orgID, client, contact, lawyer, no, details, type, subject, citation, law, name, created, status, sync,`case`,note, progress, opened, due, contact_person, contact_number) VALUES ('" + fileID + "','A3CEA444-1F39-4F91-955D-0CA57E3C7962','" + this.clientCbx.Text + "','" + this.contactTxt.Text + "','" + this.lawyerCbx.Text + "','" + this.noLbl.Text + "','" + this.descriptionTxt.Text + "','" + this.typeCbx.Text + "','" + subjectTxt.Text + "','" + this.citationTxt.Text + "','" + this.lawCbx.Text + "','" + this.nameTxt.Text + "','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','" + this.stateCbx.Text + "','false','" + this.caseTxt.Text + "','" + this.noteTxt.Text + "','" + this.progressTxt.Text + "','" + Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(this.dueDate.Text).ToString("yyyy-MM-dd") + "','" + this.contactpersonTxt.Text + "','" + this.contactTxt.Text + "');";
            MySqlConnection MyConn2 = new MySqlConnection(DBConnect.conn);

            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();
            MessageBox.Show("Information saved");
            FileForm frm = new FileForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();


        }

        private void NewFile_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileForm frm = new FileForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            string fileID = Guid.NewGuid().ToString();
            string Query = "UPDATE file SET client='" + this.clientCbx.Text + "', contact='" + this.contactTxt.Text + "' , lawyer='" + this.lawyerCbx.Text + "', no='" + this.noLbl.Text + "', details='" + this.descriptionTxt.Text + "', type='" + this.typeCbx.Text + "', subject='" + subjectTxt.Text + "', citation='" + this.citationTxt.Text + "', law='" + this.lawCbx.Text + "', name='" + this.nameTxt.Text + "', status='" + this.stateCbx.Text + "',`case`='" + this.caseTxt.Text + "',note='" + this.noteTxt.Text + "', progress='" + this.progressTxt.Text + "', opened='" + Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "', due='" + Convert.ToDateTime(this.dueDate.Text).ToString("yyyy-MM-dd") + "', contact_person='" + this.contactpersonTxt.Text + "', contact_number='" + this.contactTxt.Text + "' WHERE fileID ='"+id+"'";
            MySqlConnection MyConn2 = new MySqlConnection(DBConnect.conn);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();
            MessageBox.Show("Information Updated");
            FileForm frm = new FileForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string fileID = Guid.NewGuid().ToString();
            string Query = "DELETE from file WHERE fileID ='" + id + "'";
            MySqlConnection MyConn2 = new MySqlConnection(DBConnect.conn);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();
            MessageBox.Show("Information deleted");
            FileForm frm = new FileForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();

        }
    }
}
