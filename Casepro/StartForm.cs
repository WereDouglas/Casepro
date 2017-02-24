using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsCalendar;

namespace Casepro
{
    public partial class StartForm : Form
    {
        #region Fields

        private List<CalendarItem> _items = new List<CalendarItem>();
        private BackgroundWorker bw = new BackgroundWorker();
        private BackgroundWorker bwDownload = new BackgroundWorker();
        private BackgroundWorker bwMessage = new BackgroundWorker();
        private BackgroundWorker bwEmail = new BackgroundWorker();
        List<string> files = new List<string>();
        List<string> clients = new List<string>();
        List<string> documents = new List<string>();
        List<string> events = new List<string>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public StartForm()
        {
            InitializeComponent();

            // ContextMenu.Show();
            userprofile();
            //StartRunMail();

            if (!bwEmail.IsBusy)
            {
                bwEmail.RunWorkerAsync();
                bwEmail.WorkerReportsProgress = true;
                //  bw.WorkerSupportsCancellation = true;
                bwEmail.DoWork += new DoWorkEventHandler(bw_Email);
            }



            if (!bwDownload.IsBusy)
            {
                bwDownload.RunWorkerAsync();
                bwDownload.WorkerReportsProgress = true;
                //  bw.WorkerSupportsCancellation = true;
                bwDownload.DoWork += new DoWorkEventHandler(bw_Download);
            }

            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
                bw.WorkerReportsProgress = true;
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            }

            if (!bwMessage.IsBusy)
            {
                bwMessage.RunWorkerAsync();
                bwMessage.WorkerReportsProgress = true;
                bwMessage.DoWork += new DoWorkEventHandler(bw_Message);
            }


            // CalendarItem item = new CalendarItem(this.calendar1, DateTime.Now, DateTime.Now, "TEST");

            //_items.Add(item);
            LoadingCalendar();

        }
        #region Calendar Methods

