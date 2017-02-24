using Casepro.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
    public partial class NewFile : Form
    {
        DataTable t = new DataTable();
        private User _user = new User();
        private List<User> _userList = new List<User>();

        private Client _client = new Client();
        private List<Client> _clientList = new List<Client>();
        private string id;
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        public NewFile(string fileID)
        {
            id = fileID;
            InitializeComponent();
            LoadUsers();
            LoadClients();
            saveBtn.Visible = true;

            if (id == "")
            {
                saveBtn.Visible = true;
                updateBtn.Visible = false;
            }
            else
            {
                thisFile(id);
                // saveBtn.Visible = false;
                updateBtn.Visible = true;
            }

            //  MessageBox.Show(id);
            printdoc1.PrintPage += new PrintPageEventHandler(printdoc1_PrintPage);
        }
        //Rest of the code
        Bitmap MemoryImage;
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (MemoryImage != null)
            {
                e.Graphics.DrawImage(MemoryImage, 0, 0);
                base.OnPaint(e);
            }
        }
        void printdoc1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage, (pagearea.Width / 2) - (this.panel1.Width / 2), this.panel1.Location.Y);
        }
        public void Print(Panel pnl)
        {
            panel1 = pnl;
            GetPrintArea(pnl);
            previewdlg.Document = printdoc1;
            previewdlg.ShowDialog();
        }
        
        public void thisFile(string id)
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM file WHERE fileID= '" + id + "'";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                try { nameTxt.Text = Reader.GetString(11); }
                catch (InvalidCastException) { }

                try { caseTxt.Text = Reader.GetString(5); }
                catch (InvalidCastException) { }
                subjectTxt.Text = Reader.GetString(8);
                typeCbx.Text = Reader.GetString(7);
                try
                {
                    openedDate.Text = (Reader.IsDBNull(18) ? "none" : Reader.GetString(18));
                }
                catch { }
                lawCbx.Text = (Reader.IsDBNull(10) ? "none" : Reader.GetString(10));
                citationTxt.Text = (Reader.IsDBNull(9) ? "none" : Reader.GetString(9));
                descriptionTxt.Text = (Reader.IsDBNull(6) ? "none" : Reader.GetString(6));
                clientCbx.Text = (Reader.IsDBNull(2) ? "none" : Reader.GetString(2));
                lawyerCbx.Text = (Reader.IsDBNull(4) ? "none" : Reader.GetString(4));
                try
                {
                    noLbl.Text = Reader.GetString(5);
                }
                catch { }
                try
                {
                    contactTxt.Text = Reader.GetString(3);
                }
                catch { }
                try
                {
                    contactpersonTxt.Text = Reader.GetString(20);
                }
                catch { }
                try
                {
                    contactTxt.Text = Reader.GetString(21);
                }
                catch { }
                try
                {
                    dueDate.Text = Convert.ToDateTime(Reader.IsDBNull(19) ? "none" : Reader.GetString(19)).ToString("dd-mm-yyyy");
                }
                catch { }

                loadDis();
            }
            connection.Close();
        }
        private void loadDis()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT *,file.name As file,client.name As client FROM disbursements LEFT JOIN client ON client.clientID = disbursements.clientID LEFT JOIN file ON file.fileID = disbursements.fileID WHERE disbursements.fileID='" + id + "' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            // create and execute query  
            t = new DataTable();

            t.Columns.Add("DATE", typeof(string));
            t.Columns.Add("No.", typeof(string));
            t.Columns.Add("AMOUNT", typeof(string));
            t.Columns.Add("BAL.", typeof(string));
            t.Columns.Add("METHOD", typeof(string));
            t.Columns.Add("DETAILS", typeof(string));
            t.Rows.Add(new object[] { "", " ", "", "FILE SUMMARY", "", "" });
           
            t.Rows.Add(new object[] { " ", " ", "", "", "", "" });
            t.Rows.Add(new object[] { "DISBURSEMENTS", " ", "", "", "", "" });
            t.Rows.Add(new object[] { "Date", "Invoice No.", "Amount", "Balance", "Method", "Details" });          
            while (Reader.Read())
            {
                t.Rows.Add(new object[] { (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)), (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)), (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)), (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)), (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)), (Reader.IsDBNull(35) ? "none" : Reader.GetString(35)) });
            }
            connection.Close();
            MySqlConnection connection2 = new MySqlConnection(DBConnect.conn);
            MySqlCommand command2 = connection2.CreateCommand();
            MySqlDataReader Reader2;
            command2.CommandText = "SELECT *  FROM expenses LEFT JOIN client ON client.clientID = expenses.clientID LEFT JOIN file ON file.fileID = expenses.fileID WHERE expenses.fileID='" + id + "';";
            connection2.Open();
            Reader2 = command2.ExecuteReader();
            t.Rows.Add(new object[] { "", " ", "", "", "","" });
            t.Rows.Add(new object[] { "EXPENSES", " ", "", "", "","" });
            t.Rows.Add(new object[] { "Date", "Invoice No.", "Amount", "Balance", "Method", "Details" });
            while (Reader2.Read())
            {
                t.Rows.Add(new object[] { (Reader2.IsDBNull(11) ? "none" : Reader2.GetString(11)), " ", (Reader2.IsDBNull(7) ? "none" : Reader2.GetString(7)), (Reader2.IsDBNull(8) ? "none" : Reader2.GetString(8)), (Reader2.IsDBNull(36) ? "none" : Reader2.GetString(36)), (Reader2.IsDBNull(6) ? "none" : Reader2.GetString(6)) });
            }
            connection2.Close();

            MySqlConnection connection3 = new MySqlConnection(DBConnect.conn);
            MySqlCommand command3 = connection3.CreateCommand();
            MySqlDataReader Reader3;
            command3.CommandText = "SELECT * FROM events WHERE file ='" + nameTxt.Text + "';";
            connection3.Open();
            Reader3 = command3.ExecuteReader();
            t.Rows.Add(new object[] { "", " ", "", "", "", "" });
            t.Rows.Add(new object[] { "EVENTS", "SCHEDULES ", "", "", "", "" });
            t.Rows.Add(new object[] { "Date", "Event", "Start", "End", "Progress", "Status"});
            while (Reader3.Read())
            {
                t.Rows.Add(new object[] { (Reader3.IsDBNull(10) ? "none" : Reader3.GetString(10)), (Reader3.IsDBNull(1) ? "none" : Reader3.GetString(1)), (Reader3.IsDBNull(2) ? "none" : Reader3.GetString(2)), (Reader3.IsDBNull(3) ? "none" : Reader3.GetString(3)), (Reader3.IsDBNull(16) ? "none" : Reader3.GetString(16)), (Reader3.IsDBNull(8) ? "none" : Reader3.GetString(8)) });
            }
            connection3.Close();
            dtGrid.DataSource = t;
            dtGrid.Rows[1].DefaultCellStyle.BackColor = Color.Beige;
          

        }
        public void LoadUsers()
        {
            _userList.Clear();

            // connect to database  

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT userID, orgID, name, email, password, designation, status, contact, image, address, category, created,sync, charge, supervisor FROM users";
            connection.Open();
            Reader = command.ExecuteReader();
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


                _userList.Add(_user);
            }
            lawyerCbx.DataSource = _userList;
            lawyerCbx.DisplayMember = "name";
            connection.Close();

        }
        public void LoadClients()
        {
            _clientList.Clear();

            // connect to database  

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT clientID, orgID, name, email, contact, status, image, address, created, action, lawyer, registration, password FROM client";
            connection.Open();
            Reader = command.ExecuteReader();

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

                _clientList.Add(_client);
            }
            clientCbx.DataSource = _clientList;
            clientCbx.DisplayMember = "name";
            connection.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string fileID = Guid.NewGuid().ToString();

            string Query = "INSERT INTO file(fileID,orgID, client, contact, lawyer, no, details, type, subject, citation, law, name, created, status, sync,`case`,note, progress, opened, due, contact_person, contact_number) VALUES ('" + fileID + "','" + Helper.orgID + "','" + this.clientCbx.Text + "','" + this.contactTxt.Text + "','" + this.lawyerCbx.Text + "','" + this.noLbl.Text + "','" + this.descriptionTxt.Text + "','" + this.typeCbx.Text + "','" + subjectTxt.Text + "','" + this.citationTxt.Text + "','" + this.lawCbx.Text + "','" + this.nameTxt.Text + "','" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','" + this.stateCbx.Text + "','f','" + this.caseTxt.Text + "','" + this.noteTxt.Text + "','" + this.progressTxt.Text + "','" + Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(this.dueDate.Text).ToString("yyyy-MM-dd") + "','" + this.contactpersonTxt.Text + "','" + this.contactTxt.Text + "');";
            Helper.Execute(Query, DBConnect.conn);
            MessageBox.Show("Information saved");
            FileForm frm = new FileForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();


        }

        private void NewFile_Leave(object sender, EventArgs e)
        {
            // this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileForm frm = new FileForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            string fileID = Guid.NewGuid().ToString();
            string Query = "UPDATE file SET client='" + this.clientCbx.Text + "', sync='f',contact='" + this.contactTxt.Text + "' , lawyer='" + this.lawyerCbx.Text + "', no='" + this.noLbl.Text + "', details='" + this.descriptionTxt.Text + "', type='" + this.typeCbx.Text + "', subject='" + subjectTxt.Text + "', citation='" + this.citationTxt.Text + "', law='" + this.lawCbx.Text + "', name='" + this.nameTxt.Text + "', status='" + this.stateCbx.Text + "',`case`='" + this.caseTxt.Text + "',note='" + this.noteTxt.Text + "', progress='" + this.progressTxt.Text + "', opened='" + Convert.ToDateTime(this.openedDate.Text).ToString("yyyy-MM-dd") + "', due='" + Convert.ToDateTime(this.dueDate.Text).ToString("yyyy-MM-dd") + "', contact_person='" + this.contactpersonTxt.Text + "', contact_number='" + this.contactTxt.Text + "' WHERE fileID ='" + id + "'";
            Helper.Execute(Query, DBConnect.conn);
            MessageBox.Show("Information Updated");
            FileForm frm = new FileForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            string Query = "DELETE from file WHERE fileID ='" + id + "'";
            Helper.Execute(Query, DBConnect.conn);
            MessageBox.Show("Information deleted");
            FileForm frm = new FileForm();
            frm.MdiParent = MainForm.ActiveForm;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            objPPdialog.Document = printDocument1;
            objPPdialog.ShowDialog();
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
        #endregion
       
        private void button4_Click(object sender, EventArgs e)
        {
            Print(this.panel1);
        }
    }
}

