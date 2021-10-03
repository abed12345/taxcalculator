using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace taxcalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ItemsDataGrid.ItemsSource = Items;
        }

        public class Item
        {
            public decimal itemPrice { get; set; }
            public string itemDesc { get; set; }
            public string importTax { get; set; }
        }

        private List<Item> Items = new List<Item>();

        private void AddNewItem(decimal itemPrice, string itemDesc, string importTax)
        {
            Item item = new Item()
            {
                itemPrice = itemPrice,
                itemDesc = itemDesc,
                importTax = importTax
            };
            Items.Add(item);
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal itemPrice = Decimal.Parse(ItemPriceInput.Text);
                double importTaxRate = .05;
                string itemDesc = itemName.Text;
                string importTax = importated.Text.ToUpper();
                if (importTax == "YES")
                    itemPrice = itemPrice * (decimal)importTaxRate + itemPrice;
                else
                    itemPrice = itemPrice;
                AddNewItem(itemPrice, itemDesc, importTax);
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid Item description and price as well as import status");
            }

            ItemPriceInput.Clear();
            itemName.Clear();
            importated.Clear();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            decimal subtotal = 0;
            decimal total = 0;
            decimal taxAmount = 0;
            double taxRate = .10;

            foreach (var item in Items)
            {
                subtotal += item.itemPrice;
                taxAmount += Math.Round((item.itemPrice * (decimal)taxRate) * 2, MidpointRounding.AwayFromZero)/2;
                total += subtotal + taxAmount;
            }

            Subtotal.Text = String.Format("{0:C}", subtotal);
            TaxAmount.Text = String.Format("{0:C}", taxAmount);
            TotalPrice.Text = String.Format("{0:C}", total);
        }
    }
}
