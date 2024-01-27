using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Project
{
    public partial class ministatement : Form
    {
        public ministatement()
        {
            InitializeComponent();
        }

        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ATMDb2.mdf;Integrated Security=True;Connect Timeout=30");

        private string Acc = Login.AccNumber;

        private void populate()
        {
            Con.Open();
            String query = "select * from TransactionTbl where AccNum ='" + Acc + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MinistatementDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ministatement_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            this.Hide();
            home.Show();
        }

        private void MinistatementDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}