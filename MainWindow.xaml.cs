//Koodannut ja testannut toimivaksi 6.3.2014 EsaSalmik
using JAMK.ICT.Data;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace JAMK.ICT.ADOBlanco
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

        private DataTable dataTable;
        private DataView dataView;

    public MainWindow()
    {
      InitializeComponent();
      IniMyStuff();
    }

    private void IniMyStuff()
    {
            //TODO täytetään combobox asiakkaitten maitten nimillä
            //esimerkki kuinka App.Configissa oleva connectionstring luetaan
            /* string viesti;
             try { 
                 cbCountries.ItemsSource = DBPlacebo.GetCountriesFromSQLServer(Properties.Settings.Default.Tietokanta, Properties.Settings.Default.Taulu, out viesti).DefaultView;
                 cbCountries.DisplayMemberPath = "city";
                 cbCountries.SelectedValuePath = "city";
             }
             catch (Exception ex)
             {
                 viesti = ex.Message;
             }
             lbMessages.Content = viesti;*/
            GetData();
            FillCityComboBox();
    }

        private void GetData()
        {
            string viesti;
            try
            {
                dataTable = DBPlacebo.GetAllCustomersFromSQLServer(Properties.Settings.Default.Tietokanta, Properties.Settings.Default.Taulu, out viesti);
            }
            catch (Exception ex)
            {
                viesti = ex.Message;
            }
            dataView = new DataView();
            dataView = dataTable.DefaultView;
            dgCustomers.DataContext = dataView;
        }

        private void FillCityComboBox()
        {
            var rows = dataTable.Rows;
            List<string> list = new List<string>();
            foreach(DataRow row in rows)
            {
                if (!list.Contains(row["city"].ToString()))
                {
                    list.Add(row["city"].ToString());
                }
            }
            cbCountries.ItemsSource = list;

            //cbCountries.DataContext = dataTable;
            //cbCountries.DisplayMemberPath = "[city]";
            //cbCountries.SelectedValuePath = "[city]";

        }

    private void btnGet3_Click(object sender, RoutedEventArgs e)
    {
            dgCustomers.ItemsSource = DBPlacebo.GetTestCustomers().DefaultView;
    }

    private void btnGetAll_Click(object sender, RoutedEventArgs e)
    {
            string viesti = "";
            try {
                dgCustomers.ItemsSource = DBPlacebo.GetAllCustomersFromSQLServer(Properties.Settings.Default.Tietokanta, Properties.Settings.Default.Taulu, out viesti).DefaultView;
            }
            catch (Exception ex)
            {
                viesti = ex.Message;
            }
            lbMessages.Content = viesti;
    }

    private void btnGetFrom_Click(object sender, RoutedEventArgs e)
    {
      //TODO
    }

        private void btnYield_Click(object sender, RoutedEventArgs e)
        {
            JAMK.ICT.JSON.JSONPlacebo2015 roskaa = new JAMK.ICT.JSON.JSONPlacebo2015();
            MessageBox.Show(roskaa.Yield());
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Console.WriteLine(cbCountries.SelectedValue);
            Console.WriteLine(cbCountries.SelectedValue);
            dataView.RowFilter = string.Format("city = '{0}'", cbCountries.SelectedValue);
        }
    }
}
