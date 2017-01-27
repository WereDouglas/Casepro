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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
    }
}
