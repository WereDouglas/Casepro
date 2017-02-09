using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        private string FeeID;
        public NewFee(string feeID)
        {
            FeeID = feeID;
            InitializeComponent();
            profile();
            loadData();
            loadUser();
            if (FeeID!="") {

                thisFee(FeeID);

            }
            printdoc1.PrintPage += new PrintPageEventHandler(printdoc1_PrintPage);
        }
        public void thisFee(string id)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT *,file.name As file,client.name As client FROM fees LEFT JOIN client ON client.clientID = fees.clientID LEFT JOIN file ON file.fileID = fees.fileID WHERE feeID = '"+id+"';";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                try { noLbl.Text = Reader.IsDBNull(7) ? "" : Reader.GetString(7); }
                catch (InvalidCastException) { }

                try { clientCbx.Text = Reader.IsDBNull(54) ? "" : Reader.GetString(54); }
                catch (InvalidCastException) { }

               fileCbx.Text = Reader.IsDBNull(53) ? "" : Reader.GetString(53);
                try
                {
                    amountTxt.Text = Reader.IsDBNull(10) ? "" : Reader.GetString(10);
                }
                catch { }

                methodCbx.Text = Reader.IsDBNull(9) ? "" : Reader.GetString(9);
                try
                {
                    balanceTxt.Text = Reader.IsDBNull(12) ? "" : Reader.GetString(12);
                }
                catch { }
               lawyerCbx.Text = Reader.IsDBNull(5) ? "" : Reader.GetString(5);
               detailsTxt.Text = Reader.IsDBNull(36) ? "" : Reader.GetString(36);
                //vatTxt.Text = Reader.IsDBNull(5) ? "" : Reader.GetString(5);
                paidCbx.Text = Reader.IsDBNull(14) ? "" : Reader.GetString(14);
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
                string Query = "INSERT INTO  `fees`(`feeID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `paid`, `invoice`, `vat`, `method`, `amount`, `received`, `balance`, `approved`, `signed`, `date`) VALUES ('" + feeID + "','"+Helper.orgID+"','" + clientID + "','" + fileID + "','" + detailsTxt.Text + "','" + lawyerCbx.Text + "','"+paidCbx.Text+"','"+noLbl.Text+"','"+vatTxt.Text+ "','" + methodCbx.Text + "','" + amountTxt.Text + "','" + Helper.username + "','" + balanceTxt.Text + "','false','false','" + Convert.ToDateTime(paymentDate.Text).ToString("yyyy-MM-dd") + "',`sync`='f');";
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
            string Query = "UPDATE `fees` SET `details`='"+detailsTxt.Text+ "',`sync`='f',`paid`='" + paidCbx.Text + "',`vat`='" + vatTxt.Text + "',`method`='" + methodCbx.Text + "',`amount`='" + methodCbx.Text + "',`received`='" + Helper.username + "',`balance`= '" + balanceTxt.Text + "',`approved`='" + approveCbx.Text +"',`signed`='" + Helper.username + "' WHERE feeID ='" + FeeID + "'";
            Helper.Execute(Query, DBConnect.conn);
            this.Close();
        }

        private void print_Click(object sender, EventArgs e)
        {
            Print(this.panel1);
        }
        public void Print(Panel pnl)
        {
            panel1 = pnl;
            GetPrintArea(pnl);
            previewdlg.Document = printdoc1;
            previewdlg.ShowDialog();
        }
        //Rest of the code
        Bitmap MemoryImage;
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (MemoryImage != null)
            {
                e.Graphics.DrawImage(MemoryImage, 0, 0);
                base.OnPaint(e);
            }
        }
        void printdoc1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage, (pagearea.Width / 2) - (this.panel1.Width / 2), this.panel1.Location.Y);
        }
    }
}
