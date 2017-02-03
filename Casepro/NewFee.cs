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
    public partial class NewFee : Form
    {
        Dictionary<string, string> ClientDictionary = new Dictionary<string, string>();
        Dictionary<string, string> FileDictionary = new Dictionary<string, string>();

        public NewFee()
        {
            InitializeComponent();
            profile();
            loadData();
            loadUser();
        }
        void profile()
        {
            try
            {

                var request = WebRequest.Create(Helper.imageUrl + Helper.logo);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    logoBx.Image = Bitmap.FromStream(stream);
                    //logoBx.Image.

                }
            }
            catch { }
            addressLbl.Text = Helper.address;
            nameLbl.Text = Helper.orgName;
           // addressLbl.Text = Helper.username;
            // contactLbl.Text = Helper.contact;
            noLbl.Text = Helper.code+"/"+DateTime.Now.ToString("dd")+ DateTime.Now.ToString("mm")+ DateTime.Now.ToString("hh")+DateTime.Now.ToString("ss") + DateTime.Now.ToString("yyyy");

        }
        public void loadData()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT name,clientID FROM client";
            connection.Open();
            Reader = command.ExecuteReader();

            List<string> users = new List<string>();
            while (Reader.Read())
            {
                users.Add(Reader.GetString(0));
                ClientDictionary.Add(Reader.GetString(0), Reader.GetString(1));
               /// System.Diagnostics.Debug.WriteLine(Reader.GetString(1));
                // tenantDictionary.Add(Reader.GetString(0), Reader.GetString(0));

            }
            clientCbx.DataSource = users;
            connection.Close();
        }
        public void loadUser()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT name FROM users";
            connection.Open();
            Reader = command.ExecuteReader();

            List<string> users = new List<string>();
            while (Reader.Read())
            {
                users.Add(Reader.GetString(0));
              

            }
            lawyerCbx.DataSource = users;
            connection.Close();
        }
        string clientID;
        string fileID;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void loadFile(string client)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT name,fileID FROM file WHERE client = '"+client+"'";
            connection.Open();
            Reader = command.ExecuteReader();
            List<string> users = new List<string>();
            FileDictionary = new Dictionary<string, string>();
            while (Reader.Read())
            {
                users.Add(Reader.GetString(0));
                FileDictionary.Add(Reader.GetString(0), Reader.GetString(1));
            }
            fileCbx.DataSource = users;
            connection.Close();
        }

        private void clientCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientID = ClientDictionary[clientCbx.Text];
            loadFile(clientCbx.Text);
        }

        private void fileCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileID = FileDictionary[fileCbx.Text];
           // loadTenant(TenantID);
        }

        private void amountTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                   wordsLbl.Text = Helper.NumberToWords(Convert.ToInt32(amountTxt.Text));
          }
            catch
            {


            }

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            string feeID = Guid.NewGuid().ToString();
           
            try
            {
                string Query = "INSERT INTO  `fees`(`feeID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `paid`, `invoice`, `vat`, `method`, `amount`, `received`, `balance`, `approved`, `signed`, `date`) VALUES ('" + feeID + "','"+Helper.orgID+"','" + clientID + "','" + fileID + "','" + detailsTxt.Text + "','" + lawyerCbx.Text + "','"+paidCbx.Text+"','"+noLbl.Text+"','"+vatTxt.Text+ "','" + methodCbx.Text + "','" + amountTxt.Text + "','" + Helper.username + "','" + balanceTxt.Text + "','false','false','" + Convert.ToDateTime(paymentDate.Text).ToString("yyyy-MM-dd") + "');";
                Helper.Execute(Query, DBConnect.conn);              
                MessageBox.Show("Information saved");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
