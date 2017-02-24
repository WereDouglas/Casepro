using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
    public partial class DisForm : Form
    {
        DataTable t = new DataTable();
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        string month;
        double totalExpense = 0;
        Dictionary<string, string> ExpenseDictionary = new Dictionary<string, string>();
        public DisForm()
        {
            InitializeComponent();
            month = DateTime.Now.ToString("yyyy-MM");
            LoadData();

            searchCbx.Items.Add("Date");
            searchCbx.Items.Add("Client");
            searchCbx.Items.Add("File");
            searchCbx.Items.Add("Method");
        }
        private void LoadData()
        {
            ExpenseDictionary.Clear();
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT *,file.name As file,client.name As client FROM disbursements LEFT JOIN client ON client.clientID = disbursements.clientID LEFT JOIN file ON file.fileID = disbursements.fileID WHERE disbursements.date LIKE '%" + month + "%';";
            connection.Open();
            Reader = command.ExecuteReader();
            // create and execute query  
            t = new DataTable();
            t.Columns.Add("id", typeof(string));
            t.Columns.Add(new DataColumn("Select", typeof(bool)));
            t.Columns.Add("Date", typeof(string));//14
            t.Columns.Add("Invoice No.", typeof(string));//7
            t.Columns.Add("Client");//52
            t.Columns.Add("File");//40
            t.Columns.Add("Amount", typeof(string));//9
            t.Columns.Add("Balance", typeof(string));//11
            t.Columns.Add("Method", typeof(string));//8
            t.Columns.Add("C/O", typeof(string));//10
            t.Columns.Add("Details", typeof(string));//35
            t.Columns.Add("Approved");//12
            t.Columns.Add("Paid");//13
            t.Columns.Add("Received by");//25
            t.Columns.Add("View");  //0 
            t.Columns.Add("Delete");  //0 



            while (Reader.Read())
            {
                for (int h = 0; h <= 55; h++)
                {
                 //   System.Diagnostics.Debug.WriteLine(h + "-" + (Reader.IsDBNull(h) ? "" : Reader.GetString(h)));
                }
                t.Rows.Add(new object[] { Reader.GetString(0), false, (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)), (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)), (Reader.IsDBNull(52) ? "none" : Reader.GetString(52)), (Reader.IsDBNull(40) ? "none" : Reader.GetString(40)),Convert.ToDouble (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)).ToString("n0"),Convert.ToDouble (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)).ToString("n0"), (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)), (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)), (Reader.IsDBNull(35) ? "none" : Reader.GetString(35)), (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)), (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)), (Reader.IsDBNull(25) ? "none" : Reader.GetString(25)), "View", "Delete" });

                //t.Rows.Add(new object[] {Reader.GetString(0),Reader.GetString(1),Reader.GetString(2),Reader.GetString(3),Reader.GetString(4)});
                ExpenseDictionary.Add((Reader.IsDBNull(0) ? "none" : Reader.GetString(0)), (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)));

            }
            try
            {
                totalExpense = ExpenseDictionary.Sum(m => Convert.ToDouble(m.Value));
                t.Rows.Add(new object[] { " ", false, "", "", "", "", " ", "", "", "", "", "", "", "", "", "" });
                t.Rows.Add(new object[] { " ", false, "", "TOTAL DISBURSEMENTS :", "", (totalExpense).ToString("n0"), "", "", "", "", "", "", "", "", "", "" });
            }
            catch
            {
            }
            dtGrid.DataSource = t;
            dtGrid.Columns[0].Visible = false;
            dtGrid.Columns[14].DefaultCellStyle.BackColor = Color.Green;
            dtGrid.Columns[15].DefaultCellStyle.BackColor = Color.Red;
            // this.dtGrid.Columns[1].Visible = false;


        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NewDis frm = new NewDis(null);
            frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            this.Close();

        }
        string filterField = "Date";
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            t.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterField, DateTxt.Text);

        }

        #region Begin Print Event Handler
        /// <summary>
        /// Handles the begin print event of print document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dtGrid.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Print Page Event
        /// <summary>
        /// Handles the print page event of print document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dtGrid.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dtGrid.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dtGrid.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Customer Summary", new Font(dtGrid.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new Font(dtGrid.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(dtGrid.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dtGrid.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new Font(new Font(dtGrid.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dtGrid.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        List<string> fileIDs = new List<string>();
        private void dtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == dtGrid.Columns[1].Index && e.RowIndex >= 0)
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
            if (e.ColumnIndex == dtGrid.Columns[14].Index && e.RowIndex >= 0)
            {
                NewDis frm = new NewDis(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.MdiParent = MainForm.ActiveForm;
                frm.Show();
                this.Close();
            }
            try
            {

                if (e.ColumnIndex == dtGrid.Columns[15].Index && e.RowIndex >= 0)
                {
                    if (MessageBox.Show("YES or NO?", "Are you sure you want to delete this file? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string Query = "DELETE from disbursements WHERE disbursementID ='" + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                        Helper.Execute(Query, DBConnect.conn);
                        MessageBox.Show("Information deleted");

                    }
                    Console.WriteLine("DELETE on row {0} clicked", e.RowIndex + dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + dtGrid.Rows[e.RowIndex].Cells[2].Value.ToString());


                }
            }
            catch { }

        }
        #endregion

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            objPPdialog.Document = printDocument1;
            objPPdialog.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("YES or NO?", "Are you sure you want to delete these disbursements? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                foreach (var item in fileIDs)
                {
                    string Query = "DELETE from disbursements WHERE feeID ='" + item + "'";
                    Helper.Execute(Query, DBConnect.conn);
                    //  MessageBox.Show("Information deleted");
                }

            }

        }

        private void monthPicker_CloseUp(object sender, EventArgs e)
        {
            month = Convert.ToDateTime(monthPicker.Text).ToString("yyyy-MM");
            LoadData();
        }
    }
}

