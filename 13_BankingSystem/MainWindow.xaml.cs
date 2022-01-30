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
        public static List<Client> Clients = new List<Client>();
        static public Client CurrentClient;
        private int _numberCurrentClient;
        public static string[] arrayMonth = new string[12] { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };

        public MainWindow()
        {
            InitializeComponent();
            ListName.ItemsSource = Clients;
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
            Refresh(addClientWindow);    
            return addClientWindow;
        }
        private void Refresh(Window window)
        {
            window.Closed += (s, eventarg) =>
            {
                ListName.Items.Refresh();
            }; //метод который рефрешет родителя после закрытия чилда

        }
        private void ListName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListName.SelectedItem is Person)
            {
                Person person = ListName.SelectedItem as Person;
                DateOfBirth.Text = person.DateBirth.ToShortDateString();
                Name.Text = $"{person.NameClient}";
                CurrentClient = person;
            }
            
            else
            {
                LegalBody legalBody = ListName.SelectedItem as LegalBody;
                DateOfBirth.Text = "-";
                Name.Text = $"{legalBody.NameClient}";
                CurrentClient = legalBody;
            }
            
            StartDeposit.Text = CurrentClient.AmountNow.ToString();
            DateOfStartDeposit.Text = CurrentClient.DateOfStartDeposit.ToShortDateString();
            CountMonth.Text = CurrentClient.CountMonth.ToString();
            BankRate.Text = $"{CurrentClient.Percent.ToString()}% годовых";
            AmountNow.Text = CurrentClient.AmountNow.ToString();

            if (CurrentClient.IsCapitalization == false) AccrualOfInterest.Text = "без капитализации";
            else AccrualOfInterest.Text = "с капитализацией";
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            TransferWindow TW = TransferWindow();
            //тут будет открываться окно с реализацией переводов
        }
        private TransferWindow TransferWindow()
        {
            TransferWindow TW = new TransferWindow();
            TW.Owner = this;
            TW.Show();
            Refresh(TW);
            return TW;
        }

        private void SearchString_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchString.Text = "";
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e) //доделать эту хрень
        {
            string searchText = SearchString.Text;
            bool isFindClient = false;
            if (double.TryParse(searchText, out double deposit))
            {
                for (int i = 0; i < Clients.Count; i++)
                {
                    if (Clients[i]==CurrentClient)
                    {
                        isFindClient = true;
                    }
                    if (Clients[i] != CurrentClient && isFindClient)
                    {
                        if (Clients[i].StartDeposit == deposit)
                        {
                            ListName.SelectedItem = ListName.Items[i];
                            CurrentClient = ListName.Items[i] as Client;
                            return;
                        }
                    }
                }
            }
            //else
            //{

            //    for (int i = 0; i < Clients.Count; i++)
            //    {
            //        if (Clients[i] == CurrentClient)
            //        {
            //            isFindClient = true;
            //        }
            //        if (Clients[i] != CurrentClient && isFindClient)
            //        {
            //            if (Clients[i].FirstName == searchText)
            //            {
            //                ListName.SelectedItem = ListName.Items[i];
            //                CurrentClient = ListName.Items[i] as Client;
            //                return;
            //            }
            //            else if (Clients[i].LastName == searchText)
            //            {
            //                ListName.SelectedItem = ListName.Items[i];
            //                CurrentClient = ListName.Items[i] as Client;
            //                return;
            //            }
            //            else if (Clients[i].Patronymic == searchText)
            //            {
            //                ListName.SelectedItem = ListName.Items[i];
            //                CurrentClient = ListName.Items[i] as Client;
            //                return;
            //            }
            //        }
            //    }
            //}
        }
    }
}
