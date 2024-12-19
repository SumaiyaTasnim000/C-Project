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

namespace Pet___Beyonds
{
    public partial class Form3 : Form
    {
        DBcon dbc = new DBcon();
        public Form3()
        {
            InitializeComponent();
        }
      
 
        int check(string Email)
        {
            dbc.Opencon();

            string query = "select count(*) from RegDbtbl where Email='" + Email + "'";
            //Console.WriteLine("Query: " + query);
            SqlCommand command = new SqlCommand(query, dbc.GetCon());
            int v = (int)command.ExecuteScalar();
            dbc.Closecon();
            return v;

        }
  
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void signbtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtfirstname.Text != "" && txtlastname.Text != "" && dateTimePicker1.Text != "" && txtgender.Text != "" && txtmail.Text != "" && txtaddr.Text != "" && txtpass.Text != "" && txtconpass.Text != "")
                {

                    if (txtpass.Text == txtconpass.Text)
                    {

                        int v = check(txtmail.Text);
                        if (v != 1)
                        {
                            dbc.Opencon();

                            SqlCommand command = new SqlCommand("INSERT INTO RegDbtbl (FirstName, LastName, Birthdate, Gender, [address], [Email], [password]) VALUES (@FirstName, @LastName, @Birthdate, @Gender, @address, @Email, @password)", dbc.GetCon());

                            command.Parameters.AddWithValue("@FirstName", txtfirstname.Text);
                            command.Parameters.AddWithValue("@LastName", txtlastname.Text);
                            command.Parameters.AddWithValue("@Birthdate", dateTimePicker1.Value);
                            command.Parameters.AddWithValue("@Gender", txtgender.Text);
                            command.Parameters.AddWithValue("@address", txtaddr.Text);
                            command.Parameters.AddWithValue("@Email", txtmail.Text);
                            command.Parameters.AddWithValue("@password", txtpass.Text);
                            command.ExecuteNonQuery();
                            dbc.Closecon();
                            MessageBox.Show("Registration succesesed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtfirstname.Text = "";
                            txtlastname.Text = "";
                            // dateTimePicker1.Text = "";
                            txtgender.Text = "";
                            txtmail.Text = "";
                            txtaddr.Text = "";
                            txtpass.Text = "";
                            txtconpass.Text = "";



                        }
                        else
                        {
                            MessageBox.Show("You are already registred ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }



                    }
                    else
                    {
                        MessageBox.Show("password doesn't match! ", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("please enter all the valid information ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            // name = textBox1.Text;
            // id = textBox2.Text;

            Form2 f2 = new Form2();
            f2.Show();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                txtpass.UseSystemPasswordChar = false;
                txtconpass.UseSystemPasswordChar = false;


            }
            else
            {
                txtpass.UseSystemPasswordChar = true;
                txtconpass.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                txtpass.UseSystemPasswordChar = false;
                txtconpass.UseSystemPasswordChar = false;


            }
            else
            {
                txtpass.UseSystemPasswordChar = true;
                txtconpass.UseSystemPasswordChar = true;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
