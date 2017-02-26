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

        private List<User> _userList = new List<User>();
        private Client _client = new Client();
        private List<Client> _clientList = new List<Client>();
        DataTable t = new DataTable();
        public FileForm()
        {
            InitializeComponent();
            LoadFiles();

        }

        private void LoadFiles()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM file";
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
            t.Columns.Add("Edit");  //0 
            t.Columns.Add("Delete");  //0 


            searchCbx.Items.Add("Name");
            searchCbx.Items.Add("Lawyer");
            searchCbx.Items.Add("Description");
            searchCbx.Items.Add("Status");
            searchCbx.Items.Add("Due");

            while (Reader.Read())
            {

                t.Rows.Add(new object[] { Reader.GetString(0), false, (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)), (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)), (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)), (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)), (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)), (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)), (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)), (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)), (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)), (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)), (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)), (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)), (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)), (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)), (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)), (Reader.IsDBNull(18) ? "none" : Reader.GetString(18)), (Reader.IsDBNull(19) ? "none" : Reader.GetString(19)), (Reader.IsDBNull(20) ? "none" : Reader.GetString(20)), "Edit", "Delete"});

            }

            dtGrid.DataSource = t;
            // dtGrid.RowTemplate.Height = 60;
            dtGrid.Columns[0].Visible = false;
            //dtGrid.Columns[1].Visible = false;           
            //  dtGrid.AllowUserToAddRows = false;
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

        private void dtGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //   MessageBox.Show("end click");
        }

        private void dtGrid_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //  MessageBox.Show("end click");
        }
        List<string> fileIDs = new List<string>();
        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var senderGrid = (DataGridView)sender;

            //if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&   e.RowIndex >= 0)
            //{
            //    //TODO - Button Clicked - Execute Code Here
            //}
            if (e.ColumnIndex == dtGrid.Columns[1].Index && e.RowIndex >= 0)
            {
                if (fileIDs.Contains(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString()))
                {
                    fileIDs.Remove(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Console.WriteLine("REMOVED this id "+dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());

                }
                else
                {
                    fileIDs.Add(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Console.WriteLine("ADDED ITEM "+dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
            if (e.ColumnIndex == dtGrid.Columns[20].Index && e.RowIndex >= 0)
            {
                NewFile frm = new NewFile(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();
            }
            
            try
            {

                if (e.ColumnIndex == dtGrid.Columns[21].Index && e.RowIndex >= 0)
                {
                    if (MessageBox.Show("YES or No?", "Are you sure you want to delete this file? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string Query = "DELETE from file WHERE fileID ='" + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                        Helper.Execute(Query, DBConnect.conn);
                        string Query2 = "INSERT INTO `deletion`( `table`, `eid`,`column`, `created`) VALUES ('file','" + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "','fileID','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "');";
                        Helper.Execute(Query2, DBConnect.conn);
                        MessageBox.Show("Information deleted");

                    }
                    Console.WriteLine("DELETE on row {0} clicked", e.RowIndex + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + dtGrid.Rows[e.RowIndex].Cells[2].Value.ToString());


                }
            }
            catch { }

        }

        private void dtGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("YES or No?", "Are you sure you want to delete these file? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {               
                foreach (var item in fileIDs)
                {
                    string Query = "DELETE from file WHERE fileID ='" + item + "'";
                    Helper.Execute(Query, DBConnect.conn);
                    string Query2 = "INSERT INTO `deletion`( `table`, `eid`,`column`, `created`) VALUES ('file','" + item + "','fileID','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "');";
                    Helper.Execute(Query2, DBConnect.conn);
                   
                }
                MessageBox.Show("Information deleted");

            }

        }
    }
}
