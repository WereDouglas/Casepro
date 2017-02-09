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
    public partial class NewExpense : Form
    {
        Dictionary<string, string> ClientDictionary = new Dictionary<string, string>();
        Dictionary<string, string> FileDictionary = new Dictionary<string, string>();
        private string ExpID;
        public NewExpense(string expID)
        {
            ExpID = expID;
            InitializeComponent();
            profile();
            loadData();
            loadUser();
            if (ExpID != "")
            {

                thisExp(ExpID);

            }
        }
        public void thisExp(string id)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT *  FROM expenses LEFT JOIN client ON client.clientID = expenses.clientID LEFT JOIN file ON file.fileID = expenses.fileID WHERE expenses.expenseID = '" + id + "';";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                try { noLbl.Text = Reader.IsDBNull(36) ? "" : Reader.GetString(36); }
                catch (InvalidCastException) { }

                try { clientCbx.Text = Reader.IsDBNull(20) ? "" : Reader.GetString(20); }
                catch (InvalidCastException) { }

                fileCbx.Text = Reader.IsDBNull(38) ? "" : Reader.GetString(38);
                try
                {
                    amountTxt.Text = Reader.IsDBNull(7) ? "" : Reader.GetString(7);
                }
                catch { }

                methodCbx.Text = Reader.IsDBNull(6) ? "" : Reader.GetString(6);
                try
                {
                    balanceTxt.Text = Reader.IsDBNull(8) ? "" : Reader.GetString(8);
                }
                catch { }
                lawyerCbx.Text = Reader.IsDBNull(36) ? "" : Reader.GetString(36);
                detailsTxt.Text = Reader.IsDBNull(15) ? "" : Reader.GetString(15);
                //vatTxt.Text = Reader.IsDBNull(5) ? "" : Reader.GetString(5);
                paidCbx.Text = Reader.IsDBNull(10) ? "" : Reader.GetString(10);
                reasonTxt.Text = Reader.IsDBNull(14) ? "" : Reader.GetString(14);
                outcomeTxt.Text = Reader.IsDBNull(15) ? "" : Reader.GetString(15);
                try
                {
                    deadlineDate.Text = Convert.ToDateTime(Reader.IsDBNull(44) ? "" : Reader.GetString(44)).ToString("dd-MM-yyyy");
                }
                catch { }

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
            noLbl.Text = Helper.code + "/" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("ss") + DateTime.Now.ToString("yyyy");

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
            try
            {
                fileID = FileDictionary[fileCbx.Text];
                // loadTenant(TenantID);
            }
            catch
            {


            }
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
                string Query = "INSERT INTO  `expenses`(`expenseID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `method`, `amount`, `no`, `balance`, `paid`, `date`, `approved`, `signed`, `reason`, `outcome`, `deadline`,sync) VALUES ('" + feeID + "','" + Helper.orgID + "','" + clientID + "','" + fileID + "','" + detailsTxt.Text + "','" + lawyerCbx.Text + "','" + methodCbx.Text + "','" + amountTxt.Text + "','" + noLbl.Text + "','" + balanceTxt.Text + "','" + paidCbx.Text + "','" + Convert.ToDateTime(paymentDate.Text).ToString("yyyy-MM-dd") + "','false','" + Helper.username + "','" + reasonTxt.Text+"','" + outcomeTxt.Text + "','" + Convert.ToDateTime(deadlineDate.Text).ToString("yyyy-MM-dd") + "','f');";
                Helper.Execute(Query, DBConnect.conn);
                MessageBox.Show("Information saved");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Query = "UPDATE `expenses` SET `details`='" + detailsTxt.Text + "',`sync`='f',`paid`='" + paidCbx.Text + "',`vat`='" + vatTxt.Text + "',`method`='" + methodCbx.Text + "',`amount`='" + methodCbx.Text + "',`received`='" + Helper.username + "',`balance`= '" + balanceTxt.Text + "',`approved`='" + approveCbx.Text + "',`signed`='" + Helper.username + "',reason = '"+reasonTxt.Text+"' outcome ='"+outcomeTxt.Text+"',deadline= '"+ Convert.ToDateTime(deadlineDate.Text).ToString("yyyy-MM-dd") + "' WHERE expenseID ='" + ExpID + "'";
            Helper.Execute(Query, DBConnect.conn);
            this.Close();
        }
    }
}
