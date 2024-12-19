using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pet___Beyonds
{
    public partial class Form10 : Form
    {
        DBcon dbc = new DBcon();
        public Form10()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                    }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.Show();
            f4.HideButton();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchedProductList();
        }

        private void SearchedProductList()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("getProductList_SearchByCat ", dbc.GetCon());
                cmd.Parameters.AddWithValue("@ProdCatID", cmbsearch.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                dbc.Opencon();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2_Product.DataSource = dt;

                dbc.Closecon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            Searchby_Category();
            BindCategory();
        }

        private void BindCategory()
        {
            SqlCommand cmd = new SqlCommand("getcategoryForAcc ", dbc.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;

            dbc.Opencon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CatID";


            dbc.Closecon();
        }

        private void Searchby_Category()
        {
            SqlCommand cmd = new SqlCommand("getcategoryForAcc ", dbc.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;

            dbc.Opencon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbsearch.DataSource = dt;
            cmbsearch.DisplayMember = "CategoryName";
            cmbsearch.ValueMember = "CatID";


            dbc.Closecon();
        }

        private void dataGridView2_Product_Click(object sender, EventArgs e)
        {
            try
            {
                txtProdID.Clear();
                txtProdID.Text = dataGridView2_Product.SelectedRows[0].Cells[0].Value.ToString();
                textBox1.Clear();
                textBox1.Text = dataGridView2_Product.SelectedRows[0].Cells[1].Value.ToString();

                cmbCategory.SelectedValue = dataGridView2_Product.SelectedRows[0].Cells[3].Value.ToString();
                txtPrice.Clear();
                txtPrice.Text = dataGridView2_Product.SelectedRows[0].Cells[4].Value.ToString();

                // txtQty.Text = dataGridView2_Product.SelectedRows[0].Cells[5].Value.ToString();

                txtQty.Clear();
                txtQty.Focus();
                pictureBox4.Image = Properties.Resources.no_image_avaiable;
                pictureBox4.Image = GetPhoto((byte[])dataGridView2_Product.SelectedRows[0].Cells[6].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }
        private void txtClear()
        {

            textBox1.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            pictureBox4.Image = Properties.Resources.no_image_avaiable;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cart c = new Cart();
            c.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPrice.Text == "" || txtQty.Text == "")
                {
                    MessageBox.Show("Enter Valid Quantity or Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                else
                {
                    //double total = Convert.ToDouble(txtPrice.Text) * Convert.ToInt32(txtQty.Text);
                    SqlCommand cmd = new SqlCommand("InsertCartProduct ", dbc.GetCon());
                    cmd.Parameters.AddWithValue("@ProdID", txtProdID.Text);
                    cmd.Parameters.AddWithValue("@ProductName", textBox1.Text);
                    cmd.Parameters.AddWithValue("@ProdPrice", Convert.ToDecimal(txtPrice.Text));

                    cmd.Parameters.AddWithValue("@Quantity", Convert.ToDouble(txtQty.Text));
                    cmd.Parameters.AddWithValue("@Picture", SavePhoto());
                    cmd.Parameters.AddWithValue("@Total", Convert.ToDouble(txtPrice.Text) * Convert.ToInt32(txtQty.Text));

                    cmd.CommandType = CommandType.StoredProcedure;
                    dbc.Opencon();
                    int i = cmd.ExecuteNonQuery();
                    //dbc.Opencon();
                    if (i > 0)
                    {
                        Cart cartForm = new Cart();
                        cartForm.BindBillList();
                        MessageBox.Show("product ADDED successfully.....", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtClear();

                    }
                    dbc.Closecon();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox4.Image.Save(ms, pictureBox4.Image.RawFormat);
            return ms.GetBuffer();
        }
    }
}
