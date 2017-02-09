using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Casepro
{
    public partial class SettingForm : Form
    {
        BackgroundWorker m_oWorker;
        public SettingForm()
        {
            InitializeComponent();
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
            LoadSettings();
        }

        /// <summary>
        /// On completed do the appropriate task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            btnStartAsyncOperation.Enabled = true;
            btnCancel.Enabled = false;
        }

        /// <summary>
        /// Notification is performed here to the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            // This function fires on the UI thread so it's safe to edit

            // the UI control directly, no funny business with Control.Invoke :)

            // Update the progressBar with the integer supplied to us from the

            // ReportProgress() function.  

            progressBar1.Value = e.ProgressPercentage;
            lblStatus.Text = "Processing......" + progressBar1.Value.ToString() + "%";
        }

        /// <summary>
        /// Time consuming operations go here </br>
        /// i.e. Database operations,Reporting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // The sender is the BackgroundWorker object we need it to
            // report progress and check for cancellation.
            //NOTE : Never play with the UI thread here...
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
                    Thread.Sleep(100);
                // Periodically report progress to the main thread so that it can
                // update the UI.  In most cases you'll just need to send an
                // integer that will update a ProgressBar
                //t.Rows.Add(new object[] { Reader.GetString(0), false, (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)), (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)), (Reader.IsDBNull(2) ? "none" : Convert.ToDateTime(Reader.GetString(2)).ToString("HH:MM")), (Reader.IsDBNull(3) ? "none" : Convert.ToDateTime(Reader.GetString(3)).ToString("HH:MM")), (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)), (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)), (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)), (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) });

                //t.Rows.Add(new object[] {Reader.GetString(0),Reader.GetString(1),Reader.GetString(2),Reader.GetString(3),Reader.GetString(4)});
                string Query = "INSERT INTO `events`(`id`, `name`, `start`, `end`, `user`, `file`, `created`, `action`, `status`, `orgID`, `date`, `hours`, `court`, `notify`,`priority`, `sync`,`progress`,`client`) VALUES ('" + Reader.GetString(0) + "','" + (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)) + "','" + (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)) + "','" + (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)) + "','" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + "','" + (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)) + "','" + (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)) + "','"+ (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)) + "','"+ (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) + "','"+ (Reader.IsDBNull(9) ? "none" : Reader.GetString(9)) + "','" + (Reader.IsDBNull(10) ? "none" : Reader.GetString(10)) + "','"+ (Reader.IsDBNull(11) ? "none" : Reader.GetString(11)) + "','" + (Reader.IsDBNull(12) ? "none" : Reader.GetString(12)) + "','" + (Reader.IsDBNull(13) ? "none" : Reader.GetString(13)) + "','t','"+ (Reader.IsDBNull(15) ? "none" : Reader.GetString(15)) + "','" + (Reader.IsDBNull(16) ? "none" : Reader.GetString(16)) + "','" + (Reader.IsDBNull(17) ? "none" : Reader.GetString(17)) + "');";
                MySqlConnection MyConn2 = new MySqlConnection(DBConnect.remoteConn);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MyConn2.Close();

                string Query3 = "UPDATE `events` SET `sync`='t' WHERE id ='" + Reader.GetString(0) + "'";
                MySqlConnection MyConn3 = new MySqlConnection(DBConnect.conn);
                MySqlCommand MyCommand3 = new MySqlCommand(Query3, MyConn3);
                MySqlDataReader MyReader3;
                MyConn3.Open();
                MyReader3 = MyCommand3.ExecuteReader();
               // MessageBox.Show("Information Updated");
                MyConn3.Close();

                m_oWorker.ReportProgress(totalRow);
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
                
            }
            connection.Close();

            //Report 100% completion on operation completed
            m_oWorker.ReportProgress(100);
        }

        private void btnStartAsyncOperation_Click(object sender, EventArgs e)
        {
            //Change the status of the buttons on the UI accordingly
            //The start button is disabled as soon as the background operation is started
            //The Cancel button is enabled so that the user can stop the operation 
            //at any point of time during the execution
            btnStartAsyncOperation.Enabled = false;
            btnCancel.Enabled = true;

            // Kickoff the worker thread to begin it's DoWork function.
            m_oWorker.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (m_oWorker.IsBusy)
            {

                // Notify the worker thread that a cancel has been requested.

                // The cancel will not actually happen until the thread in the

                // DoWork checks the m_oWorker.CancellationPending flag. 

                m_oWorker.CancelAsync();
            }
        }

        private void SettingForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            XElement xml = new XElement("Servers",
            new XElement("Server",
            new XElement("Name", remoteNameTxt.Text),
            new XElement("Ip", remoteIpTxt.Text),
            new XElement("Port", remotePortTxt.Text)
            )
            );

            xml.Save("RemoteXMLFile.xml");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            XElement xml = new XElement("Servers",
            new XElement("Server",
            new XElement("Name", localNameTxt.Text),
            new XElement("Ip", localIpTxt.Text),
            new XElement("Port", localPortTxt.Text)
            )
            );

            xml.Save("LocalXMLFile.xml");

        }
        private void LoadSettings()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load("LocalXMLFile.xml");


                var servers = from person in xmlDoc.Descendants("Server")
                              select new
                              {
                                  Name = person.Element("Name").Value,
                                  Ip = person.Element("Ip").Value,
                                  Port = person.Element("Port").Value,
                              };


                foreach (var server in servers)
                {
                    localNameTxt.Text = server.Name;
                    localIpTxt.Text = server.Ip;
                    localPortTxt.Text = server.Port;
                    Helper.serverName = server.Name;
                    Helper.serverIP = server.Ip;
                    Helper.port = server.Port;
                }
            }
            catch { }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}