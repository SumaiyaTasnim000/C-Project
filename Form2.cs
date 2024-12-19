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
    public partial class Form2 : Form
    {
       
        DBcon dbc = new DBcon();
        public static string name;
        public static string id;
        public static string  userName;


        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void Form2_Load(object sender, EventArgs e)
        {
            //panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
            textBox1.Text = "tasnimakter@gmail.com";
            textBox2.Text = "09876";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            name = textBox1.Text;
            id = textBox2.Text;

            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ResetControl();
        }
        private void ResetControl()
        {
            
            textBox1.Clear();
            textBox2.Clear();


        }
      

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {

                    dbc.Opencon();
                    string query = "select count(*) from RegDbtbl where Email='" + textBox1.Text + "' and password='" + textBox2.Text + "'";
                    SqlCommand command = new SqlCommand(query, dbc.GetCon());

                    int v = (int)command.ExecuteScalar();
                    if (v != 1)
                    {
                        MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("log in succesesed welcome to homepage!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";

                        // loginname = textBox1.Text;
                        userName = textBox1.Text;
                        clrValue();
                        this.Hide();
                        Form4 f4 = new Form4();
                        f4.Show();

                        f4.HideButton(); // C


                    }
                }



            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbc.Closecon();
            }
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }

        }

        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.Icon = Properties.Resources.cross;
                errorProvider1.SetError(this.textBox1, "Enter valid username!");

            }
            else
            {
                errorProvider1.Icon = Properties.Resources.correct;
                //errorProvider1.Clear();
            }
        }

        private void textBox2_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                errorProvider2.Icon = Properties.Resources.cross;
                errorProvider2.SetError(this.textBox2, "Enter valid password!");

            }
            else
            {
                errorProvider2.Icon = Properties.Resources.correct;
                //errorProvider2.Clear();
            }
        }




        private void clrValue()
        {
           
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectRole s = new selectRole();
            s.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }
    }
}
