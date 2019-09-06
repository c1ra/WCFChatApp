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

namespace localClient
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        string usrName, pass;
        TheController controller = TheController.Instance();
        public LogInPage()
        {
            InitializeComponent();

        }

        private async void LogInBttn_Click(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Dugme Pressed");
            usrName = usrNameTextBox.Text;
            pass = passTextBox.Text;
            bool b;
            b = await controller.ConnectAsync(usrName, pass);
            System.Console.WriteLine("Zavrsio sam await u eventu");
            if (b)
            {
                Home Home = new Home();
                NavigationService.Navigate(Home);

            }
            else
                msgLabel.Content = "Wrong username/password";
        }
    }
}
