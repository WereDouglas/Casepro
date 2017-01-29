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
using System.Windows.Forms.Calendar;

namespace Casepro
{
    public partial class HomeForm : Form
    {
        List<CalendarItem> _items = new List<CalendarItem>();
        CalendarItem contextItem = null;
        public HomeForm()
        {
            InitializeComponent();
            //Monthview colors
            monthView2.MonthTitleColor = monthView2.MonthTitleColorInactive = CalendarColorTable.FromHex("#C2DAFC");
            monthView2.ArrowsColor = CalendarColorTable.FromHex("#77A1D3");
            monthView2.DaySelectedBackgroundColor = CalendarColorTable.FromHex("#F4CC52");
            monthView2.DaySelectedTextColor = monthView2.ForeColor;
           // calendar2.DaysMode = CalendarDaysMode.Short;
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
            connection.Open();
            Reader = command.ExecuteReader();
            string state = "";
            while (Reader.Read())
            {
                //CalendarItem cal = new CalendarItem(calendar2,Convert.ToDateTime(Reader.GetString(5)+"T"+Reader.GetString(3)+":00"), Convert.ToDateTime(Reader.GetString(5) + "T" + Reader.GetString(4) + ":00"), Reader.GetString(2));
                System.Diagnostics.Debug.WriteLine(Reader.GetString(2));
                //Reader.IsDBNull(2) ? "": Reader.GetString(2)
                CalendarItem cal = new CalendarItem(calendar2, Convert.ToDateTime(Reader.GetString(2)), Convert.ToDateTime(Reader.GetString(3)), Reader.GetString(1) + " C/O:" + (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)) + " File:"+ (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)));
               
                if (Reader.IsDBNull(13)) {
                    state = "none";
                }
                else {
                    state = Reader.GetString(13);
                }
                if (state=="medium") {  cal.ApplyColor(Color.Green);  }
                if (state == "low") { cal.ApplyColor(Color.CornflowerBlue); }
                if (state == "high") { cal.ApplyColor(Color.Red); }
                if (state=="none") {cal.ApplyColor(Color.Cornsilk); }
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
    }
}
