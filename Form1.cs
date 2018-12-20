using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace NorthWindExercise
{
    public partial class formNorthWind : Form
    {

        OleDbConnection connection;
        OleDbCommand commandSelectCustomers;
        OleDbDataAdapter adapterCustomer;
        DataTable tableCustomers;
        CurrencyManager currencyManager;

        public formNorthWind()
        {
            InitializeComponent();
        }

        private void formNorthWind_Load(object sender, EventArgs e)
        {
            var connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;
                Data Source=D:\PROJETOS\UdemyCourse (Alex)\NorthWindExercise UdemyProject3\NorthWind.accdb;
                Persist Security Info = False; ";

            connection = new OleDbConnection(connectionString);
            connection.Open();


            commandSelectCustomers = new OleDbCommand("SELECT * FROM CUSTOMERS", connection);
            adapterCustomer = new OleDbDataAdapter();
            adapterCustomer.SelectCommand = commandSelectCustomers;
            tableCustomers = new DataTable();
            adapterCustomer.Fill(tableCustomers);

            //bind controls
            txtCustomerId.DataBindings.Add("Text", tableCustomers, "CustomerID");
            txtCompName.DataBindings.Add("Text", tableCustomers, "CompanyName");
            txtContName.DataBindings.Add("Text", tableCustomers, "ContactName");
            txtContTitle.DataBindings.Add("Text", tableCustomers, "ContactTitle");

            //establish currency manager
            currencyManager = (CurrencyManager)BindingContext[tableCustomers];


            connection.Close();
            connection.Dispose();
            adapterCustomer.Dispose();
            tableCustomers.Dispose();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currencyManager.Position = 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currencyManager.Position++;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currencyManager.Position--;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currencyManager.Position = currencyManager.Count;
        }


    }
}
