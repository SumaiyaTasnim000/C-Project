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
    public partial class Form7 : Form
    {
        DBcon dbc = new DBcon();
        double GrandTotal = 0.0;
        //int n = 0;
        public Form7()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            BindCategory();
            BindBillList();
            lblDate.Text = DateTime.Now.ToShortDateString();
        }
        private void SearchedProductList()
        {

            try
            {

                SqlCommand cmd = new SqlCommand("getProductList_SearchByCat ", dbc.GetCon());
                cmd.Parameters.AddWithValue("@ProdCatID", cmbCategory.SelectedValue);
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
        private void BindCategory()
        
        {
            try
            {
                SqlCommand cmd = new SqlCommand("getcategory ", dbc.GetCon());
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefCat_Click(object sender, EventArgs e)
        {
            SearchedProductList();
        }

        private void dataGridView2_Product_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void dataGridView2_Product_Click(object sender, EventArgs e)
        {
            try
            {
                txtProdID.Clear();
                txtProdID.Text = dataGridView2_Product.SelectedRows[0].Cells[0].Value.ToString();
                txtProductName.Clear();
                txtProductName.Text = dataGridView2_Product.SelectedRows[0].Cells[1].Value.ToString();
                //cmbCategory.SelectedValue = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtPrice.Clear();
                txtPrice.Text = dataGridView2_Product.SelectedRows[0].Cells[4].Value.ToString();
            
                // txtQty.Text = dataGridView2_Product.SelectedRows[0].Cells[5].Value.ToString();
               
                txtQty.Clear();
                txtQty.Focus();
                pictureBox2.Image = Properties.Resources.no_image_avaiable;
                pictureBox2.Image = GetPhoto((byte[])dataGridView2_Product.SelectedRows[0].Cells[6].Value);
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

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPrice.Text == "" || txtQty.Text == "") { MessageBox.Show("Enter Valid Quantity or Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                double total = Convert.ToDouble(txtPrice.Text) * Convert.ToInt32(txtQty.Text);
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.RowTemplate.Height = 150;

                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView2);
                //addRow.Cells[0].Value = ++n;
                addRow.Cells[0].Value = txtProductName.Text;
                addRow.Cells[1].Value = txtPrice.Text;
                addRow.Cells[2].Value = txtQty.Text;
                if (pictureBox2.Image != null)
                {              
                    Image image = new Bitmap(pictureBox2.Image);
                    addRow.Cells[3].Value = image;
                }
                else
                {
                    addRow.Cells[3].Value = Properties.Resources.no_image_avaiable;
                }
              
                addRow.Cells[4].Value = total;
              

                dataGridView2.Rows.Add(addRow);
                GrandTotal += total;
                lblGrandTotal.Text = GrandTotal + " Bdt";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_Product_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnAddBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBillNo.Text == "")
                { MessageBox.Show("enter bill no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else
                {

                    SqlCommand cmd = new SqlCommand("InsertBilllist ", dbc.GetCon());
                    cmd.Parameters.AddWithValue("@BillId", txtBillNo.Text);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@SellingDate", lblDate.Text);
                    cmd.Parameters.AddWithValue("@TotalAmmount", Convert.ToDouble(txtQty.Text));

                    cmd.CommandType = CommandType.StoredProcedure;
                    dbc.Opencon();
                    int i = cmd.ExecuteNonQuery();
                    //dbc.Opencon();
                    if (i > 0)
                    {
                        BindBillList();
                        MessageBox.Show("BILL ADDED successfully.....", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BindBillList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetBillListitem ", dbc.GetCon());
                cmd.CommandType = CommandType.StoredProcedure;
                dbc.Opencon();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dbc.Closecon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
