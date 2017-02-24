using MySql.Data.MySqlClient;
using System;
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
    public partial class FinancialForm : Form
    {
        DataTable t = new DataTable();
        string month;
        double totalExpense = 0;
        double totalFees = 0;
        double amount = 0;
        List<string> Expenses = new List<string>();
       
        Dictionary<string, string> ExpenseDictionary = new Dictionary<string, string>();
        Dictionary<string, string> FeesDictionary = new Dictionary<string, string>();
        public FinancialForm()
        {
            InitializeComponent();
            printdoc1.PrintPage += new PrintPageEventHandler(printdoc1_PrintPage);
            month = DateTime.Now.ToString("yyyy-MM");
            loadDis();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Print(this.panel1);
        }
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
     
        private void loadDis()
        {

            MySqlConnection connection = new MySqlConnection(DBConnect.conn);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT fees.date As date,fees.invoice AS invoice,client.name As client,file.name As file,fees.amount As amount,fees.balance AS balance,fees.method as method,fees.details AS details,fees.paid AS paid FROM fees LEFT JOIN client ON client.clientID = fees.clientID LEFT JOIN file ON file.fileID = fees.fileID WHERE fees.date LIKE '%" + month + "%';";
            connection.Open();
            Reader = command.ExecuteReader();
            // create and execute query  
            t = new DataTable();

            t.Columns.Add("DATE", typeof(string));//0
            t.Columns.Add("No.", typeof(string));//1
            t.Columns.Add("CLIENT", typeof(string));//2
            t.Columns.Add("FILE", typeof(string));     //3      
            t.Columns.Add("AMOUNT", typeof(string));//4
            t.Columns.Add("BAL.", typeof(string));//5
            t.Columns.Add("METHOD", typeof(string));//6
            t.Columns.Add("DETAILS", typeof(string));//7
            t.Columns.Add("PAID", typeof(string));//8
            t.Columns.Add(" ", typeof(string));//8
            t.Rows.Add(new object[] { " ", " ", "", "", "FINANCIAL", "REPORT ", ""+month, "", "", "" });
            t.Rows.Add(new object[] { "FEES", " ", "", "", "", "", "", "", "", "" });
            t.Rows.Add(new object[] { "Date", "Invoice No.", "Client", "File", "Amount", "Balance", "Method", "Details", "Paid", "" });
            while (Reader.Read())
            {
                t.Rows.Add(new object[] { (Reader.IsDBNull(0) ? "none" : Reader.GetString(0)), (Reader.IsDBNull(1) ? "none" : Reader.GetString(1)), (Reader.IsDBNull(2) ? "none" : Reader.GetString(2)), (Reader.IsDBNull(3) ? "none" : Reader.GetString(3)),Convert.ToDouble( (Reader.IsDBNull(4) ? "none" : Reader.GetString(4))).ToString("n0"), (Reader.IsDBNull(5) ? "none" : Reader.GetString(5)), (Reader.IsDBNull(6) ? "none" : Reader.GetString(6)), (Reader.IsDBNull(7) ? "none" : Reader.GetString(7)), (Reader.IsDBNull(8) ? "none" : Reader.GetString(8)) });
                FeesDictionary.Add((Reader.IsDBNull(0) ? "none" : Reader.GetString(0)), (Reader.IsDBNull(4) ? "none" : Reader.GetString(4)));
            }
            try
            {
                totalFees= FeesDictionary.Sum(m => Convert.ToDouble(m.Value));
                // double amount = totalRent - totalExpense;
                t.Rows.Add(new object[] { " ", " ", "TOTAL FEES:", "", "" + totalFees.ToString("n0"), "" , "", "", "", "" });
            }
            catch
            {


            }
            connection.Close();
            MySqlConnection connection2 = new MySqlConnection(DBConnect.conn);
            MySqlCommand command2 = connection2.CreateCommand();
            MySqlDataReader Reader2;
            command2.CommandText = "SELECT *  FROM petty WHERE date LIKE '%" + month + "%';";
            connection2.Open();
            Reader2 = command2.ExecuteReader();
            t.Rows.Add(new object[] { "", " ", "", "", "", "", "", "", "", "" });
            t.Rows.Add(new object[] { "FIRM EXPENSES", " ", "", "", "", "", "", "", "", "" });
            t.Rows.Add(new object[] { "Date", "Invoice No.","Item","Unit cost","Qty","Total Cost", "Method", "Reason", "Paid","Approved" });
            while (Reader2.Read())
            {
                t.Rows.Add(new object[] { (Reader2.IsDBNull(5) ? "none" : Reader2.GetString(5)), (Reader2.IsDBNull(0) ? "none" : Reader2.GetString(0)), (Reader2.IsDBNull(1) ? "none" : Reader2.GetString(1)), Convert.ToDouble((Reader2.IsDBNull(2) ? "none" : Reader2.GetString(2))).ToString("n0"), (Reader2.IsDBNull(3) ? "none" : Reader2.GetString(3)), Convert.ToDouble((Reader2.IsDBNull(4) ? "none" : Reader2.GetString(4))).ToString("n0"), (Reader2.IsDBNull(10) ? "none" : Reader2.GetString(10)), (Reader2.IsDBNull(9) ? "none" : Reader2.GetString(9)), (Reader2.IsDBNull(6) ? "none" : Reader2.GetString(6)), (Reader2.IsDBNull(11) ? "none" : Reader2.GetString(11)) });
                ExpenseDictionary.Add((Reader2.IsDBNull(0) ? "none" : Reader2.GetString(0)), (Reader2.IsDBNull(4) ? "none" : Reader2.GetString(4)));
            }
            connection2.Close();

            dtGrid.DataSource = t;
            dtGrid.Rows[1].DefaultCellStyle.BackColor = Color.Beige;

            try
            {
                totalExpense = ExpenseDictionary.Sum(m => Convert.ToDouble(m.Value));
                  amount = totalFees - totalExpense;
                 t.Rows.Add(new object[] { " ", " ", "", "TOTAL EXPENSES:", "", ""+ totalExpense.ToString("n0"), "", "", "", "" });
            }
            catch
            {


            }
            // double amount = totalRent - totalExpense;
            t.Rows.Add(new object[] { " ", " ", "", "", "PROFIT DECLARATION: ", "" + amount.ToString("n0"), "", "", "", "" });

        }

        private void monthPicker_CloseUp(object sender, EventArgs e)
        {
            month = Convert.ToDateTime(monthPicker.Text).ToString("yyyy-MM");
            loadDis();
        }

        private void FinancialForm_Leave(object sender, EventArgs e)
        {
            Close();
        }
    }
}
