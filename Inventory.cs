using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IE322_Project
{
    public partial class InventoryManagementStudio : Form
    {
        DataTable inventory = new DataTable();
        public InventoryManagementStudio()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            SKUTextBox.Text = "";
            PriceTextBox.Text = "";
            NameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            QuantityTextBox.Text = "";
            categoryBox.SelectedIndex = -1;


        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Save All the values from our fields into variables 
            String sku = SKUTextBox.Text;
            String name = NameTextBox.Text;
            String description = DescriptionTextBox.Text;   
            String price = PriceTextBox.Text;
            String quantity = QuantityTextBox.Text;
            String category = (string)categoryBox.SelectedItem;

            //add these values to the datatable
            inventory.Rows.Add(sku, name, category, price , description , quantity);

            //clear fields after save
            NewButton_Click(sender, e); 

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                inventory.Rows[inventoryGridView.CurrentCell.RowIndex].Delete();
            }
            catch (Exception err)
            {
                Console.WriteLine("Error:" + err);
            }
        }

        private void inventoryGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SKUTextBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[0].ToString();
                NameTextBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[1].ToString();
                PriceTextBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[3].ToString();
                DescriptionTextBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[4].ToString();
                QuantityTextBox.Text = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[5].ToString();

                String itemToLookFor = inventory.Rows[inventoryGridView.CurrentCell.RowIndex].ItemArray[2].ToString();
                categoryBox.SelectedIndex = categoryBox.Items.IndexOf(itemToLookFor);
            }
            catch (Exception err)
            {
                Console.WriteLine("There has been an error:" + err);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InventoryManagementStudio_Load(object sender, EventArgs e)
        {
            inventory.Columns.Add("SKU");
            inventory.Columns.Add("Name");
            inventory.Columns.Add("Category");
            inventory.Columns.Add("Price");
            inventory.Columns.Add("Description");
            inventory.Columns.Add("Quantity");

            inventoryGridView.DataSource = inventory;
        }
    }
}
