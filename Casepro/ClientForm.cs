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
    public partial class ClientForm : Form
    {
        public Client _client = new Client();
        public static List<Client> _clientList = new List<Client> { };
        public static DataTable table = new DataTable();
        MySqlDataReader Reader;
        public ClientForm()
        {
            InitializeComponent();
            LoadClients();
            //dtGrid.DataSource = _clientList;
        }
        public void LoadClients()
        {
            _clientList.Clear();

            // connect to database  

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM client";
            connection.Open();
            Reader = command.ExecuteReader();
            // create and execute query  

            DataTable t = new DataTable();
            t.Columns.Add("clientID");
            t.Columns.Add("uri");
            t.Columns.Add(new DataColumn("Img", typeof(Bitmap)));
            t.Columns.Add("Name");
            t.Columns.Add("E-mail");
            t.Columns.Add("Contact");
            t.Columns.Add("Lawyer(C/O)");
            t.Columns.Add("Address");          
            t.Columns.Add("Status");
            t.Columns.Add("created");


            Bitmap b = new Bitmap(50, 50);

            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawString("Loading...", this.Font, new SolidBrush(Color.Black), 0f, 0f);
            }
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


                t.Rows.Add(new object[] { Reader.GetString(0), Helper.imageUrl + Reader.GetString(6) as string, b, Reader.IsDBNull(2) ? "": Reader.GetString(2), Reader.IsDBNull(3) ? "" : Reader.GetString(3), Reader.IsDBNull(4) ? "" : Reader.GetString(4), Reader.IsDBNull(10) ? "" : Reader.GetString(10), Reader.IsDBNull(7) ? "" : Reader.GetString(7), Reader.IsDBNull(5) ? "" : Reader.GetString(5) + " ", Reader.IsDBNull(8) ? "" : Reader.GetString(8) + "" });
                _clientList.Add(_client);
            }

            dtGrid.DataSource = t;
            dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns[0].Visible = false;
            dtGrid.Columns[1].Visible = false;
            dtGrid.Columns[0].Visible = false;

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
                    catch {

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
          
        }
        public string SafeGetString(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dtGrid.Rows[rowIndex];
                // MessageBox.Show(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());
                NewClient frm = new NewClient(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();
            
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
            NewClient frm = new NewClient(null);
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();
        }

        private void ClientForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtGrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            NewClient frm = new NewClient(null);
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();
        }

        private void dtGrid_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClientForm_Leave_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
