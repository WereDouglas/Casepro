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
    public partial class NewPetty : Form
    {
        public NewPetty(string id)
        {
            InitializeComponent();
            profile();
            loadUser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
            noLbl.Text = Helper.code + "/" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("ss") + DateTime.Now.ToString("yyyy");

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

        private void Submit_Click(object sender, EventArgs e)
        {
            string feeID = Guid.NewGuid().ToString();
            try
            {
                string Query = "INSERT INTO `petty`(`id`, `name`, `unit`, `qty`, `total`, `date`, `paid`, `created`, `user`, `reason`, `method`, `approved`,`orgID`,`sync`)  VALUES ('" + noLbl.Text + "','"+itemTxt.Text+"','" + costTxt.Text + "','" + qtyTxt.Text + "','" + amountTxt.Text + "','" + Convert.ToDateTime(paymentDate.Text).ToString("yyyy-MM-dd") + "','"+paidCbx.Text+ "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','"+lawyerCbx.Text+"','"+reasonTxt.Text+"','"+methodCbx.Text+"','"+approveCbx.Text+"','" + Helper.orgID + "','f');";
                Helper.Execute(Query, DBConnect.conn);
                MessageBox.Show("Information saved");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nameLbl_Click(object sender, EventArgs e)
        {

        }

        private void qtyTxt_TextChanged(object sender, EventArgs e)
        {
            amountTxt.Text = (Convert.ToDouble(costTxt.Text) * Convert.ToDouble(qtyTxt.Text)).ToString();
            try
            {
                wordsLbl.Text = Helper.NumberToWords(Convert.ToInt32(amountTxt.Text));
            }
            catch
            {


            }
        }
    }
}
