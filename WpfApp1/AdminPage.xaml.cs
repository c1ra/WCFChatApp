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
using WpfApp1.SerRef;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private Dictionary<ListBoxItem, Client> clientDictionary = new Dictionary<ListBoxItem, Client>();
        private Dictionary<ListBoxItem, GroupClient> groupClientDictionary = new Dictionary<ListBoxItem, GroupClient>();
        private List<GroupClient> groupClients = new List<GroupClient>();
        TheController theController = TheController.Instance();
        private Client selectedClient;
        GroupClient selecteGroupRequest;


        public object ClientDictionary { get; private set; }

        public AdminPage()
        {
            InitializeComponent();
        }



        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Client c in theController.ClientList)
            {
                ListBoxItem item = MakeItem(c.Nadimak);
                ClientsListBox.Items.Add(item);
                clientDictionary.Add(item, c);
            }

            groupClients = await theController.getGroupRequests();

            List<Group> gS = theController.GroupsDictionary.Values.ToList();
            foreach (GroupClient gC in groupClients)
            {
                Client c = theController.ClientList.Find(x => x.Id_C == gC.Id_C);
                Group g = gS.Find(x => x.Id_G == gC.Id_G);
                ListBoxItem item = MakeItem(c.Nadimak, g.GName);
                ClientGroupListBox.Items.Add(item);
                groupClientDictionary.Add(item, gC);
            }
        }


        private ListBoxItem MakeItem(string text, params string[] other)
        {
            TextBlock textBlock = new TextBlock();

            if (other.Length == 0)
            {
                textBlock.Text = text;
            }
            else
            {
                for (int i = 0; i < other.Length; i++)
                {
                    text += " " + other[i];
                }
                textBlock.Text = text;
            }


            StackPanel panel = new StackPanel();
            // panel.Orientation = Orientation.Horizontal;
            panel.Children.Add(textBlock);

            ListBoxItem boxItem = new ListBoxItem();
            boxItem.Content = panel;

            return boxItem;
        }

        private void ClientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = ClientsListBox.SelectedItem as ListBoxItem;
            if (item != null)
            {
                
                selectedClient = clientDictionary[item];
                usrIDTextBox.Text = selectedClient.Id_C.ToString();
                usrNameTextBox.Text = selectedClient.UsrName;
                usrPassTextBox.Text = selectedClient.Pass;
                usrNickTextBox.Text = selectedClient.Nadimak;
                usrTypeTextBox.Text = selectedClient.Type;

            }
        }

        private void ClientGroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = ClientGroupListBox.SelectedItem as ListBoxItem;
            if (item != null)
            {
                GroupClient gC = groupClientDictionary[item];

                selecteGroupRequest = gC;
                usrIdLabel.Content = "usrId: " + gC.Id_C;
                //comeback for gID

                
            }
        }

        private async void EditUserButton_Click(object sender, RoutedEventArgs e)
        {


            bool b;
            b = await  theController.EditUsr(int.Parse(usrIDTextBox.Text), usrNameTextBox.Text, usrPassTextBox.Text, usrNickTextBox.Text, usrTypeTextBox.Text);

            if (b)
            {
                messageLabel.Content = "User Edited";
            }
            else
            {
                messageLabel.Content = "User not edited";
            }


        }

        private async void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {

            Client tmpC = new Client();

            if (usrNameTextBox.Text != "" & usrPassTextBox.Text != "" & usrNickTextBox.Text != "" &
                usrTypeTextBox.Text != "")
            {
                bool b;
                b = await theController.CreateUsr(usrNameTextBox.Text, usrPassTextBox.Text, usrNickTextBox.Text,
                                                usrTypeTextBox.Text);
                if (b)
                    messageLabel.Content = "Succesful";
                else
                    messageLabel.Content = "Failed";

            }
            else
            {
                messageLabel.Content = "Morate Uneti sva polja!!";
            }
        }

        private async void ApproveGRButton_Click(object sender, RoutedEventArgs e)
        {
             await theController.GroupRequest(true, selecteGroupRequest.Id_C, selecteGroupRequest.Id_G);

        }

        private async void DenyGRButton_Click(object sender, RoutedEventArgs e)
        {
            await theController.GroupRequest(false, selecteGroupRequest.Id_C, selecteGroupRequest.Id_G);
        }
    }
}
