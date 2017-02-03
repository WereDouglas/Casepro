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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
    public partial class UserForm : Form
    {
        public User _user = new User();
        public static List<User> _userList = new List<User> { };
        public static DataTable table = new DataTable();
        DataTable t = new DataTable();
        public UserForm()
        {
            InitializeComponent();
            LoadUsers();
            //dtGrid.DataSource = _userList;
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
            // create and execute query  

           
            t.Columns.Add("userID");
            t.Columns.Add("uri");
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));
            t.Columns.Add("Name");
            t.Columns.Add("E-mail");
            t.Columns.Add("Contact");
            t.Columns.Add("Designation");
            t.Columns.Add("Address");
            t.Columns.Add("Supervisor");
            t.Columns.Add("Status");
            t.Columns.Add("Charge");
            t.Columns.Add("Edit");  //0 
            t.Columns.Add("Delete");  //0 


            searchCbx.Items.Add("Name");
            searchCbx.Items.Add("E-mail");
            searchCbx.Items.Add("Contact");
            searchCbx.Items.Add("Designation");
            searchCbx.Items.Add("Status");



            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Black), 0f, 0f);
            }
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

                t.Rows.Add(new object[] { Reader.GetString(0), Helper.imageUrl + (Reader.IsDBNull(8) ? "" : Reader.GetString(8)), false, b, Reader.IsDBNull(2) ? "" : Reader.GetString(2), Reader.IsDBNull(3) ? "" : Reader.GetString(3), Reader.IsDBNull(7) ? "" : Reader.GetString(7), Reader.IsDBNull(5) ? "" : Reader.GetString(5), Reader.IsDBNull(9) ? "" : Reader.GetString(9), Reader.IsDBNull(14) ? "" : Reader.GetString(14), Reader.IsDBNull(6) ? "" : Reader.GetString(6), Reader.IsDBNull(13) ? "" : Reader.GetString(13), "Edit", "Delete" });
                _userList.Add(_user);
            }

            dtGrid.DataSource = t;
            dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns[0].Visible = false;
            dtGrid.Columns[1].Visible = false;
            ThreadPool.QueueUserWorkItem(delegate
            {

                foreach (DataRow row in t.Rows)
                {
                    try
                    {
                        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(row["uri"].ToString());
                        myRequest.Method = "GET";
                        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                        myResponse.Close();
                        Bitmap bps = new Bitmap(bmp, 50, 50);
                        row["Img"] = bps;
                    }
                    catch
                    {

                        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Helper.imageUrl + "default.png");
                        myRequest.Method = "GET";
                        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                        myResponse.Close();
                        Bitmap bps = new Bitmap(bmp, 50, 50);
                        row["Img"] = bps;
                    }
                   
                }

            });

            
            dtGrid.AllowUserToAddRows = false;

            connection.Close();
            dtGrid.Columns[0].Visible = false;
            this.dtGrid.Columns[12].DefaultCellStyle.BackColor = Color.Green;
            this.dtGrid.Columns[13].DefaultCellStyle.BackColor = Color.Red;
        }
        string filterField="Name";
      
        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Do Something with your button.            
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dtGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NewUser frm = new NewUser(null);
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();
        }

        private void UserForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
        List<string> fileIDs = new List<string>();

        private void dtGrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == dtGrid.Columns[2].Index && e.RowIndex >= 0)
            {
                if (fileIDs.Contains(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString()))
                {
                    fileIDs.Remove(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Console.WriteLine("REMOVED this id " + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());

                }
                else
                {
                    fileIDs.Add(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Console.WriteLine("ADDED ITEM " + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
            if (e.ColumnIndex == dtGrid.Columns[12].Index && e.RowIndex >= 0)
            {
                NewUser frm = new NewUser(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();
            }
            try
            {

                if (e.ColumnIndex == dtGrid.Columns[13].Index && e.RowIndex >= 0)
                {
                    if (MessageBox.Show("YES or No?", "Are you sure you want to delete this file? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string Query = "DELETE from file WHERE fileID ='" + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                        Helper.Execute(Query, DBConnect.conn);
                        MessageBox.Show("Information deleted");

                    }
                    Console.WriteLine("DELETE on row {0} clicked", e.RowIndex + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + dtGrid.Rows[e.RowIndex].Cells[2].Value.ToString());


                }
            }
            catch { }

        }

        private void searchCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterField = searchCbx.Text;
        }

        private void DateTxt_TextChanged(object sender, EventArgs e)
        {
            t.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterField, DateTxt.Text);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("YES or No?", "Are you sure you want to delete these users? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                foreach (var item in fileIDs)
                {
                    string Query = "DELETE from user WHERE userID ='" + item + "'";
                    Helper.Execute(Query, DBConnect.conn);
                    //  MessageBox.Show("Information deleted");
                }

            }

        }
    }
}
