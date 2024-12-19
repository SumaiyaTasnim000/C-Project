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
    public partial class Form8 : Form
    {
        DBcon dbc = new DBcon();
        public Form8()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            lblCatID.Visible = false;
            BindCategory();
        }

        private void btnAddcat_Click(object sender, EventArgs e)
        {
            if (textCatname.Text == String.Empty)
            {
                MessageBox.Show("Please Enter category name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textCatname.Focus();
                return;
            }
            else if (rtbCatDesc.Text == String.Empty)
            {
                MessageBox.Show("Please Enter category description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbCatDesc.Focus();
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select CategoryName from tblCategory where CategoryName= @CategoryName ", dbc.GetCon());
                cmd.Parameters.AddWithValue("@CategoryName", textCatname.Text);
                dbc.Opencon();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {

                    MessageBox.Show("CategoryName {0} already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClear();
                }
                else
                {

                    cmd = new SqlCommand("SppCatInsert ", dbc.GetCon());
                    cmd.Parameters.AddWithValue("@CategoryName", textCatname.Text);
                    cmd.Parameters.AddWithValue("@CategoryDesc", rtbCatDesc.Text);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {

                        MessageBox.Show("Category inserted successfully.....", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClear();
                        BindCategory();
                    }
                }
                dbc.Closecon();

            }
        }

        private void BindCategory()
        {
            SqlCommand cmd = new SqlCommand("select CatID as CategoryID, CategoryName, CategoryDesc as CategoryDescription  from tblCategory ", dbc.GetCon());

            dbc.Opencon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dbc.Closecon();
        }

        private void txtClear()
        {
            textCatname.Clear(); 
            rtbCatDesc.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            lblCatID.Visible = true;
            btnAddcat.Visible = false;

            lblCatID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textCatname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            rtbCatDesc.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblCatID.Text == String.Empty)
                {
                    MessageBox.Show("Please Select category ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textCatname.Focus();
                    return;
                }
                if (textCatname.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter category name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textCatname.Focus();
                    return;
                }
                else if (rtbCatDesc.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter category description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rtbCatDesc.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select CategoryName from tblCategory where CategoryName= @CategoryName ", dbc.GetCon());
                    cmd.Parameters.AddWithValue("@CategoryName", textCatname.Text);
                    dbc.Opencon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {

                        MessageBox.Show("CategoryName already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {

                        cmd = new SqlCommand("SppCatUpdate ", dbc.GetCon());
                        cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(lblCatID.Text));
                        cmd.Parameters.AddWithValue("@CategoryName", textCatname.Text);
                        cmd.Parameters.AddWithValue("@CategoryDesc", rtbCatDesc.Text);
                        cmd.CommandType = CommandType.StoredProcedure;

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {

                            MessageBox.Show("Category updated successfully.....", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindCategory();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            btnAddcat.Visible = true;
                            lblCatID.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Update failed.....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                        }
                    }
                    dbc.Closecon();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (lblCatID.Text == String.Empty)
                {
                    MessageBox.Show("Please Select category ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textCatname.Focus();
                    return;
                }
                if (lblCatID.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete?", "confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("SppCatDelete ", dbc.GetCon());
                        cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(lblCatID.Text));
                        dbc.Opencon();
                        cmd.CommandType = CommandType.StoredProcedure;

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {

                            MessageBox.Show("Category deleted successfully.....", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindCategory();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            btnAddcat.Visible = true;
                            lblCatID.Visible = false;
                        }
                    }

                    else
                    {
                        MessageBox.Show("Delete failed.....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtClear();
                    }
                }
                dbc.Closecon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtClear();
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnAddcat.Visible = true;
            lblCatID.Visible = false;
        }
    }
}
