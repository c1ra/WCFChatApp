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
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Page {


        TheController theController = TheController.Instance();
        int reciever;
        private void SendButton_Click()
        {
        }
        public AdminHome()
        {

            InitializeComponent();
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

        public string test1(int value)
        {
            throw new NotImplementedException();
        }

        public Task<string> test1Async(int value)
        {
            throw new NotImplementedException();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            List<Group> gS = new List<Group>();

            gS = await theController.GetGs();
            
            foreach(Group g in gS)
            {
                ListBoxItem item = new ListBoxItem();
                item = MakeItem(g.GName);
                theController.GroupsDictionary.Add(item, g);
                GroupListBox.Items.Add(item);
            }

            foreach (Group g in theController.GroupsDictionary.Values)
            {
                System.Console.WriteLine("Usao sam u Page_Loaded i pravim GItem");
                

            }

            await theController.getClients();

        }   //Set Message        

        private void GroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            messageListbox.Items.Clear();
            
            ListBoxItem item = GroupListBox.SelectedItem as ListBoxItem;
            if (item != null)
            {
                reciever = theController.GroupsDictionary[item].Id_G;

            }

            foreach (Message m in theController.MessagesList)
            {
                foreach(Client c in theController.ClientList)
                    if(c.Id_C == m.Id_Pos)
                    {
                        messageListbox.Items.Add(MakeItem(c.Nadimak, m.MsgTxt, m.MsgTime.ToString()));
                    }
                
            }
        }

        private async void SendButton_Click_1(object sender, RoutedEventArgs e)
        {
            string msgText = ChatInputTextBox.Text;

            await theController.SendMsg(msgText, reciever);
        }
    }
}
