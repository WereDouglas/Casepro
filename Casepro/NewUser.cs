using Casepro.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
    public partial class NewUser : Form
    {
        WebCam webcam;
        private string id;
        private User _user = new User();
        private List<User> _userList = new List<User>();
        public NewUser(string userID)
        {
            id = userID;
            InitializeComponent();
            LoadUsers();

            if (id == "")
            {

                updateBtn.Visible = false;
            }
            else
            {
                thisUser(id);
                // saveBtn.Visible = false;
                updateBtn.Visible = true;
            }
        }
        public void thisUser(string id)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM users WHERE userID= '" + id + "'";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                try { nameTxtBx.Text = Reader.IsDBNull(2) ? "" : Reader.GetString(2); }
                catch (InvalidCastException) { }

                try { emailTxtBx.Text = Reader.IsDBNull(3) ? "" : Reader.GetString(3); }
                catch (InvalidCastException) { }
                designationCbx.Text = Reader.IsDBNull(5) ? "" : Reader.GetString(5);
                contactTxtBx.Text = Reader.IsDBNull(7) ? "" : Reader.GetString(7);
                try
                {
                    addressTxtBx.Text = Reader.IsDBNull(9) ? "" : Reader.GetString(9);
                }
                catch { }
                chargeTxtBx.Text = Reader.IsDBNull(13) ? "" : Reader.GetString(13);
                supervisorCbx.Text = Reader.IsDBNull(14) ? "" : Reader.GetString(14);
                try
                {
                    statusCbx.Text = Reader.IsDBNull(6) ? "" : Reader.GetString(6);
                }
                catch { }
                try
                {
                    var request = WebRequest.Create(Helper.imageUrl + (Reader.IsDBNull(8) ? "" : Reader.GetString(8)));

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    imgCapture.Image = Bitmap.FromStream(stream);

                }
                }
                catch { }

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
            supervisorCbx.DataSource = _userList;
            supervisorCbx.DisplayMember = "name";
            connection.Close();

        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UserForm frm = new UserForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            webcam.ResolutionSetting();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            webcam.AdvanceSetting();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Helper.Exists("users", "name", nameTxtBx.Text))
            {
                MessageBox.Show("User name exists");
                return;
            }
            if (Helper.Exists("users", "contact", contactTxtBx.Text))
            {
                MessageBox.Show("User contact exists");
                return;
            }
            string userID = Guid.NewGuid().ToString();
            string paths = @"c:\Case\images";
            if (!Directory.Exists(paths))
            {
                DirectoryInfo dim = Directory.CreateDirectory(paths);
                Console.WriteLine("The directory was created successfully at {0}.",
                Directory.GetCreationTime(paths));
            }

            string filename = @"c:\Case\\images\" + userID.Trim() + ".jpg";
            FileStream fstream = new FileStream(filename, FileMode.Create);
            imgCapture.Image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            fstream.Close();


            string image = @"c:\Case\\images\" + userID.Trim() + ".jpg";
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
                string Query = "INSERT INTO users(userID, orgID, name, email, password, designation, status, contact, image, address, category, created, sync, charge, supervisor) VALUES ('" + userID + "','" + Helper.orgID + "','" + this.nameTxtBx.Text + "','" + this.emailTxtBx.Text + "','" + Helper.MD5Hash(this.passwordTxtBx.Text) + "','" + this.designationCbx.Text + "','" + this.statusCbx.Text + "','" + this.contactTxtBx.Text + "','" + userID.Trim() + ".jpg" + "','" + this.addressTxtBx.Text + "','staff','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','f','" + this.chargeTxtBx.Text + "','" + supervisorCbx.Text + "');";
                Helper.Execute(Query, DBConnect.conn);
                MessageBox.Show("Information saved");
                UserForm frm = new UserForm();
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void confirmTxtbx_Leave(object sender, EventArgs e)
        {
            if (confirmTxtBx.Text != passwordTxtBx.Text)
            {

                MessageBox.Show("Passwords donot match ");
            }
        }

        private void addressTxtBx_TextChanged(object sender, EventArgs e)
        {

        }
        private void bntCapture_Click(object sender, EventArgs e)
        {
            imgCapture.Image = imgVideo.Image;
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            Helper.SaveImageCapture(imgCapture.Image);
        }

        private void bntStart_Click_1(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void bntStop_Click_1(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void bntContinue_Click_1(object sender, EventArgs e)
        {
            webcam.Continue();
        }

        private void bntCapture_Click_1(object sender, EventArgs e)
        {
            imgCapture.Image = imgVideo.Image;
        }

        private void bntSave_Click_1(object sender, EventArgs e)
        {
            Helper.SaveImageCapture(imgCapture.Image);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            UserForm frm = new UserForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {

            string filename = @"c:\Case\\images\" + id.Trim() + ".jpg";
            FileStream fstream = new FileStream(filename, FileMode.Create);
            imgCapture.Image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            fstream.Close();


            string image = @"c:\Case\\images\" + id.Trim() + ".jpg";
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

            string Query;

            if (passwordTxtBx.Text != "")
            {
                Query = "UPDATE `users` SET `name`='" + this.nameTxtBx.Text + "',`email`='" + this.emailTxtBx.Text + "',`image`='" + id.Trim() + ".jpg" + "' ,`password`='" + Helper.MD5Hash(this.passwordTxtBx.Text) + "',`designation`='" + this.designationCbx.Text + "',`status`='" + this.statusCbx.Text + "',`contact`='" + this.contactTxtBx.Text + "',`address`='" + this.addressTxtBx.Text + "',`sync`='f',`charge`='" + this.chargeTxtBx.Text + "',`supervisor`='" + supervisorCbx.Text + "',`action`='update' WHERE userID = '" + id + "'";

            }
            else
            {
                Query = "UPDATE `users` SET `name`='" + this.nameTxtBx.Text + "',`email`='" + this.emailTxtBx.Text + "',`image`='" + id.Trim() + ".jpg" + "',`designation`='" + this.designationCbx.Text + "',`status`='" + this.statusCbx.Text + "',`contact`='" + this.contactTxtBx.Text + "',`address`='" + this.addressTxtBx.Text + "',`sync`='f',`charge`='" + this.chargeTxtBx.Text + "',`supervisor`='" + supervisorCbx.Text + "',`action`='update' WHERE userID = '" + id + "'";

            }
            Helper.Execute(Query,DBConnect.conn);          
            MessageBox.Show("Information Updated");
            UserForm frm = new UserForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm or no?", "Confirm deletion", MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string fileID = Guid.NewGuid().ToString();
                string Query = "DELETE from user WHERE userID ='" + id + "'";
                Helper.Execute(Query, DBConnect.conn);
                MessageBox.Show("Information deleted");
                UserForm frm = new UserForm();
                frm.MdiParent = MainForm.ActiveForm;
                frm.Dock = DockStyle.Fill;
                frm.Show();
                this.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                imgCapture.Image = new Bitmap(open.FileName);
                // image file path
                fileUrlTxtBx.Text = open.FileName;
            }
        }

        private void fileUrlTxtBx_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewUser_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
