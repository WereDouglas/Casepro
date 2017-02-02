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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Calendar;

namespace Casepro
{
    public partial class HomeForm : Form
    {
        List<CalendarItem> _items = new List<CalendarItem>();
        CalendarItem contextItem = null;
        BackgroundWorker m_oWorker;
        public HomeForm()
        {
            InitializeComponent();
            //Monthview colors
            monthView2.MonthTitleColor = monthView2.MonthTitleColorInactive = CalendarColorTable.FromHex("#C2DAFC");
            monthView2.ArrowsColor = CalendarColorTable.FromHex("#77A1D3");
            monthView2.DaySelectedBackgroundColor = CalendarColorTable.FromHex("#F4CC52");
            monthView2.DaySelectedTextColor = monthView2.ForeColor;
            // calendar2.DaysMode = CalendarDaysMode.Short;            

            m_oWorker = new BackgroundWorker();

            // Create a background worker thread that ReportsProgress &
            // SupportsCancellation
            // Hook up the appropriate events.
            m_oWorker.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);
            m_oWorker.ProgressChanged += new ProgressChangedEventHandler
                    (m_oWorker_ProgressChanged);
            m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (m_oWorker_RunWorkerCompleted);
            m_oWorker.WorkerReportsProgress = true;
            m_oWorker.WorkerSupportsCancellation = true;
            background();
            userprofile();
        }
        void userprofile() {
            try
            {

                var request = WebRequest.Create(Helper.imageUrl + Helper.image);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    imgCapture.Image = Bitmap.FromStream(stream);

                }
            }
            catch { }
            orgLbl.Text = Helper.orgID;
            nameLbl.Text = Helper.username;
            contactLbl.Text = Helper.contact;


        }
        private void background()
        {

            m_oWorker.RunWorkerAsync();

        }

        private void HomeForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }


        private void calendar2_LoadItems(object sender, CalendarLoadEventArgs e)
        {
            PlaceItems();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            List<ItemInfo> lst = new List<ItemInfo>();
            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT * FROM events";
            try
            {
                connection.Open();
            }
            catch {
                MessageBox.Show("Server is offline");

            }
            Reader = command.ExecuteReader();
            string state = "";
            while (Reader.Read())
            {
                //CalendarItem cal = new CalendarItem(calendar2,Convert.ToDateTime(Reader.GetString(5)+"T"+Reader.GetString(3)+":00"), Convert.ToDateTime(Reader.GetString(5) + "T" + Reader.GetString(4) + ":00"), Reader.GetString(2));
                System.Diagnostics.Debug.WriteLine(Reader.GetString(2));
                //Reader.IsDBNull(2) ? "": Reader.GetString(2)
                CalendarItem cal = new CalendarItem(calendar2, Convert.ToDateTime(Reader.GetString(2)), Convert.ToDateTime(Reader.GetString(3)), Reader.GetString(1) + " C/O:" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + " File:" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)));

                if (Reader.IsDBNull(13))
                {
                    state = "none";
                }
                else
                {
                    state = Reader.GetString(13);
                }
                if (state == "medium") { cal.ApplyColor(Color.Green); }
                if (state == "low") { cal.ApplyColor(Color.CornflowerBlue); }
                if (state == "high") { cal.ApplyColor(Color.Red); }
                if (state == "none") { cal.ApplyColor(Color.Cornsilk); }
                _items.Add(cal);
                // t.Rows.Add(new object[] { Reader.GetString(0), Helper.imageUrl + Reader.GetString(8), b, Reader.GetString(2), Reader.GetString(3), Reader.GetString(7), Reader.GetString(5), Reader.GetString(9), Reader.GetString(14) + "", Reader.GetString(6), "" + Reader.GetString(13) + "" });

            }

            PlaceItems();

        }
        private void PlaceItems()
        {
            foreach (CalendarItem item in _items)
            {
                if (calendar2.ViewIntersects(item))
                {
                    calendar2.Items.Add(item);
                }
            }
        }

        private void calendar2_ItemCreated(object sender, CalendarItemCancelEventArgs e)
        {
            _items.Add(e.Item);
        }

        private void calendar2_ItemMouseHover(object sender, CalendarItemEventArgs e)
        {
            Text = e.Item.Text;
        }

        private void calendar2_ItemClick(object sender, CalendarItemEventArgs e)
        {
            //MessageBox.Show(e.Item.Text);
        }

        private void hourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calendar2.TimeScale = CalendarTimeScale.SixtyMinutes;
        }

        private void minutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calendar2.TimeScale = CalendarTimeScale.ThirtyMinutes;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            calendar2.TimeScale = CalendarTimeScale.FifteenMinutes;
        }

        private void minutesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            calendar2.TimeScale = CalendarTimeScale.SixMinutes;
        }

        private void minutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            calendar2.TimeScale = CalendarTimeScale.TenMinutes;
        }

        private void minutesToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            calendar2.TimeScale = CalendarTimeScale.FiveMinutes;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void redTagToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void yellowTagToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void greenTagToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void blueTagToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calendar2.ActivateEditMode();
        }




        private void otherColorTagToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void calendar2_ItemDoubleClick(object sender, CalendarItemEventArgs e)
        {
            MessageBox.Show("Double click: " + e.Item.Text);
        }

        private void calendar2_ItemDeleted(object sender, CalendarItemEventArgs e)
        {
            _items.Remove(e.Item);
        }

        private void calendar2_DayHeaderClick(object sender, CalendarDayEventArgs e)
        {
            calendar2.SetViewRange(e.CalendarDay.Date, e.CalendarDay.Date);
        }

        private void diagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hatchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void monthView2_SelectionChanged(object sender, EventArgs e)
        {
            calendar2.SetViewRange(monthView2.SelectionStart, monthView2.SelectionEnd);
        }

        private void northToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eastToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void southToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void westToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void monthView2_SelectionChanged_1(object sender, EventArgs e)
        {
            calendar2.SetViewRange(monthView2.SelectionStart, monthView2.SelectionEnd);
        }

        private void calendar2_DayHeaderClick_1(object sender, CalendarDayEventArgs e)
        {
            calendar2.SetViewRange(e.CalendarDay.Date, e.CalendarDay.Date);
        }

        private void calendar2_ItemCreated_1(object sender, CalendarItemCancelEventArgs e)
        {
            _items.Add(e.Item);
        }

        private void calendar2_ItemDoubleClick_1(object sender, CalendarItemEventArgs e)
        {
            MessageBox.Show("Double click: " + e.Item.Text);
        }

        private void monthView2_SelectionChanged_2(object sender, EventArgs e)
        {
            calendar2.SetViewRange(monthView2.SelectionStart, monthView2.SelectionEnd);
        }

        private void calendar2_LoadItems_1(object sender, CalendarLoadEventArgs e)
        {
            PlaceItems();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NewEvent frm = new NewEvent(null);
            // frm.MdiParent = MainForm.ActiveForm;
            frm.Show();
            //this.Close();
        }
        void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled)
            {
                lblStatus.Text = "Task Cancelled.";
            }

            // Check to see if an error occurred in the background process.

            else if (e.Error != null)
            {
                lblStatus.Text = "Error while performing background operation.";
            }
            else
            {
                // Everything completed normally.
                lblStatus.Text = "Task Completed...";
            }

            //Change the status of the buttons on the UI accordingly

        }
       
        void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            // This function fires on the UI thread so it's safe to edit

            // the UI control directly, no funny business with Control.Invoke :)

            // Update the progressBar with the integer supplied to us from the

            // ReportProgress() function.  

            progressBar1.Value = e.ProgressPercentage;
            lblStatus.Text = "Synchronising data......" + progressBar1.Value.ToString() + "%";
        }

        /// <summary>
        /// Time consuming operations go here </br>
        /// i.e. Database operations,Reporting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // var request = (HttpWebRequest)WebRequest.Create(Helper.msgUrl);
            //request.GetResponse();
            // The sender is the BackgroundWorker object we need it to
            // report progress and check for cancellation.
            //NOTE : Never play with the UI thread here...
            Thread.Sleep(100);
            try
            {
                SyncEvents();
                SyncUsers();
                SyncClients();
                SyncFiles();
                SyncOrg();
            }
            catch
            {
              
                return;
            }
            m_oWorker.ReportProgress(1);
            // Periodically check if a cancellation request is pending.
            // If the user clicks cancel the line
            // m_AsyncWorker.CancelAsync(); if ran above.  This
            // sets the CancellationPending to true.
            // You must check this flag in here and react to it.
            // We react to it by setting e.Cancel to true and leaving
            if (m_oWorker.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                m_oWorker.ReportProgress(0);
                return;
            }


            //Report 100% completion on operation completed
            m_oWorker.ReportProgress(100);
        }
        int count;
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
                string Query = "INSERT INTO `events`(`id`, `name`, `start`, `end`, `user`, `file`, `created`, `action`, `status`, `orgID`, `date`, `hours`, `court`, `notify`,`priority`, `sync`,`progress`,`client`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','t','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','" + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)) + "');";
                Helper.Execute(Query, DBConnect.remoteConn);
                string Query3 = "UPDATE `events` SET `sync`='t' WHERE id ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);
               

            }
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

                string Query = "INSERT INTO `org`(`orgID`, `name`, `license`, `starts`, `ends`, `code`, `address`, `email`, `status`, `image`, `currency`, `country`, `region`, `city`, `action`, `tin`, `vat`, `top`, `sync`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','" + (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','" + (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','" + (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','" + (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','" + (Reader.IsDBNull(14) ? "none" : Reader.GetString(14)) + "','" + (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','" + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)) + "','" + (Reader.IsDBNull(18) ? "none" : Reader.GetString(18)) + "','t');";
                Helper.Execute(Query, DBConnect.remoteConn);

                string Query3 = "UPDATE `org` SET `sync`='t' WHERE orgID ='" + Reader.GetString(0) + "'";
                Helper.Execute(Query3, DBConnect.conn);

            }
            connection.Close();


        }
    }
}
