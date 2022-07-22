using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConvertingInformation;

namespace _13_BankingSystem
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddOrEditClientWindow : Window
    {
        Client currentClient =MainWindow.CurrentClient;
        Client newClient;
        private double percentPerson = 10;
        private double percentLegalBody = 3;

        double percent;
       public bool isCapitalization;
        public AddOrEditClientWindow()
        {

            InitializeComponent();


            for (int i = 0; i < MainWindow.arrayMonth.Length; i++)
            {
                MonthBirthday.Items.Add(MainWindow.arrayMonth[i]);
                MonthStartDeposit.Items.Add(MainWindow.arrayMonth[i]);
                MonthEndDeposit.Items.Add(MainWindow.arrayMonth[i]);
            }
            for (int i=DateTime.Now.Year-50;i< DateTime.Now.Year+50; i++)
            {
                if (DateTime.Now.Year >= i)
                {
                    YearBirthday.Items.Add(i);
                }
                if (DateTime.Now.Year <= i)
                {
                    YearStartDeposit.Items.Add(i);
                    YearEndDeposit.Items.Add(i);
                }
            }
            MonthBirthday.SelectedItem = MonthBirthday.Items[0];
            MonthStartDeposit.SelectedItem = MonthStartDeposit.Items[DateTime.Now.Month];
            MonthEndDeposit.SelectedItem = MonthEndDeposit.Items[DateTime.Now.Month];

            YearBirthday.SelectedItem = YearBirthday.Items[0];
            YearStartDeposit.SelectedItem = YearStartDeposit.Items[0];
            YearEndDeposit.SelectedItem = YearEndDeposit.Items[0];
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            double depositCalculated = Converter.ConvertingStringInDouble(Deposit.Text);
            if (depositCalculated >= 0)
            {

                DateTime startDepositDate = Converter.GetDate(MainWindow.arrayMonth,
                    YearStartDeposit.Text,
                    MonthStartDeposit.Text,
                    CountDayStartDeposit.Text);

                DateTime endDepositDate = Converter.GetDate(MainWindow.arrayMonth,
                    YearEndDeposit.Text,
                    MonthEndDeposit.Text,
                    CountDayEndDeposit.Text);

                if (RadioButtonLegalBody.IsChecked == true)
                {
                    newClient = new LegalBody("", depositCalculated, startDepositDate, endDepositDate, GetBankRate(), isCapitalization, CheckVIP());
                }
                else if (RadioButtonClient.IsChecked == true)
                {
                    newClient = new Person("", depositCalculated, startDepositDate, endDepositDate, GetBankRate(), isCapitalization, CheckVIP());
                }
                Amount.Text = newClient.AmountEnd.ToString();
            }
            else 
            {
                Amount.Text = "";
                DialogString.Text = "Некорректные данные";
            }
        }

        private void client_Checked(object sender, RoutedEventArgs e)
        {
            TextBlock_LastName.Foreground = Brushes.Black;
            LastName.IsEnabled = true;
            TextBlock_FirstName.Foreground = Brushes.Black;
            FirstName.IsEnabled = true;
            TextBlock_Patronymic.Foreground = Brushes.Black;
            Patronymic.IsEnabled = true;
            TextBlock_NameCompany.Foreground = Brushes.Gray;
            NameCompany.IsEnabled = false;

        }
        private void LegalEntity_Checked(object sender, RoutedEventArgs e)
        {
            TextBlock_LastName.Foreground = Brushes.Gray;
            LastName.IsEnabled = false;
            TextBlock_FirstName.Foreground = Brushes.Gray;
            FirstName.IsEnabled = false;
            TextBlock_Patronymic.Foreground = Brushes.Gray;
            Patronymic.IsEnabled = false;

            TextBlock_NameCompany.Foreground = Brushes.Black;
            NameCompany.IsEnabled = true;
        }

        private void Capitalization_Checked(object sender, RoutedEventArgs e)
        {
            GetTypeAccruals(true);
        }

        private void NotCapitalization_Checked(object sender, RoutedEventArgs e)
        {
            GetTypeAccruals(false);
        }

        private double GetBankRate()
        {
            double _percent;
            if (RadioButtonClient.IsChecked == true)
                _percent = percentPerson;
            else _percent = percentLegalBody;
            if (VIPclient.IsChecked == true) //если перед нами випклиент его банковская ставка увеличивается на 2
            {
                _percent *= 2;
                percent = _percent;
                return percent;
            }
            else
            {
                percent = _percent;
                return percent;
            }
        }
        private bool GetTypeAccruals(bool _isCapitalization) //получить тип начислений
        {
            isCapitalization = _isCapitalization;
            return isCapitalization;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DateTime startDepositDate = Converter.GetDate(MainWindow.arrayMonth,YearStartDeposit.Text, MonthStartDeposit.Text, CountDayStartDeposit.Text);
                DateTime endDepositDate = Converter.GetDate(MainWindow.arrayMonth,YearEndDeposit.Text, MonthEndDeposit.Text, CountDayEndDeposit.Text);

                if (RadioButtonClient.IsChecked == true)
                {
                    DateTime birthDay = Converter.GetDate(MainWindow.arrayMonth, YearBirthday.Text, MonthBirthday.Text, CountDayBirth.Text);
                    newClient = new Person(
                            LastName.Text,
                            FirstName.Text,
                            Patronymic.Text,
                            birthDay,
                            Converter.ConvertingStringInDouble(Deposit.Text),
                            startDepositDate,
                            endDepositDate,
                            GetBankRate(),
                            isCapitalization,
                            CheckVIP()
                            );
                }
                else if(RadioButtonLegalBody.IsChecked==true)
                {
                    newClient = new LegalBody(NameCompany.Text, Converter.ConvertingStringInDouble(Deposit.Text), startDepositDate, endDepositDate, GetBankRate(), isCapitalization, CheckVIP());
                }
                Amount.Text = "";
                DialogString.Text = "Результат сохранён";
                string name = Environment.UserName;
                DateTime date = DateTime.Now;

                if (MainWindow.IsEdit == false)
                {
                    MainWindow.Clients.Add(newClient);

                    string text = $"Создан счет на {newClient.NameClient} на сумму {newClient.AmountNow.ToString()} [{date.ToShortTimeString()} {name}]";
                    MainWindow.MsgsHistory.Added(text);

                    Close();
                }
                else if (MainWindow.IsEdit == true)
                {
                    string text = $"Счет изменен с {MainWindow.Clients[MainWindow.NumberCurrentClient].NameClient}" +
                        $" сумма: {MainWindow.Clients[MainWindow.NumberCurrentClient].AmountNow.ToString()} "  +
                        $"на {newClient.NameClient} сумма: {newClient.AmountNow.ToString()}" +
                        $"[{date.ToShortTimeString()} {name}]";


                    MainWindow.MsgsHistory.Added(text);
                    MainWindow.Clients[MainWindow.NumberCurrentClient] = newClient;
                    Close();
                }
            }
            catch(IndexOutOfRangeException)
            {
                Amount.Text = "";
                DialogString.Text = "Некорректные данные";
            }
        }

        private bool CheckVIP()
        {
            if (VIPclient.IsChecked == true)
                return true;
            else
                return false;
        }
        private void Deposit_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Deposit.Text = "";
            Deposit.Foreground = Brushes.Black;
        }

        private void CountDayBirthPlus_Click(object sender, RoutedEventArgs e)
        {
            CountDayBirth.Text = GetCountPlus(CountDayBirth,MonthBirthday,YearBirthday);
        }

        private void CountDayBirthMinus_Click(object sender, RoutedEventArgs e)
        {
            CountDayBirth.Text =GetCountMinus(CountDayBirth);
        }

        private void CountDayDepositPlus_Click(object sender, RoutedEventArgs e)
        {
            CountDayStartDeposit.Text = GetCountPlus(CountDayStartDeposit,MonthStartDeposit, YearStartDeposit);
        }

        private void CountDayDepositMinus_Click(object sender, RoutedEventArgs e)
        {
            CountDayStartDeposit.Text = GetCountMinus(CountDayStartDeposit);
        }

        private void CountDayPlus_Click(object sender, RoutedEventArgs e)
        {
            CountDayEndDeposit.Text = GetCountPlus(CountDayEndDeposit,MonthEndDeposit, YearEndDeposit);
        }

        private void CountDayMinus_Click(object sender, RoutedEventArgs e)
        {
            CountDayEndDeposit.Text = GetCountMinus(CountDayEndDeposit);
        }
        private string GetCountPlus(TextBlock day, ComboBox month, ComboBox year)
        {
            int countDay = int.Parse(day.Text);

            if (countDay < DateTime.DaysInMonth(int.Parse(year.Text), (month.SelectedIndex + 1)))
                countDay++;
            return countDay.ToString();
        }
        private string GetCountMinus(TextBlock textBlock)
        {
            int countDay = int.Parse(textBlock.Text);
            if (countDay > 1)
                countDay--;
            return countDay.ToString();
        }

        private void MonthBirthday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountDayBirth.Text = CheckCountInMonth(YearBirthday.Text, MonthBirthday.SelectedIndex + 1, CountDayBirth.Text);
        }

        private void MonthStartDeposit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountDayStartDeposit.Text = CheckCountInMonth(YearStartDeposit.Text, MonthStartDeposit.SelectedIndex + 1, CountDayStartDeposit.Text);
        }

        private void MonthEndDeposit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountDayEndDeposit.Text = CheckCountInMonth(YearEndDeposit.Text, MonthEndDeposit.SelectedIndex + 1, CountDayEndDeposit.Text);
        }

        private string CheckCountInMonth(string year,int month, string day)
        {
            if (YearBirthday.Text != "") //это проверка на то что окно только что не создано
            {
                int yearInt = int.Parse(year);
                int countDay = DateTime.DaysInMonth(yearInt, month);
                int currentCountDay = int.Parse(day);

                if (countDay < currentCountDay)
                {
                    return countDay.ToString();
                }
                else
                {
                    return currentCountDay.ToString();
                }
            }
            return "";
        }
    }
}
