using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Pet___Beyonds
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Admin";
            textBox2.Text = "121314";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Please Enter Username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox1.Text == "Admin" && textBox2.Text == "121314")
                {
                    MessageBox.Show("log in succesesed welcome to homepage!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form4 f4 = new Form4();
                    f4.Show();
                    this.Hide();
                }
                else { MessageBox.Show("invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetControl();
           

        }

        private void ResetControl()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectRole selectRole   = new selectRole();
            selectRole.Show();
            this.Hide();
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
    }
}
