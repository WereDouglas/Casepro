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
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
            LoadSettings();
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
            LoginForm frm = new LoginForm();
            frm.Show();
            this.Close();
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
    }
}
