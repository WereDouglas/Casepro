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
        public EventForm()
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
                da = new MySqlDataAdapter("SELECT * FROM events;", conn);
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
               // this.dtGrid.Columns[1].Visible = false;

            }
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
    }
}
