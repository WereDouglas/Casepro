using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Casepro
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadSettings();

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
                    Helper.serverName = server.Name;
                    Helper.serverIP = server.Ip;
                    Helper.port = server.Port;

                    Helper.fileUrl = "http://" + server.Ip + "/caseprofessionals/files/";
                    Helper.imageUrl = "http://" + server.Ip + "/caseprofessionals/uploads/";
                    Helper.uploadUrl = "http://" + server.Ip + "/caseprofessionals/uploads/uploads.php";
                    Helper.RemoteUploadUrl = "http://caseprofessional.org/uploads/uploads.php";
                    Helper.msgUrl = "http://" + server.Ip + "/caseprofessionals/index.php/message/event";
                }
               // MessageBox.Show(Helper.serverIP);
                HomeForm frm = new HomeForm();
                frm.MdiParent = this;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
            catch
            {

                ServerForm frm = new ServerForm();
                frm.Show();
                this.Close();
            }


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {


        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            try
            {
                UserForm frm = new UserForm();
                frm.MdiParent = this;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FileForm frm = new FileForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();

        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            HomeForm frm = new HomeForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ClientForm frm = new ClientForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            EventForm frm = new EventForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DocumentForm frm = new DocumentForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            SettingForm frm = new SettingForm();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerForm frm = new ServerForm();
            frm.Show();
        }
    }
}
