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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
    public partial class NewClient : Form
    {
        WebCam webcam;
        private string id;
        private Client _client = new Client();
        private List<Client> _clientList = new List<Client>();
        public NewClient(string clientID)
        {
            id = clientID;
            InitializeComponent();
            LoadClients();

            if (id == "")
            {

                updateBtn.Visible = false;
            }
            else
            {
                thisClient(id);
                // saveBtn.Visible = false;
                updateBtn.Visible = true;
            }
        }
        public void thisClient(string id)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM client WHERE clientID= '" + id + "'";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                try { nameTxtBx.Text = Reader.IsDBNull(2) ? "" : Reader.GetString(2); }
                catch (InvalidCastException) { }

                try { emailTxtBx.Text = Reader.IsDBNull(3) ? "" : Reader.GetString(3); }
                catch (InvalidCastException) { }

                contactTxtBx.Text = Reader.IsDBNull(7) ? "" : Reader.GetString(7);
                try
                {
                    addressTxtBx.Text = Reader.IsDBNull(9) ? "" : Reader.GetString(9);
                }
                catch { }

                supervisorCbx.Text = Reader.IsDBNull(10) ? "" : Reader.GetString(10);
                try
                {
                    statusCbx.Text = Reader.IsDBNull(5) ? "" : Reader.GetString(5);
                }
                catch { }
                try
                {
                    var request = WebRequest.Create(Helper.imageUrl + (Reader.IsDBNull(6) ? "default.png" : Reader.GetString(6)));

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
        public void LoadClients()
        {
            _clientList.Clear();

            // connect to database  

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT userID, orgID, name, email, password, designation, status, contact, image, address, category, created,sync, charge, supervisor FROM users";
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
            supervisorCbx.DataSource = _clientList;
            supervisorCbx.DisplayMember = "name";
            connection.Close();

        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClientForm frm = new ClientForm();
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



        private void button4_Click_1(object sender, EventArgs e)
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

        private void bntStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void bntStop_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void bntContinue_Click(object sender, EventArgs e)
        {
            webcam.Continue();
        }

        private void bntCapture_Click_2(object sender, EventArgs e)
        {
            imgCapture.Image = imgVideo.Image;
        }

        private void bntSave_Click_2(object sender, EventArgs e)
        {
            Helper.SaveImageCapture(imgCapture.Image);
        }

        private void toolStripLabel2_Click_1(object sender, EventArgs e)
        {
            webcam.AdvanceSetting();
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            webcam.ResolutionSetting();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Helper.Exists("client", "name", nameTxtBx.Text))
            {
                MessageBox.Show("Client name exists");
                return;
            }
            string clientID = Guid.NewGuid().ToString();
            string paths = @"c:\Case\images";
            if (!Directory.Exists(paths))
            {
                DirectoryInfo dim = Directory.CreateDirectory(paths);
                Console.WriteLine("The directory was created successfully at {0}.",
                Directory.GetCreationTime(paths));
            }

            string filename = @"c:\Case\\images\" + clientID.Trim() + ".jpg";
            FileStream fstream = new FileStream(filename, FileMode.Create);
            imgCapture.Image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            fstream.Close();


            string image = @"c:\Case\\images\" + clientID.Trim() + ".jpg";
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
                string Query = "INSERT INTO client(clientID, orgID, name, email, password, status, contact, image, address, created, sync,action, lawyer,registration) VALUES ('" + clientID + "','"+ Helper.orgID + "','" + this.nameTxtBx.Text + "','" + this.emailTxtBx.Text + "','" + Helper.MD5Hash(this.passwordTxtBx.Text) + "','" + this.statusCbx.Text + "','" + this.contactTxtBx.Text + "','" + clientID.Trim() + ".jpg" + "','" + this.addressTxtBx.Text + "','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','f','create','" + supervisorCbx.Text + "','" + Convert.ToDateTime(this.regDate.Text).ToString("yyyy-MM-dd") + "');";
                Helper.Execute(Query, DBConnect.conn);
                MessageBox.Show("Information saved");          
                ClientForm frm = new ClientForm();
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateBtn_Click_1(object sender, EventArgs e)
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
                Query = "UPDATE `client` SET `name`='" + this.nameTxtBx.Text + "',`email`='" + this.emailTxtBx.Text + "',`image`='" + id.Trim() + ".jpg" + "' ,`password`='" + Helper.MD5Hash(this.passwordTxtBx.Text) + "',`status`='" + this.statusCbx.Text + "',`contact`='" + this.contactTxtBx.Text + "',`address`='" + this.addressTxtBx.Text + "',`sync`='f',`lawyer`='" + supervisorCbx.Text + "',`action`='update' WHERE clientID = '" + id + "'";

            }
            else
            {
                Query = "UPDATE `client` SET `name`='" + this.nameTxtBx.Text + "',`email`='" + this.emailTxtBx.Text + "',`image`='" + id.Trim() + ".jpg" + "',`status`='" + this.statusCbx.Text + "',`contact`='" + this.contactTxtBx.Text + "',`address`='" + this.addressTxtBx.Text + "',`sync`='f',`lawyer`='" + supervisorCbx.Text + "',`action`='update' WHERE clientID = '" + id + "'";

            }
            Helper.Execute(Query, DBConnect.conn);
            MessageBox.Show("Information Updated");
            ClientForm frm = new ClientForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string fileID = Guid.NewGuid().ToString();
            string Query = "DELETE from client WHERE clientID ='" + id + "'";
            Helper.Execute(Query, DBConnect.conn);
            MessageBox.Show("Information deleted");
            ClientForm frm = new ClientForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            ClientForm frm = new ClientForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();

        }

        private void NewClient_Load_1(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            ClientForm frm = new ClientForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();

        }

        private void NewClient_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
