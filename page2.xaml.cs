using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaction logic for page2.xaml
    /// </summary>
    public partial class page2 : Window
    {
        string connectionString = "Data Source=.; Initial Catalog=Inventory; User ID=sa; Password=FIATS@2024";
        int selectedNameId = 0;
        string nameProduct = "";

        string name;
        public page2()
        {
            InitializeComponent();
            view_data();
        }

        private void view_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (view.SelectedItem != null)
            {
                DataRowView row = (DataRowView)view.SelectedItem;
                selectedNameId =Convert.ToInt32(row["Product_id"]);
                nameProduct = row["Product_name"].ToString();
                

            }
        }
        public void view_data()
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from logins", sqlConnection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                view.ItemsSource = table.DefaultView;
            }
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand("delete from Product where Product_id = @name_id", sqlConnection))
                    {
                        command.Parameters.AddWithValue("@name_id", selectedNameId);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO sales_prise (Product_id) VALUES (@name)", sqlConnection))
                    {
                        command1.Parameters.AddWithValue("@name", selectedNameId);
                        command1.ExecuteReader();
                        view_data();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ: " + ex.Message);
                }
            }
        }

        private void end_Click(object sender, RoutedEventArgs e)
        {

        }

        private void view_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
