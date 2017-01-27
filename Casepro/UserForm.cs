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
        public UserForm()
        {
            InitializeComponent();
            LoadUsers();
            //dtGrid.DataSource = _userList;
        }
        public  void LoadUsers()
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

            DataTable t = new DataTable();
            t.Columns.Add("userID");
            t.Columns.Add("uri");
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));           
            t.Columns.Add("Name");
            t.Columns.Add("E-mail");
            t.Columns.Add("Contact");
            t.Columns.Add("Designation");
            t.Columns.Add("Address");
            t.Columns.Add("Supervisor");
            t.Columns.Add("Status");
            t.Columns.Add("Charge");            


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
                

                t.Rows.Add(new object[] { Reader.GetString(0),  Helper.imageUrl + Reader.GetString(8), b, Reader.GetString(2), Reader.GetString(3), Reader.GetString(7), Reader.GetString(5), Reader.GetString(9), Reader.GetString(14)+"", Reader.GetString(6), ""+ Reader.GetString(13) +""});
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
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(row["uri"].ToString());
                    myRequest.Method = "GET";
                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                    myResponse.Close();
                    Bitmap bps = new Bitmap(bmp, 50, 50);
                  
                    row["Img"] = bps;
                }
            });

            dtGrid.CellEndEdit += dataGridView1_CellEndEdit;
            dtGrid.AllowUserToAddRows = false;

            connection.Close();
            dtGrid.CellClick += dtGrid_CellClick;
        }
        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
                //Do Something with your button.            
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string value = dtGrid.Rows[e.RowIndex].Cells["uri"].Value.ToString();
            ThreadPool.QueueUserWorkItem(delegate
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(value);
                myRequest.Method = "GET";
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                myResponse.Close();
                dtGrid.Rows[e.RowIndex].Cells["Img"].Value = bmp;
            });
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

        private void dtGrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dtGrid.Rows[rowIndex];
               // MessageBox.Show(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());
                NewUser frm = new NewUser(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();

              
            

        }
    }
}
