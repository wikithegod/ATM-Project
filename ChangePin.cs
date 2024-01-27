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
    public partial class ChangePin : Form
    {
        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\ATMDb2.mdf;Integrated Security=True;Connect Timeout=30");
        private string Acc = Login.AccNumber;

        public ChangePin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Pin1Tb.Text == "" || Pin2Tb.Text == "")
            {
                MessageBox.Show("Enter Your New Pin");
            }
            else if (Pin2Tb.Text != Pin1Tb.Text)
            {
                MessageBox.Show("Entered Pins Are Different");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "UPDATE AccountTbl SET PIN = @NewPin WHERE Accnum = @AccountNumber";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    // Use parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@NewPin", Pin1Tb.Text);
                    cmd.Parameters.AddWithValue("@AccountNumber", Acc);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("PIN Updated Successfully");

                    Con.Close();
                    Login home = new Login();
                    home.Show();
                    this.Hide();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ChangePin_Load(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            this.Hide();
            home.Show();
        }
    }
}