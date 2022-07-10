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
    /// Логика взаимодействия для UpBalanceWindow.xaml
    /// </summary>
    public partial class UpBalanceWindow : Window
    {
        public UpBalanceWindow()
        {
            InitializeComponent();
            ClientName.Text = MainWindow.CurrentClient.NameClient;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UpMoney.Text, out int result))
            {
                    MainWindow.Clients[MainWindow.NumberCurrentClient].AmountNow += result;
                    InfoText.Text = "Успешно выполнено";

                    string msg = $"Счёт клиента {MainWindow.CurrentClient.NameClient}"+
                        $"пополнен на сумму: {result}"+
                        $"[{DateTime.Now.ToShortTimeString()} {MainWindow.name}]";
                    MainWindow.MsgsHistory.Added(msg);
                Close();
            }
            else
            {
                InfoText.Text = "Неверный формат";
            }
        }
    }
}
