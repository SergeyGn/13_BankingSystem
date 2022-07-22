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
    public partial class MainWindow : Window
    {
        public static List<Client> Clients = new List<Client>();
        private string _warningMessage = "Выберите пользователя";
        private string _warningDeleteMessage = "Удалён пользователь ";
        private string _warningFindEnd = "Поиск окончен";
        private string _warningNotClient = "Нет пользователей!";
        public static  Client CurrentClient;
        public static  bool IsEdit;
        public static  int NumberCurrentClient;
        public static string[] arrayMonth = new string[12] { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };


        public static Messages MsgsHistory = new Messages();
   
         public static string name = Environment.UserName;
        public MainWindow()
        {
            InitializeComponent();
            MsgsHistory.MessageText = new List<string>();
            ListName.ItemsSource = Clients;
            if (Clients.Count != 0)
            {
                CurrentClient = Clients[0];
                ListName.SelectedItem = ListName.Items[0];
            }
        }
        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            IsEdit = false;
            AddOrEditClientWindow addClientWindow=CreateWindowClient();
        }

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            IsEdit = true;
            if (CurrentClient != null)
            {
                NumberCurrentClient = ListName.SelectedIndex;
                AddOrEditClientWindow editClientWindow = new AddOrEditClientWindow();
                if (CurrentClient is Person)
                {
                    Person person = CurrentClient as Person;
                    
                    editClientWindow.FirstName.Text = person.FirstName;
                    editClientWindow.LastName.Text = person.LastName;
                    editClientWindow.Patronymic.Text = person.Patronymic;
                    editClientWindow.CountDayBirth.Text = person.DateBirth.Day.ToString();
                    editClientWindow.MonthBirthday.SelectedItem = arrayMonth[person.DateBirth.Month - 1];
                    editClientWindow.YearBirthday.SelectedItem = person.DateBirth.Year;

                    editClientWindow.RadioButtonClient.IsChecked = true;
                    
                }
                else if (CurrentClient is LegalBody)
                {
                    editClientWindow.NameCompany.Text = CurrentClient.NameClient;

                    editClientWindow.RadioButtonLegalBody.IsChecked = true;
                }

                editClientWindow.VIPclient.IsChecked = CurrentClient.IsVIP;

                editClientWindow.CountDayStartDeposit.Text = CurrentClient.DateOfStartDeposit.Day.ToString();
                editClientWindow.CountDayEndDeposit.Text = CurrentClient.DateOfEndDeposit.Day.ToString();

                editClientWindow.MonthStartDeposit.SelectedItem = arrayMonth[CurrentClient.DateOfStartDeposit.Month-1];
                editClientWindow.MonthEndDeposit.SelectedItem = arrayMonth[CurrentClient.DateOfEndDeposit.Month - 1];

                editClientWindow.Deposit.Text = CurrentClient.AmountNow.ToString();

                editClientWindow.Owner = this;
                editClientWindow.Show();
                Refresh(editClientWindow);
            }
            else
            {
                MessageBox.Show(_warningMessage, "Edit", MessageBoxButton.OK);
            }
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
                if (Clients.Count != 0)
                {
                    CurrentClient = Clients[0];
                    ListName.SelectedItem = ListName.Items[0];
                    
                }
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
            
            else if(ListName.SelectedItem is LegalBody)
            {
                LegalBody legalBody = ListName.SelectedItem as LegalBody;
                DateOfBirth.Text = "-";
                Name.Text = $"{legalBody.NameClient}";
                CurrentClient = legalBody;
            }

            NumberCurrentClient = ListName.SelectedIndex;

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
            if (CheckClients())
            {
                TransferWindow TW = GetTransferWindow();
            }
        }

        
        private TransferWindow GetTransferWindow()
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckClients())
            {
                string searchText = SearchString.Text;
                bool isFindClient = false;
                if (double.TryParse(searchText, out double deposit))
                {
                    for (int i = 0; i < Clients.Count; i++)
                    {
                        if (Clients[i] == CurrentClient)
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
                else
                {
                    var client = CurrentClient;
                    for (int i = 0; i < Clients.Count; i++)
                    {

                        if (Clients[i] == CurrentClient)
                        {
                            isFindClient = true;
                        }

                        if (Clients[i] != CurrentClient && isFindClient)
                        {
                            if (Clients[i].NameClient.Contains(searchText))
                            {
                                ListName.SelectedItem = ListName.Items[i];
                                CurrentClient = ListName.Items[i] as Client;
                                return;
                            }
                        }

                    }
                    if (client == CurrentClient)
                    {
                        if (Clients.Count > 0)
                        {
                            CurrentClient = Clients[0];
                            MessageBox.Show(_warningFindEnd, "", MessageBoxButton.OK);
                        }
                    }
                }

            }
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (CheckClients())
            {
                Clients.Remove(ListName.SelectedItem as Client);
                MessageBox.Show(_warningDeleteMessage + CurrentClient.NameClient, "Delete", MessageBoxButton.OK);
                string msg = $"Счёт {CurrentClient.NameClient} закрыт [{DateTime.Now.ToShortTimeString()} {name}]";
                MsgsHistory.Added(msg);
                ListName.Items.Refresh();
            }
        }

        private bool CheckClients()
        {
            bool isClients;
            if (Clients.Count > 0)
            {
                isClients = true;
                return isClients;
            }
            else
            {
                MessageBox.Show(_warningNotClient, "", MessageBoxButton.OK);
                isClients = false;
                return isClients;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MsgsHistory.MessageText.Count != 0)
            {
                StackPanelHistory.Children.Clear();
                for (int i = 0; i < MsgsHistory.MessageText.Count; i++)
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = MsgsHistory.MessageText[i];
                    StackPanelHistory.Children.Add(tb);
                }
            }
        }

        private void UpBalance_Click(object sender, RoutedEventArgs e)
        {
            if (CheckClients())
            {
                UpBalanceWindow UBW = GetUpBalanceWindow();
            }
        }

        private UpBalanceWindow GetUpBalanceWindow()
        {
            UpBalanceWindow UBW = new UpBalanceWindow();
            UBW.Owner = this;
            UBW.Show();
            Refresh(UBW);
            return UBW;
        }
    }
}
