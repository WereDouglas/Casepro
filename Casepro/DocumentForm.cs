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
    public partial class DocumentForm : Form
    {
        private MySqlDataAdapter da;        // Data Adapter
        private DataSet ds;
        private string sTable = "files";
        public DocumentForm()
        {
            InitializeComponent();
            LoadDocs();
        }
        private void LoadDocs()
        {


            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DBConnect.conn);

                conn.Open();
                da = new MySqlDataAdapter("SELECT * FROM document;", conn);
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

        private void DocumentForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NewDoc frm = new NewDoc(null);
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();
        }

        private void DocumentForm_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM client";
            connection.Open();
            Reader = command.ExecuteReader();
            TreeNode treeNode = new TreeNode("Clients");
          //  myTreeView.Nodes.Add(treeNode);
            // create and execute query  
            while (Reader.Read())
            {
               
               treeNode = new TreeNode(Reader.GetString(2));
            //    myTreeView.Nodes.Add(treeNode);

            }
           
            //TreeNode node2 = new TreeNode("C#");
            //TreeNode node3 = new TreeNode("VB.NET");
            //TreeNode[] array = new TreeNode[] { node2, node3 };
            //
            // Final node.
            //
            //treeNode = new TreeNode("Dot Net Perls", array);
           // myTreeView.Nodes.Add(treeNode);

        }

        private void myTreeView_DoubleClick(object sender, EventArgs e)
        {
            //
            // Get the selected node.
          //  //
          //  TreeNode node = myTreeView.SelectedNode;
            //
            // Render message box.
            //
            ///MessageBox.Show(string.Format("You selected: {0}", node.Text));
        }
    }
}
