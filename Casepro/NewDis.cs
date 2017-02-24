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
    public partial class NewDis : Form
    {
        Dictionary<string, string> ClientDictionary = new Dictionary<string, string>();
        Dictionary<string, string> FileDictionary = new Dictionary<string, string>();
        private string DisID;
        public NewDis(string disID)
        {
            DisID = disID;
            InitializeComponent();
            profile();
            loadData();
            loadUser();
            if (DisID != "")
            {
                thisDis(DisID);
            }
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
                }
            }
            catch { }
            addressLbl.Text = Helper.address;
            nameLbl.Text = Helper.orgName;
             noLbl.Text = Helper.code + "/" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("ss") + DateTime.Now.ToString("yyyy");

        }
        public void thisDis(string id)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT *,file.name As file,client.name As client FROM disbursements LEFT JOIN client ON client.clientID = disbursements.clientID LEFT JOIN file ON file.fileID = disbursements.fileID WHERE disbursements.disbursementID = '" + id + "';";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                try { noLbl.Text = Reader.IsDBNull(7) ? "" : Reader.GetString(7); }
                catch (InvalidCastException) { }

                try { clientCbx.Text = Reader.IsDBNull(52) ? "" : Reader.GetString(52); }
                catch (InvalidCastException) { }

                fileCbx.Text = Reader.IsDBNull(40) ? "" : Reader.GetString(40);
                try
                {
                    amountTxt.Text = Reader.IsDBNull(9) ? "" : Reader.GetString(9);
                }
                catch { }

                methodCbx.Text = Reader.IsDBNull(8) ? "" : Reader.GetString(8);
                try
                {
                    balanceTxt.Text = Reader.IsDBNull(11) ? "" : Reader.GetString(11);
                }
                catch { }
                lawyerCbx.Text = Reader.IsDBNull(10) ? "" : Reader.GetString(10);
                detailsTxt.Text = Reader.IsDBNull(35) ? "" : Reader.GetString(35);
                //vatTxt.Text = Reader.IsDBNull(5) ? "" : Reader.GetString(5);
                paidCbx.Text = Reader.IsDBNull(13) ? "" : Reader.GetString(13);
                try
                {
                    wordsLbl.Text = Helper.NumberToWords(Convert.ToInt32(amountTxt.Text));
                }
                catch
                {


                }


            }
            connection.Close();
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
            command.CommandText = "SELECT name,fileID FROM file WHERE client = '" + client + "'";
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
            

        }

        private void Submit_Click_1(object sender, EventArgs e)
        {
            string feeID = Guid.NewGuid().ToString();

            try
            {
                string Query = "INSERT INTO  `disbursements`(`disbursementID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `paid`, `invoice`, `method`, `amount`, `received`, `balance`, `approved`, `signed`, `date`,`sync`) VALUES ('" + feeID + "','" + Helper.orgID + "','" + clientID + "','" + fileID + "','" + detailsTxt.Text + "','" + lawyerCbx.Text + "','" + paidCbx.Text + "','" + noLbl.Text + "','" + methodCbx.Text + "','" + amountTxt.Text + "','" + Helper.username + "','" + balanceTxt.Text + "','false','false','" + Convert.ToDateTime(paymentDate.Text).ToString("yyyy-MM-dd") + "','f');";
                Helper.Execute(Query, DBConnect.conn);
                MessageBox.Show("Information saved");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Query = "UPDATE `disbursements` SET `details`='" + detailsTxt.Text + "',`sync`='f',`paid`='" + paidCbx.Text + "',`vat`='" + vatTxt.Text + "',`method`='" + methodCbx.Text + "',`amount`='" + methodCbx.Text + "',`received`='" + Helper.username + "',`balance`= '" + balanceTxt.Text + "',`approved`='" + approveCbx.Text + "',`signed`='" + Helper.username + "' WHERE disbursements.disbursementID ='" + DisID + "'";
            Helper.Execute(Query, DBConnect.conn);
            this.Close();
        }
    }
}

