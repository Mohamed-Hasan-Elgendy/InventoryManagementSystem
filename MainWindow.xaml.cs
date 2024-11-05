using System;
using System.Data.SqlClient;
using System.Windows;

namespace InventoryManagementSystem
{
    public partial class MainWindow : Window
    {
        string connectionstring = "Data Source=.;Initial Catalog=Inventory ;User ID=sa;Password=FIATS@2024;";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = text1.Text.ToLower();
            string password = text2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // تحقق من بيانات العملاء
                    string queryCustomers = "SELECT COUNT(*) FROM Customers WHERE customer_password = @customer_password AND customer_name = @customer_name";
                    using (SqlCommand cmd = new SqlCommand(queryCustomers, conn))
                    {
                        cmd.Parameters.AddWithValue("@customer_name", username);
                        cmd.Parameters.AddWithValue("@customer_password", password);

                        int customerCount = (int)cmd.ExecuteScalar();

                        if (customerCount > 0)
                        {
                            page2 customerPage = new page2();
                            customerPage.Show();
                            this.Close();
                            return; // إنهاء الدالة بعد تغيير الصفحة
                        }
                    }

                    // تحقق من بيانات الموردين
                    string querySuppliers = "SELECT COUNT(*) FROM Suppliers WHERE Supplier_password = @Supplier_password AND Supplier_name = @Supplier_name";
                    using (SqlCommand cmd1 = new SqlCommand(querySuppliers, conn))
                    {
                        cmd1.Parameters.AddWithValue("@Supplier_name", username);
                        cmd1.Parameters.AddWithValue("@Supplier_password", password);

                        int supplierCount = (int)cmd1.ExecuteScalar();

                        if (supplierCount > 0)
                        {
                            page3 supplierPage = new page3();
                            supplierPage.Show();
                            this.Close();
                            return; // إنهاء الدالة بعد تغيير الصفحة
                        }
                    }

                    // إذا لم يتم العثور على أي مطابقة
                    MessageBox.Show("Invalid Username and Password. Please try again.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}