        /// <summary>
        /// Handles the LoadItems event of the calendar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WindowsFormsCalendar.CalendarLoadEventArgs"/> instance containing the event data.</param>
        private void calendar1_LoadItems(object sender, CalendarLoadEventArgs e)
        {
            
            PlaceItems();
        }
        private void PlaceItems()
        {
            foreach (CalendarItem item in _items)
            {
                if (calendar1.ViewIntersects(item))
                {
                    calendar1.Items.Add(item);
                }
            }
        }
        private void LoadingCalendar() {
            List<ItemInfo> lst = new List<ItemInfo>();
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM events";
            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("Server is offline");

            }
            Reader = command.ExecuteReader();
            string state = "";
            while (Reader.Read())
            {
                //CalendarItem cal = new CalendarItem(calendar1,Convert.ToDateTime(Reader.GetString(5)+"T"+Reader.GetString(3)+":00"), Convert.ToDateTime(Reader.GetString(5) + "T" + Reader.GetString(4) + ":00"), Reader.GetString(2));
                System.Diagnostics.Debug.WriteLine(Reader.GetString(13));
                //Reader.IsDBNull(2) ? "": Reader.GetString(2)
                CalendarItem cal = new CalendarItem(calendar1, Convert.ToDateTime(Reader.GetString(2)), Convert.ToDateTime(Reader.GetString(3)), Reader.GetString(1) + " C/O:" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + " File:" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)));

                if (Reader.IsDBNull(13))
                {
                    state = "none";
                }
                else
                {
                    state = Reader.GetString(13);
                }
                if (state == "Medium") { cal.ApplyColor(Color.LightGreen); }
                if (state == "Low") { cal.ApplyColor(Color.CornflowerBlue); }
                if (state == "High") { cal.ApplyColor(Color.IndianRed); }
                if (state == "none") { cal.ApplyColor(Color.LightSeaGreen); }
                _items.Add(cal);
                // t.Rows.Add(new object[] { Reader.GetString(0), Helper.imageUrl + Reader.GetString(8), b, Reader.GetString(2), Reader.GetString(3), Reader.GetString(7), Reader.GetString(5), Reader.GetString(9), Reader.GetString(14) + "", Reader.GetString(6), "" + Reader.GetString(13) + "" });

            }
            eventLbl1.Text = "Events and tasks: " + _items.Count;
            PlaceItems();
            Clients();
        }

        private void Clients()
        {
            List<ItemInfo> lst = new List<ItemInfo>();
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT name FROM client";
            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("Server is offline");

            }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                clients.Add(Reader.GetString(0));
            }
            clientLbl1.Text = "Clients : " + clients.Count;
            Files();
        }
        private void Files()
        {
            List<ItemInfo> lst = new List<ItemInfo>();
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT name FROM file";
            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("Server is offline");

            }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                files.Add(Reader.GetString(0));
            }
            fileLbl1.Text = "Files : " + files.Count;
            Docs();

        }
        private void Docs()
        {
            List<ItemInfo> lst = new List<ItemInfo>();
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT name FROM document";
            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("Server is offline");

            }
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                documents.Add(Reader.GetString(0));
            }
            documentLbl1.Text = "Documents : " + documents.Count;

        }

        #endregion

        #region Month View Methods

        /// <summary>
        /// Handles the SelectionChanged event of the monthView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void monthView1_SelectionChanged(object sender, EventArgs e)
        {
            this.calendar1.SetViewRange(this.monthView1.SelectionStart.Date, this.monthView1.SelectionEnd.Date);
        }

        #endregion

        private void StartForm_Load(object sender, EventArgs e)
        {

        }

        private void Downloading()
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + " Downloading file information" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                DownloadFiles();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + " Downloading file information failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {


                }

            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + " Downloading event information" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                DownloadEvents();
            }
            catch
            {
            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + " Downloading expense information" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                DownloadExpense();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + " Downloading expense information failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });

                }
                catch
                {


                }
            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + " Downloading fees information" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                DownloadFees();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + " Downloading fees information failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });

                }
                catch
                {


                }
            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + " Downloading disbursements information" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                DownloadDisbbursements();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + " Downloading disbursement information failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch { }

            }
        }
        private void DownloadEvents()
        {
            System.Diagnostics.Debug.WriteLine("Downloading from :" + Helper.orgID);
            MySqlConnection connection = new MySqlConnection(DBConnect.remoteConn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM events WHERE sync ='f' AND orgID = '" + Helper.orgID + "' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string Query2 = "DELETE from events WHERE id ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.conn);
                string Query = "INSERT INTO `events`(`id`, `name`, `start`, `end`, `user`, `file`, `created`, `action`, `status`, `orgID`, `date`, `hours`, `court`, `notify`,`priority`, `sync`,`progress`,`client`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','t','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','" + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)) + "');";
                Helper.Execute(Query, DBConnect.conn);
                string Query3 = "UPDATE `events` SET `sync`='t' WHERE id ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.remoteConn);

            }
            Reader.Close();
            connection.Close();

        }
        private void SyncFiles()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM file WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from file WHERE fileID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.remoteConn);

                string Query = "INSERT INTO `file`(`fileID`, `orgID`, `client`, `contact`, `lawyer`, `no`, `details`, `type`, `subject`, `citation`, `law`, `name`, `created`, `status`, `sync`, `case`, `note`, `progress`, `opened`, `due`, `contact_person`, `contact_number`,`action`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','t','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','" + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)) + "','" + (Reader.IsDBNull(18) ? "none" : Reader.GetString(18)) + "','" + (Reader.IsDBNull(19) ? "none" : Reader.GetString(19)) + "','" + (Reader.IsDBNull(20) ? "none" : Reader.GetString(20)) + "','" + (Reader.IsDBNull(21) ? "none" : Reader.GetString(21)) + "','none');";
                Helper.Execute(Query, DBConnect.remoteConn);

                string Query3 = "UPDATE `file` SET `sync`='t' WHERE fileID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);

            }
            connection.Close();


        }
        private void DownloadFiles()
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.remoteConn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM file WHERE sync ='f' ;";
            try
            {
                connection.Open();
            }
            catch { }
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from file WHERE fileID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.conn);

                string Query = "INSERT INTO `file`(`fileID`, `orgID`, `client`, `contact`, `lawyer`, `no`, `details`, `type`, `subject`, `citation`, `law`, `name`, `created`, `status`, `sync`, `case`, `note`, `progress`, `opened`, `due`, `contact_person`, `contact_number`,`action`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','t','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','" + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)) + "','" + (Reader.IsDBNull(18) ? "none" : Reader.GetString(18)) + "','" + (Reader.IsDBNull(19) ? "none" : Reader.GetString(19)) + "','" + (Reader.IsDBNull(20) ? "none" : Reader.GetString(20)) + "','" + (Reader.IsDBNull(21) ? "none" : Reader.GetString(21)) + "','none');";
                Helper.Execute(Query, DBConnect.conn);

                string Query3 = "UPDATE `file` SET `sync`='t' WHERE fileID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.remoteConn);

            }
            connection.Close();
        }
        private void Uploading()
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + " Organisation information uploaded information" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                SyncOrg();
            }
            catch
            {


                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + " Upload information failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {


                }

            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "Event uploaded" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                SyncEvents();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "Event upload failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {


                }

            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "User uploaded" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                SyncUsers();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "User upload failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {

                }
            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "Clients uploaded" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                SyncClients();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "Client upload failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {

                }
            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "Files uploaded" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                SyncFiles();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "File upload failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {

                }

            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "Expense uploaded" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                SyncExpense();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "Expense upload failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {

                }

            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "Fees uploaded" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                SyncFees();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "Fees upload failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {

                }

            }
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "Disbursements uploaded" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                SyncDisbbursements();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "Disbursements upload failed" + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {

                }

            }
        }
        private void SyncEvents()
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM events WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from events WHERE id ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.remoteConn);
                string Query = "INSERT INTO `events`(`id`, `name`, `start`, `end`, `user`, `file`, `created`, `action`, `status`, `orgID`, `date`, `hours`, `court`, `notify`,`priority`, `sync`,`progress`,`client`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','" + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)) + "');";
                Helper.Execute(Query, DBConnect.remoteConn);
                string Query3 = "UPDATE `events` SET `sync`='t' WHERE id ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);
            }
            Reader.Close();
            connection.Close();
        }
        private void Messaging()
        {

            if (!Messenger.state)
            {

                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "messaging not connected trying again =" + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                Messenger.connect();

            }
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM events WHERE notify ='true' and date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";

            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string user = (Reader.IsDBNull(4) ? "none" : Reader.GetString(4));
                string eventID = (Reader.IsDBNull(0) ? "none" : Reader.GetString(0));
                string message = "SCHEDULE " + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + " FILE" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + " CLIENT: " + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17));

                MySqlConnection connection2 = new MySqlConnection(DBConnect.conn);

                MySqlCommand command2 = connection2.CreateCommand();
                MySqlDataReader Reader2;
                connection2.Open();
                command2.CommandText = "SELECT contact FROM users WHERE name = '" + user + "'";
                Reader2 = command2.ExecuteReader();
                while (Reader2.Read())
                {
                    string contact = (Reader2.IsDBNull(0) ? "none" : Reader2.GetString(0));
                    if (contact != "")
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            uploadTxt.Text = uploadTxt.Text + "Sending message to " + user + "  " + contact + "\r\n";
                            uploadTxt.ForeColor = Color.RoyalBlue;
                            uploadTxt.ScrollToCaret();
                        });
                        System.Diagnostics.Debug.WriteLine(user + "  " + contact);
                        string info = Messenger.SendUpdate(message, contact.Trim());
                        if (info == "sent")
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                uploadTxt.Text = uploadTxt.Text + "Message sent: " + user + "  " + contact + "\r\n";
                                uploadTxt.ForeColor = Color.RoyalBlue;
                                uploadTxt.ScrollToCaret();
                            });
                            System.Diagnostics.Debug.WriteLine("Message sent");
                            string Query3 = "UPDATE `events` SET `notify`='false' WHERE id ='" + eventID + "'";
                            Helper.Execute(Query3, DBConnect.conn);
                        }
                    }
                }
                connection2.Close();
            }
            connection.Close();
        }
        private void bw_Message(object sender, DoWorkEventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "Starting messaging...." + "\r\n";
                    uploadTxt.ForeColor = Color.RoyalBlue;
                    uploadTxt.ScrollToCaret();
                });
                Messaging();
            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "Messaging failed...." + "\r\n";
                        uploadTxt.ForeColor = Color.Red;
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch { }

            }
            System.Threading.Thread.Sleep(5000);

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            uploadTxt.SelectionStart = uploadTxt.Text.Length;
            uploadTxt.ScrollToCaret();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Uploading();
            System.Threading.Thread.Sleep(1000);

        }
        private void bw_Email(object sender, DoWorkEventArgs e)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Helper.msgUrl);
                request.GetResponse();
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + " Message  sent" + "\r\n";
                    uploadTxt.ScrollToCaret();
                });

            }
            catch
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + " Message not sent" + "\r\n";
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch
                {

                }

            }


            if (Helper.IsInternetAvailable())
            {

                MySqlConnection connection = new MySqlConnection(DBConnect.remoteConn);
                Console.WriteLine("Attempting to open connection with {0} second timeout, starting at {1}.", connection.ConnectionTimeout, DateTime.Now.ToLongTimeString());

                MySqlCommand command = connection.CreateCommand();
                // MySqlDataReader Reader;
                try
                {
                    connection.Open();
                    Helper.serverOnline = true;
                    // Helper.serverOnline = false;
                    Invoke((MethodInvoker)delegate
                    {
                        uploadTxt.Text = uploadTxt.Text + "Server is online " + "\r\n";
                        uploadTxt.ScrollToCaret();
                    });
                }
                catch (Exception c)
                {
                    Helper.serverOnline = false;
                    try
                    {

                        Invoke((MethodInvoker)delegate
                        {
                            uploadTxt.Text = uploadTxt.Text + "Server offline " + "\r\n";
                            uploadTxt.ScrollToCaret();
                        });
                    }
                    catch { }
                }


            }
            else
            {
                Helper.serverOnline = false;
                // uploadTxt.Text = "Server offline /n ";
                Invoke((MethodInvoker)delegate
                {
                    uploadTxt.Text = uploadTxt.Text + "Server offline " + "\r\n";
                    uploadTxt.ScrollToCaret();
                });
            }
            System.Threading.Thread.Sleep(500);

        }
        private void bw_Download(object sender, DoWorkEventArgs e)
        {


            Downloading();
            System.Threading.Thread.Sleep(500);

        }

        void userprofile()
        {
            try
            {
                var request = WebRequest.Create(Helper.imageUrl + Helper.image);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    pictureBox5.Image = Bitmap.FromStream(stream);
                    // pictureBox5.BackgroundImageLayout();
                }
            }

            catch { }
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            orgLbl1.Text = Helper.orgID;
            nameLbl1.Text = Helper.username;
            contactLbl1.Text = Helper.contact;

        }
        private void SyncUsers()
        {

            string paths = @"c:\Case\images";
            if (!Directory.Exists(paths))
            {
                DirectoryInfo dim = Directory.CreateDirectory(paths);
                Console.WriteLine("The directory was created successfully at {0}.",
                Directory.GetCreationTime(paths));
            }
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM users WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;

                try
                {
                    string filepath = (Helper.imageUrl + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)));
                    string image = @"c:\Case\\images\" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8));

                    string remotePath = (Helper.RemoteUploadUrl).Replace("\\", "/");
                    WebClient theClient = new WebClient();
                    theClient.DownloadFile(filepath, image);
                    //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
                    theClient.Dispose();


                    string images = @"c:\Case\\images\" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8));
                    if (System.IO.File.Exists(images))
                    {
                        try
                        {
                            string filepaths = images;
                            // string localPaths = new Uri(filepaths).LocalPath;
                            string remotePaths = Helper.RemoteUploadUrl;

                            WebClient theClients = new WebClient();
                            byte[] responseArray = theClients.UploadFile(remotePaths, filepaths);
                            Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
                            theClients.Dispose();
                        }
                        catch (Exception c)
                        {

                        }
                    }

                    // MessageBox.Show("done");

                }
                catch (Exception c)
                {
                    //  MessageBox.Show(c.ToString());
                    Console.WriteLine(c.ToString());

                }
                // return;
                string Query2 = "DELETE from users WHERE userID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.remoteConn);

                string Query = "INSERT INTO `users`(`userID`, `orgID`, `name`, `email`, `password`, `designation`, `status`, `contact`, `image`, `address`, `category`, `created`, `sync`, `charge`, `supervisor`, `action`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','t','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','none');";
                Helper.Execute(Query, DBConnect.remoteConn);

                string Query3 = "UPDATE `users` SET `sync`='t' WHERE userID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);

            }
            connection.Close();
        }
        private void SyncClients()
        {

            string paths = @"c:\Case\images";
            if (!Directory.Exists(paths))
            {
                DirectoryInfo dim = Directory.CreateDirectory(paths);
                Console.WriteLine("The directory was created successfully at {0}.",
                Directory.GetCreationTime(paths));
            }
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM client WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;

                try
                {
                    string filepath = (Helper.imageUrl + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)));
                    string image = @"c:\Case\\images\" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6));

                    string remotePath = (Helper.RemoteUploadUrl).Replace("\\", "/");
                    WebClient theClient = new WebClient();
                    theClient.DownloadFile(filepath, image);
                    //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
                    theClient.Dispose();


                    string images = @"c:\Case\\images\" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6));
                    if (System.IO.File.Exists(images))
                    {
                        try
                        {
                            string filepaths = images;
                            // string localPaths = new Uri(filepaths).LocalPath;
                            string remotePaths = Helper.RemoteUploadUrl;

                            WebClient theClients = new WebClient();
                            byte[] responseArray = theClients.UploadFile(remotePaths, filepaths);
                            Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
                            theClients.Dispose();
                        }
                        catch (Exception c)
                        {

                        }
                    }

                    // MessageBox.Show("done");

                }
                catch (Exception c)
                {
                    //  MessageBox.Show(c.ToString());
                    Console.WriteLine(c.ToString());

                }
                // return;
                string Query2 = "DELETE from client WHERE clientID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.remoteConn);

                string Query = "INSERT INTO `client`(`clientID`, `orgID`, `name`, `email`, `contact`, `status`, `image`, `address`, `created`, `action`, `lawyer`, `registration`, `password`, `sync`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','none','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','t');";
                Helper.Execute(Query, DBConnect.remoteConn);

                string Query3 = "UPDATE `client` SET `sync`='t' WHERE clientID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);
            }
            connection.Close();
        }
        private void SyncOrg()
        {

            string paths = @"c:\Case\images";
            if (!Directory.Exists(paths))
            {
                DirectoryInfo dim = Directory.CreateDirectory(paths);
                Console.WriteLine("The directory was created successfully at {0}.",
                Directory.GetCreationTime(paths));
            }
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM org WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;

                try
                {
                    string filepath = (Helper.imageUrl + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)));
                    string image = @"c:\Case\\images\" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9));

                    string remotePath = (Helper.RemoteUploadUrl).Replace("\\", "/");
                    WebClient theClient = new WebClient();
                    theClient.DownloadFile(filepath, image);
                    //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
                    theClient.Dispose();
                    string images = @"c:\Case\\images\" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9));
                    if (System.IO.File.Exists(images))
                    {
                        try
                        {
                            string filepaths = images;
                            // string localPaths = new Uri(filepaths).LocalPath;
                            string remotePaths = Helper.RemoteUploadUrl;

                            WebClient theClients = new WebClient();
                            byte[] responseArray = theClients.UploadFile(remotePaths, filepaths);
                            Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
                            theClients.Dispose();
                        }
                        catch (Exception c)
                        {

                        }
                    }

                    // MessageBox.Show("done");

                }
                catch (Exception c)
                {
                    //  MessageBox.Show(c.ToString());
                    Console.WriteLine(c.ToString());

                }
                // return;
                string Query2 = "DELETE from org WHERE orgID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.remoteConn);

                string Query = "INSERT INTO `org`(`orgID`, `name`, `license`, `starts`, `ends`, `code`, `address`, `email`, `status`, `image`, `currency`, `country`, `region`, `city`, `action`, `tin`, `vat`, `top`, `sync`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','" + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)) + "','t');";
                Helper.Execute(Query, DBConnect.remoteConn);

                string Query3 = "UPDATE `org` SET `sync`='t' WHERE orgID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);

            }
            connection.Close();
        }
        private void SyncExpense()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM expenses WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from expenses WHERE expenseID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.remoteConn);

                string Query = "INSERT INTO  `expenses` (`expenseID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `method`, `amount`, `no`, `balance`, `paid`, `date`, `approved`, `signed`, `reason`, `outcome`, `deadline`,`sync`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','t');";
                Helper.Execute(Query, DBConnect.remoteConn);

                string Query3 = "UPDATE `expenses` SET `sync`='t' WHERE expenseID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);
            }
            connection.Close();
        }
        private void DownloadExpense()
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.remoteConn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM expenses WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;
            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from expenses WHERE expensesID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.conn);
                string Query = "INSERT INTO  `expenses`(`expenseID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `method`, `amount`, `no`, `balance`, `paid`, `date`, `approved`, `signed`, `reason`, `outcome`, `deadline`,sync) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','t';";
                Helper.Execute(Query, DBConnect.conn);
                string Query3 = "UPDATE `expenses` SET `sync`='t' WHERE expenseID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.remoteConn);

            }
            connection.Close();
        }
        private void SyncFees()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM fees WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from fees WHERE feeID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.remoteConn);

                string Query = "INSERT INTO  `fees`(`feeID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `paid`, `invoice`, `vat`, `method`, `amount`, `received`, `balance`, `approved`, `signed`, `date`,sync) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','t';";
                Helper.Execute(Query, DBConnect.remoteConn);

                string Query3 = "UPDATE `fees` SET `sync`='t' WHERE feeID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);

            }
            connection.Close();
        }
        private void DownloadFees()
        {
            MySqlConnection connection = new MySqlConnection(DBConnect.remoteConn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM fees WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from fees WHERE feeID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.conn);

                string Query = "INSERT INTO  `fees`(`feeID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `paid`, `invoice`, `vat`, `method`, `amount`, `received`, `balance`, `approved`, `signed`, `date`,sync) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','t';";
                Helper.Execute(Query, DBConnect.conn);

                string Query3 = "UPDATE `fees` SET `sync`='t' WHERE feeID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.remoteConn);

            }
            connection.Close();
        }
        private void SyncDisbbursements()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM disbursements WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from disbursements WHERE disbursementD ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.remoteConn);

                string Query = "INSERT INTO  `disbursements`(`disbursementID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `paid`, `invoice`, `method`, `amount`, `received`, `balance`, `approved`, `signed`, `date`,sync) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','t';";
                Helper.Execute(Query, DBConnect.remoteConn);

                string Query3 = "UPDATE `disbursements` SET `sync`='t' WHERE disbursementID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);

            }
            connection.Close();
        }
        private void DownloadDisbbursements()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.remoteConn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM disbursements WHERE sync ='f' ;";
            connection.Open();
            Reader = command.ExecuteReader();
            int totalRow = 0;

            while (Reader.Read())
            {
                totalRow++;
                string Query2 = "DELETE from disbursements WHERE disbursementD ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query2, DBConnect.conn);

                string Query = "INSERT INTO  `disbursements`(`disbursementID`, `orgID`, `clientID`, `fileID`, `details`, `lawyer`, `paid`, `invoice`, `method`, `amount`, `received`, `balance`, `approved`, `signed`, `date`,sync) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','t';";
                Helper.Execute(Query, DBConnect.conn);

                string Query3 = "UPDATE `disbursements` SET `sync`='t' WHERE disbursementID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.remoteConn);

            }
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
          //  calendar1.SetDaysMode(CalendarDaysMode.Short);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
