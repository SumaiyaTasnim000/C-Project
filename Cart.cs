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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Pet___Beyonds
{
    public partial class Cart : Form
    {
        
        DBcon dbc = new DBcon();
        double GrandTotal = 0.0;
        public Cart()
        {
            InitializeComponent();

        }

        private void Cart_Load(object sender, EventArgs e)
        {
            BindBillList();
           
        }

        public void BindBillList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GettblCartProduct ", dbc.GetCon());
                cmd.CommandType = CommandType.StoredProcedure;
                dbc.Opencon();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                
                //dbc.Closecon();
                //double total = Convert.ToDouble(txtPrice.Text) * Convert.ToInt32(textBox2.Text);
                ////GrandTotal += total;
                ////lblGrandTotal.Text = GrandTotal + " Bdt";
                dbc.Closecon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                        int prodIDToDelete = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells[0].Value);

                        SqlCommand cmd = new SqlCommand("DeleteCartProduct", dbc.GetCon());
                        cmd.Parameters.AddWithValue("@ProdID", prodIDToDelete);
                        dbc.Opencon();
                        cmd.CommandType = CommandType.StoredProcedure;

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Product deleted successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            BindBillList();
                           
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select a product that you want to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (name.Text == "" || phone.Text  == "" || loaction.Text == "")
                {

                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Oder has been taken successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    name.Text = "";
                    phone.Text = "";
                    loaction.Text= "";

                    // loginname = textBox1.Text;
                    //userName = textBox1.Text;
                    clrValue();



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

        private void clrValue()
        {
            name.Clear();
            phone.Clear();
            loaction.Clear();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    txtProdID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    txtPrice.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    textBox2.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            void UpdateTotal(double a)
            {
                double total = 0.0;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    double price = Convert.ToDouble(row.Cells["PriceColumn"].Value); // Replace "PriceColumn" with the actual name of the column containing the item prices
                    int quantity = Convert.ToInt32(row.Cells["QuantityColumn"].Value); // Replace "QuantityColumn" with the actual name of the column containing the item quantities
                    total += price * quantity;
                }

                GrandTotal = total;
                lblGrandTotal.Text = $"Total: {GrandTotal} Bdt";
              
            }
            try
            {
                // ... Your existing code to populate dataGridView1

                UpdateTotal(a); // Call the method to calculate and update the total bill
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clrValue();
        }
    }

   
   

}
//dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//dataGridView1.RowTemplate.Height = 150;

//DataGridViewRow addRow = new DataGridViewRow();
//addRow.CreateCells(dataGridView1);

//addRow.Cells[0].Value = productName;
//addRow.Cells[1].Value = price;
//addRow.Cells[2].Value = quantity;

//if (image != null)
//{
//    Image imageCopy = new Bitmap(image);
//    addRow.Cells[3].Value = imageCopy;
//}
//else
//{
//    addRow.Cells[3].Value = Properties.Resources.no_image_avaiable;
//}

//addRow.Cells[4].Value = total;

//dataGridView1.Rows.Add(addRow);
