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
    public partial class OrganisationForm : Form
    {
        public string id;
        public OrganisationForm()
        {
            InitializeComponent();
            thisOrg();
        }
        public void thisOrg()
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM  org LIMIT 1";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                id = Reader.IsDBNull(0) ? "" : Reader.GetString(0);
                try { nameTxtBx.Text = Reader.IsDBNull(1) ? "" : Reader.GetString(1); }
                catch (InvalidCastException) { }

                try { emailTxtBx.Text = Reader.IsDBNull(7) ? "" : Reader.GetString(7); }
                catch (InvalidCastException) { }
             
                try
                {
                    addressTxtBx.Text = Reader.IsDBNull(6) ? "" : Reader.GetString(6);
                }
                catch { }
                currencyTxt.Text = Reader.IsDBNull(10) ? "" : Reader.GetString(10);
                countryTxt.Text = Reader.IsDBNull(12) ? "" : Reader.GetString(12);
                cityTxt.Text = Reader.IsDBNull(13) ? "" : Reader.GetString(13);
                tinTxt.Text = Reader.IsDBNull(15) ? "" : Reader.GetString(15);
                vatTxt.Text = Reader.IsDBNull(16) ? "" : Reader.GetString(16);
                //topTxt.Text = Reader.IsDBNull(13) ? "" : Reader.GetString(13);

                try
                {
                    regionTxt.Text = Reader.IsDBNull(12) ? "" : Reader.GetString(12);
                }
                catch { }
                try
                {
                    var request = WebRequest.Create(Helper.imageUrl + Reader.GetString(9).ToString());

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

        private void button1_Click(object sender, EventArgs e)
        {

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

          
                Query = "UPDATE `org` SET `name`='" + this.nameTxtBx.Text + "',`email`='" + this.emailTxtBx.Text + "',`image`='" + id.Trim() + ".jpg" + "',`currency`='" + this.currencyTxt.Text + "',`country`='" + this.countryTxt.Text + "',`address`='" + this.addressTxtBx.Text + "',`sync`='f',`region`='" + this.regionTxt.Text + "',`city`='" + cityTxt.Text + "',`action`='update',`tin`='" + tinTxt.Text + "',`vat`='" + vatTxt.Text + "' WHERE orgID = '" + id + "'";

            Helper.Execute(Query, DBConnect.conn);
           
            MessageBox.Show("Information Updated");
           // LoginForm frm = new LoginForm();
          
           //frm.Show();
            //this.Close();

        }

        private void OrganisationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           // LoginForm frm = new LoginForm();
            //frm.Show();
            //this.Hide();
        }

        private void addressTxtBx_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
