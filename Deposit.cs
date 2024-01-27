using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ATM_Project
{
    public partial class Deposit : Form
    {
        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ATMDb2.mdf;Integrated Security=True;Connect Timeout=30");
        private string Acc = Login.AccNumber;
        private int oldbalance, newbalance;

        public Deposit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DepoAmtTb.Text == "" || Convert.ToInt32(DepoAmtTb.Text) <= 0)
            {
                MessageBox.Show("Enter The Amount To Deposit");
            }
            else
            {
                newbalance = oldbalance + Convert.ToInt32(DepoAmtTb.Text);
                try
                {
                    Con.Open();
                    string query = "UPDATE AccountTbl SET Balance = @NewBalance WHERE Accnum = @AccountNumber";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    // Use parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@NewBalance", newbalance);
                    cmd.Parameters.AddWithValue("@AccountNumber", Acc);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Balance Updated");

                    Con.Close();
                    addtransaction();

                    // Close the current form before opening the HOME form
                    this.Hide();
                    HOME home = new HOME();
                    home.Show();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            home.Show();
            this.Hide();
        }

        private void getbalance()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Balance from AccountTbl where AccNum = '" + Acc + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            oldbalance = Convert.ToInt32(dt.Rows[0][0].ToString());

            Con.Close();
        }

        private void Deposit_Load(object sender, EventArgs e)
        {
            getbalance();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addtransaction()
        {
            string TrType = "Deposit";
            try
            {
                Con.Open();

                string query = "insert into TransactionTbl values('" + Acc + "','" + TrType + "' , " + DepoAmtTb.Text + " , '" + DateTime.Today.Date.ToString() + "'  )";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();

                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}