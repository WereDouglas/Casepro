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
        private MySqlDataAdapter da;        // Data Adapter
        private DataSet ds;
        private string sTable = "files";
        private User _user = new User();
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


            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DBConnect.conn);

                conn.Open();
                da = new MySqlDataAdapter("SELECT * FROM file;", conn);
                ds = new DataSet();
                da.Fill(ds, sTable);
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
            finally
            {
                
                dtGrid.Refresh();
                dtGrid.DataSource = ds;
                dtGrid.DataMember = sTable;
                this.dtGrid.Columns[0].Visible = false;
                this.dtGrid.Columns[1].Visible = false;

            }
        }
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
    }
}
