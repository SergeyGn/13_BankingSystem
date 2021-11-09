﻿using System;
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
using System.Windows.Shapes;

namespace _13_BankingSystem
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddOrEditClientWindow : Window
    {
        Client currentClient =MainWindow.CurrentClient;
        Client newClient;

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
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDepositDate = GetDate(YearStartDeposit.Text, MonthStartDeposit.Text, CountDayStartDeposit.Text);
            DateTime endDepositDate = GetDate(YearEndDeposit.Text, MonthEndDeposit.Text, CountDayEndDeposit.Text);
            newClient = new Client(ConvertingStringInDouble(Deposit.Text), startDepositDate, endDepositDate, percent, isCapitalization);
            Amount.Text = newClient.AmountNow.ToString();

        }

        private static DateTime GetDate(string year, string month, string day)
        {
            int countMonth=1;
            for(int i=0;i< MainWindow.arrayMonth.Length;i++)
            {
                if(MainWindow.arrayMonth[i]==month)
                {
                    countMonth = i + 1;
                }
            }
            DateTime date = new DateTime(int.Parse(year), countMonth, int.Parse(day));
            return date;
        }

        private double ConvertingStringInDouble(string text)
        {
            if(double.TryParse(text,out double result))
            {
                return result;
            }
            else
            {
                Deposit.Text = "Некорректное значение";
                return -1;
            }
        }

        private void client_Checked(object sender, RoutedEventArgs e)
        {
            GetBankRate(10);
        }

        private void VIPclient_Checked(object sender, RoutedEventArgs e)
        {
            GetBankRate(25);
        }

        private void LegalEntity_Checked(object sender, RoutedEventArgs e)
        {
            GetBankRate(5);
        }

        private void Capitalization_Checked(object sender, RoutedEventArgs e)
        {
            GetTypeAccruals(true);
        }

        private void NotCapitalization_Checked(object sender, RoutedEventArgs e)
        {
            GetTypeAccruals(false);
        }

        private double GetBankRate(double _percent)
        {
            percent = _percent;
            return percent;
        }
        private bool GetTypeAccruals(bool _isCapitalization) //получить тип начислений
        {
            isCapitalization = _isCapitalization;
            return isCapitalization;
        }
    }
}
