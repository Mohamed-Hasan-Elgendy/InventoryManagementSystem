using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls.Primitives;

namespace InventoryManagementSystem
{
    public partial class page3 : Window
    {
        string connectionString = "Data Source=.; Initial Catalog=Inventory; User ID=sa; Password=FIATS@2024";
        int selectedNameId = 0;

        public page3()
        {
            InitializeComponent();
            update();
        }

        public void update()
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Product", sqlConnection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                read.ItemsSource = table.DefaultView;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Product  (Product_name) VALUES (@name)", sqlConnection))
                    {
                        command.Parameters.AddWithValue("@name", tx.Text);
                        command.ExecuteReader();
                    }
                    update();
                    tx.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ: " + ex.Message);
                }
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand("delete from Product where Product_id = @Product_id", sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Product_id", selectedNameId);
                        command.ExecuteNonQuery();
                    }
                    update();
                    tx.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ: " + ex.Message);
                }
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Product SET Product_name = @Product_name WHERE Product_id = @Product_id", sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Product_id", selectedNameId);
                        command.Parameters.AddWithValue("@Product_name", tx.Text);
                        command.ExecuteNonQuery();
                    }
                    update();
                    tx.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ: " + ex.Message);
                }
            }
        }

        private void read_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (read.SelectedItem != null)
            {
                DataRowView row = (DataRowView)read.SelectedItem;
                selectedNameId = Convert.ToInt32(row["Product_id"]);
                tx.Text = row["Product_name"].ToString();


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM order_list", connection);

                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(result.ToString(), "Data from Database");
                }
                else
                {
                    MessageBox.Show("No data found.", "Information");
                }

            }
        }
    }
}
    