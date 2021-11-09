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

namespace _13_BankingSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Client> clients = new List<Client>();
        static public Client CurrentClient;
        public static string[] arrayMonth = new string[12] { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditClientWindow addClientWindow=CreateWindowClient();


        }

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            CreateWindowClient();
        }

        private AddOrEditClientWindow CreateWindowClient()
        {
            AddOrEditClientWindow addClientWindow = new AddOrEditClientWindow();
            string dayNow = DateTime.Now.Day.ToString();
            addClientWindow.CountDayBirth.Text = dayNow;
            addClientWindow.CountDayStartDeposit.Text = dayNow;
            addClientWindow.CountDayEndDeposit.Text = dayNow;

            int monthNow = DateTime.Now.Month;
            addClientWindow.MonthBirthday.SelectedItem = arrayMonth[monthNow - 1];
            addClientWindow.MonthStartDeposit.SelectedItem = arrayMonth[monthNow - 1];
            addClientWindow.MonthEndDeposit.SelectedItem = arrayMonth[monthNow - 1];

            string yearNow = DateTime.Now.Year.ToString();
            addClientWindow.YearBirthday.SelectedItem = yearNow;
            addClientWindow.YearStartDeposit.SelectedItem = yearNow;
            addClientWindow.YearEndDeposit.SelectedItem = yearNow;
            addClientWindow.Owner = this;
            addClientWindow.Show();
            return addClientWindow;
        }

        private void ListName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
