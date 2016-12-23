using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LoginWindow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JRSubrean\Documents\LoginInfo.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From LoginInfo where Username ='" + textBox1.Text + "' and Password = '" + textBox2.Text + "'", connect);
            DataTable tableOfData = new DataTable();
            sda.Fill(tableOfData);
            if (tableOfData.Rows[0][0].ToString() == "1")
            {
                this.Hide();

                Main aquaPage = new LoginWindow.Main();
                aquaPage.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username and/or Password combination. Please try again.");
            }
        }
    }
}
