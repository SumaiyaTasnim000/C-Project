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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pet___Beyonds
{
    public partial class Form6 : Form
    {
        DBcon dbc = new DBcon();
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            BindCategory();
            BindProductList();
            lblProductID.Visible = false;
            btnProdUpdate.Visible = false;
            btnProdDelete.Visible = false;

            btnProdAdd.Visible = true;
            Searchby_Category();
        }

        private void BindCategory()
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
        private void Searchby_Category()
        {
            SqlCommand cmd = new SqlCommand("getcategory ", dbc.GetCon());
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            //ofd.Filter = "PNG FILE (*.PNG) | *.PNG";
            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(ofd.FileName);
            }
        }

        private void btnProdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProdName.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter product name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProdName.Focus();
                    return;
                }
                else if (txtPrice.Text == String.Empty || Convert.ToInt32(txtPrice.Text) < 0)
                {
                    MessageBox.Show("Please Enter valid Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                else if (txtQty.Text == String.Empty || Convert.ToInt32(txtQty.Text) < 0)
                {
                    MessageBox.Show("Please Enter valid Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQty.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("spcheckDuplicateProduct ", dbc.GetCon());
                    cmd.Parameters.AddWithValue("@ProdName", txtProdName.Text);
                    cmd.Parameters.AddWithValue("@ProdCatID", cmbCategory.SelectedValue);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dbc.Opencon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {

                        MessageBox.Show("Product Name already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {

                        cmd = new SqlCommand("InsertProduct ", dbc.GetCon());
                        cmd.Parameters.AddWithValue("@ProdName", txtProdName.Text);
                        cmd.Parameters.AddWithValue("@ProdCatID", cmbCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@ProdPrice", Convert.ToDecimal(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@ProdQty", Convert.ToInt32(txtQty.Text));
                        cmd.Parameters.AddWithValue("@Picture", SavePhoto());

                        cmd.CommandType = CommandType.StoredProcedure;

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {

                            MessageBox.Show("Product inserted successfully.....", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindProductList();
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

        private byte[] SavePhoto()
        {

            MemoryStream ms = new MemoryStream();
            pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void BindProductList()
        {

            try
            {
                SqlCommand cmd = new SqlCommand("getProductList ", dbc.GetCon());
                cmd.CommandType = CommandType.StoredProcedure;
                dbc.Opencon();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView1.Columns[6];
                dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.RowTemplate.Height = 100;

                dbc.Closecon();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtClear()
        {
            txtProdName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            pictureBox2.Image = Properties.Resources.no_image_avaiable;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                btnProdUpdate.Visible = true;
                btnProdDelete.Visible = true;
                lblProductID.Visible = true;
                btnProdAdd.Visible = false;

                lblProductID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtProdName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                cmbCategory.SelectedValue = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtPrice.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtQty.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                pictureBox2.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[6].Value);
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

        private void btnProdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblProductID.Text == "" || txtProdName.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter product ID and Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProdName.Focus();
                    return;
                }
                else if (txtPrice.Text == String.Empty || Convert.ToDecimal(txtPrice.Text) < 0)
                {
                    MessageBox.Show("Please Enter valid Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                else if (txtQty.Text == String.Empty || Convert.ToInt32(txtQty.Text) < 0)
                {
                    MessageBox.Show("Please Enter valid Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQty.Focus();
                    return;
                }
                else
                {
                    //SqlCommand cmd = new SqlCommand("spcheckDuplicateProduct ", dbCon.GetCon());
                    //cmd.Parameters.AddWithValue("@ProdName", txtProdName.Text);
                    //cmd.Parameters.AddWithValue("@ProdCatID", cmbCategory.SelectedValue);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //dbCon.OpenCon();
                    //var result = cmd.ExecuteScalar();
                    //if (result != null)
                    //{

                    //    MessageBox.Show("Product Name already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtClear();
                    //}
                    //else
                    //{


                    //}
                    SqlCommand cmd = new SqlCommand("UpdateProduct ", dbc.GetCon());
                    cmd.Parameters.AddWithValue("@ProdName", txtProdName.Text);
                    cmd.Parameters.AddWithValue("@ProdCatID", cmbCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProdPrice", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@ProdQty", Convert.ToInt32(txtQty.Text));
                    cmd.Parameters.AddWithValue("@ProdID", Convert.ToInt32(lblProductID.Text));
                    cmd.Parameters.AddWithValue("@Picture", SavePhoto());

                    cmd.CommandType = CommandType.StoredProcedure;
                    dbc.Opencon();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {

                        MessageBox.Show("Product Updated successfully.....", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClear();
                        BindProductList();
                        btnProdUpdate.Visible = false;
                        btnProdDelete.Visible = false;
                        lblProductID.Visible = false;
                        btnProdAdd.Visible = true;
                    }
                    else { MessageBox.Show("Updation failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    dbc.Closecon();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProdDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (lblProductID.Text == String.Empty)
                {
                    MessageBox.Show("Please Select product ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                if (lblProductID.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete?", "confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("DeleteProduct ", dbc.GetCon());
                        cmd.Parameters.AddWithValue("@ProdID", Convert.ToInt32(lblProductID.Text));
                        dbc.Opencon();
                        cmd.CommandType = CommandType.StoredProcedure;

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {

                            MessageBox.Show("Product deleted successfully.....", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindProductList();
                            btnProdUpdate.Visible = false;
                            btnProdDelete.Visible = false;
                            lblProductID.Visible = false;
                            btnProdAdd.Visible = true;
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

        private void cmbsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
            SearchedProductList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BindProductList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
