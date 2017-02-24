using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
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
            try
            {
                Shares();
            }
            catch {


                MessageBox.Show("Cannot acess server files OR you do not have the right permissions to access these files");
            }
        }
        private void Shares()
        {
            myTreeView.Nodes.Clear();
            TreeNode treeNode = new TreeNode("Documents");
       
            using (ManagementClass shares = new ManagementClass("\\\\" + Helper.serverIP + "\\root\\cimv2", "Win32_Share", new ObjectGetOptions()))
            {
                foreach (ManagementObject share in shares.GetInstances())
                {
                    Console.WriteLine(share.Properties["Name"].Value + " " + share.Properties["Path"].Value);
                    treeNode = new TreeNode(share.Properties["Name"].Value.ToString());
                    myTreeView.Nodes.Add(treeNode);
                    string path = "\\\\" + Helper.serverName + "\\" + share.Properties["Name"].Value;
                    //var folders = System.IO.Directory.GetDirectories(path);
                    try
                    {
                        // var folders = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

                        DirectoryInfo objDir = new DirectoryInfo(path);

                        //Check whether path exists
                        if (objDir.Exists)
                        {
                            //find all directories
                            DirectoryInfo[] objSubDirs = objDir.GetDirectories();

                            //recurse each directory
                            for (int i = 0; i < objSubDirs.Length; i++)
                            {
                                //CheckPath(objSubDirs[i].FullName);
                                Console.WriteLine(objSubDirs[i].FullName);
                                //FileInfo objFile = new FileInfo(path);
                                //Directory.GetFiles(@"" + objSubDirs[i].FullName);
                                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"" + objSubDirs[i].FullName);
                                //foreach (System.IO.FileInfo f in Directory.GetFiles(@"" + objSubDirs[i].FullName))
                                foreach (System.IO.FileInfo f in dir.GetFiles("*.*"))
                                {
                                    Console.WriteLine(f.Name);
                                    TreeNode child = new TreeNode();
                                    child.Name =f.Name;                                  
                                    child.Tag =f.Name;
                                    child.Text = objSubDirs[i].FullName+"\\"+f.Name;
                                    treeNode.Nodes.Add(child);
                                }

                            }

                        }
                    }
                    catch
                    {


                    }
                    //try
                    //{
                    //    var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    //}
                    //catch 
                    //{    }


                }
            }

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

        private void myTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MessageBox.Show((myTreeView.SelectedNode.Text));
            string path =  myTreeView.SelectedNode.Text;
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                // read from file or write to file
            }
        }

        private void myTreeView_Click(object sender, EventArgs e)
        {
          
            //TreeViewHitTestInfo info = myTreeView.HitTest(myTreeView.PointToClient(Cursor.Position));
            //if (info != null)
            //    MessageBox.Show(info.Node.Text);
        }
    }
}
