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
using System.Windows.Shapes;

namespace _13_BankingSystem
{
    /// <summary>
    /// Логика взаимодействия для TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        private int numberSender;
        private int numberRecipient;
        public TransferWindow()
        {
            InitializeComponent();
            ListNameSender.ItemsSource = MainWindow.clients;
            ListNameRecipient.ItemsSource = MainWindow.clients;
        }

        private void ListNameSender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numberSender = ListNameSender.SelectedIndex;
            InfoText.Text = "";
        }

        private void ListNameRecipient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numberRecipient = ListNameRecipient.SelectedIndex;
            InfoText.Text = "";
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TransferMoney.Text, out int result))
            {
                if (MainWindow.clients[numberSender].AmountNow >= result)
                {
                    MainWindow.clients[numberSender].AmountNow -= result;
                    MainWindow.clients[numberRecipient].AmountNow += result;
                    InfoText.Text = "Успешно выполнено";
                    ListNameSender.Items.Refresh();
                    ListNameRecipient.Items.Refresh();
                }
                else
                {
                    InfoText.Text = "Недостаточно средств";
                }
            }
            else
            {
                InfoText.Text = "Неверный формат";
            }
        }
    }
}
