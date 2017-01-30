using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
    public partial class EventForm : Form
    {
        private MySqlDataAdapter da;        // Data Adapter
        private DataSet ds;
        private string sTable = "files";
        DataTable dt = new DataTable();
        DataTable t = new DataTable();
        public EventForm()
        {
            InitializeComponent();
            LoadFiles();
        }
        private void LoadFiles()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM events;";
            connection.Open();
            Reader = command.ExecuteReader();
            // create and execute query  
            //t = new DataTable();
            t.Columns.Add("id", typeof(string));
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("Date", typeof(string));
            t.Columns.Add("Name", typeof(string));
            t.Columns.Add("Start", typeof(string));
            t.Columns.Add("End", typeof(string));
            t.Columns.Add("User", typeof(string));
            t.Columns.Add("Client");
            t.Columns.Add("File");
            t.Columns.Add("Progress");
            searchCbx.Items.Add("Date");
            searchCbx.Items.Add("Client");
            searchCbx.Items.Add("File");
            searchCbx.Items.Add("Progress");


            while (Reader.Read())
            {
                t.Rows.Add(new object[] { Reader.GetString(0),false,(Reader.IsDBNull(10) ? "none" : Reader.GetString(10)), (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)), (Reader.IsDBNull(2) ? "none" : Convert.ToDateTime( Reader.GetString(2)).ToString("HH:MM")), (Reader.IsDBNull(3) ? "none" : Convert.ToDateTime(Reader.GetString(3)).ToString("HH:MM")), (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)), (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)), (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)), (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) });

                //t.Rows.Add(new object[] {Reader.GetString(0),Reader.GetString(1),Reader.GetString(2),Reader.GetString(3),Reader.GetString(4)});

            }
            dtGrid.DataSource = t;


            this.dtGrid.Columns[0].Visible = false;
            this.dtGrid.Columns[3].DefaultCellStyle.BackColor = Color.Green;
            this.dtGrid.Columns[4].DefaultCellStyle.BackColor = Color.Red;
            // this.dtGrid.Columns[1].Visible = false;


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EventForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NewEvent frm = new NewEvent(null);
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();
        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowIndex = e.RowIndex;
            DataGridViewRow row = dtGrid.Rows[rowIndex];
            // MessageBox.Show(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());
            NewEvent frm = new NewEvent(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();

            //  textBox5.Text = dtGrid.Rows[1].Cells[1].Value.ToString();// row.Cells[1].Value;
            // MessageBox.Show();

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
        string filterField = "Date";
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            t.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterField, DateTxt.Text);
          
        }

        private void searchCbx_Click(object sender, EventArgs e)
        {

        }

        private void searchCbx_DropDownClosed(object sender, EventArgs e)
        {
           
        }

        private void searchCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterField = searchCbx.Text;
        }
    }
}
