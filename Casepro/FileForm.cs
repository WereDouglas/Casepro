using Casepro.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
    public partial class FileForm : Form
    {
      
        private User _user = new User();
        DataTable t = new DataTable();
        private List<User> _userList = new List<User>();
        private Client _client = new Client();
        private List<Client> _clientList = new List<Client>();

        public FileForm()
        {
            InitializeComponent();
            LoadFiles();
           
        }
    
        private void LoadFiles()
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
            t.Columns.Add("fileID");  //0         
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("No");//05
            t.Columns.Add("Name");//11
            t.Columns.Add("Client");//2
            t.Columns.Add("Lawyer");//4
            t.Columns.Add("Contact");//3
            t.Columns.Add("Description");//6
            t.Columns.Add("Type");//7
            t.Columns.Add("Subject");//8
            t.Columns.Add("Citation");//9 
            t.Columns.Add("Law");//10
            t.Columns.Add("Created");//12 
            t.Columns.Add("Status");//13 
            t.Columns.Add("Case");//15 
            t.Columns.Add("Note");//16
            t.Columns.Add("Progress");//17 
            t.Columns.Add("Opened");//18
            t.Columns.Add("Due");//19
            t.Columns.Add("C/O");//20 
           

            searchCbx.Items.Add("Name");
            searchCbx.Items.Add("Lawyer");
            searchCbx.Items.Add("Description");
            searchCbx.Items.Add("Status");
            searchCbx.Items.Add("Due");

            while (Reader.Read())
            {

                t.Rows.Add(new object[] { Reader.GetString(0), false, (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)), (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)), (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)), (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)), (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)), (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)), (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)), (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)), (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)), (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)), (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)), (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)), (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)), (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)), (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)), (Reader.IsDBNull(18) ? "none" : Reader.GetString(18)), (Reader.IsDBNull(19) ? "none" : Reader.GetString(19)), (Reader.IsDBNull(20) ? "none" : Reader.GetString(20)), (Reader.IsDBNull(20) ? "none" : Reader.GetString(20)) });
              
            }

            dtGrid.DataSource = t;
            dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns[0].Visible = false;
            dtGrid.Columns[1].Visible = false;           
            dtGrid.AllowUserToAddRows = false;
            this.dtGrid.Columns[3].DefaultCellStyle.BackColor = Color.Green;
            this.dtGrid.Columns[4].DefaultCellStyle.BackColor = Color.Red;

            connection.Close();
            dtGrid.Columns[0].Visible = false;
            //dtGrid.CellClick += dtGrid_CellClick;

        }
        string filterField = "Name";
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void FileForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }



        private void dtGrid_CurrentCellChanged(object sender, EventArgs e)
        {

            if (dtGrid.SelectedRows.Count != 0)
            {
                MessageBox.Show(dtGrid.CurrentCell.RowIndex.ToString());

            }

        }

        private void dtGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dtGrid.Rows[rowIndex];
            //  textBox5.Text = dtGrid.Rows[1].Cells[1].Value.ToString();// row.Cells[1].Value;
            MessageBox.Show(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());

        }

        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dtGrid.Rows[rowIndex];
                // MessageBox.Show(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());
                NewFile frm = new NewFile(dtGrid.Rows[rowIndex].Cells[0].Value.ToString());
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();

                //  textBox5.Text = dtGrid.Rows[1].Cells[1].Value.ToString();// row.Cells[1].Value;
                // MessageBox.Show();
            }
            catch {
            }
        }

      

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NewFile frm = new NewFile(null);
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();

        }

        private void dtGrid_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void DateTxt_TextChanged(object sender, EventArgs e)
        {
            t.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterField, DateTxt.Text);
        }

        private void searchCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterField = searchCbx.Text;
        }
    }
}
