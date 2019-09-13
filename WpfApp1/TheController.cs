using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using WpfApp1.SerRef;
using System.Windows.Controls;

namespace WpfApp1
{
    class TheController : SerRef.IAdminCallback
    {


        //ZABAO SI ZATO:
        //Ne mozes u contoler da rzis listbox item u dictionary 
        //jer ne mozes da ga pravis tu, verovatno

        private Dictionary<ListBoxItem, Message> messageDictionary = new Dictionary<ListBoxItem, Message>();
        private Dictionary<ListBoxItem,Group >  groupDictionary = new Dictionary<ListBoxItem, Group>();
        private List<Message> messagesList = new List<Message>();
        private List<Client> clientList = new List<Client>();

        private AdminClient proxy = null;
        private int receiver;
        public  Client localclient = null;
        private GroupClient groupClient1 = null;
        private static TheController instance;

        
        public Dictionary<ListBoxItem, Group> GroupsDictionary { get => groupDictionary; set => groupDictionary = value; }
        public int Receiver { get => receiver; set => receiver = value; }
        public Dictionary<ListBoxItem, Message> GroupsMessages { get => messageDictionary; set => messageDictionary = value; }
        public List<Message> MessagesList { get => messagesList; set => messagesList = value; }
        public List<Client> ClientList { get => clientList; set => clientList = value; }

        private TheController()
        {

        }

        public static TheController Instance()
        {
            if (instance == null)
            {
                instance = new TheController();
            }
            return instance;
        }



        async void InnerDuplexChannel_Closed(Object sender, EventArgs e)
        {

            await Task.Run(async () => HandleProxy());

        }

        async void InnerDuplexChannel_Faulted(Object sender, EventArgs e)
        {
            await Task.Run(async () => HandleProxy());
        }

        async void InnerDuplexChannel_Opened(Object sender, EventArgs e)
        {
            await Task.Run(async () => HandleProxy());
        }


        private void HandleProxy()
        {
            if (proxy != null)
            {
                switch (this.proxy.State)
                {
                    case CommunicationState.Closed:
                        proxy = null;
                        // chatListBox.Items.Clear();
                        //OnlineUsersListBox.Items.Clear();
                        //LogInButton.IsEnabled = true;
                        break;
                    case CommunicationState.Closing:
                        break;
                    case CommunicationState.Created:
                        break;
                    case CommunicationState.Faulted:
                        proxy.Abort();
                        proxy = null;
                        //chatListBox.Items.Clear();
                        //OnlineUsersListBox.Items.Clear();

                        //LogInButton.IsEnabled = true;
                        //Try to handle Foulted state better
                        break;
                    case CommunicationState.Opened:
                        //Handle UI better in All states
                        break;
                    case CommunicationState.Opening:
                        break;
                    default:
                        break;


                }
            }
        }

        public async Task<bool> ConnectAsync(string usrName, string pass)
        {
            InstanceContext context = new InstanceContext(this);

            proxy = new AdminClient(context);
            proxy.InnerDuplexChannel.Faulted += new EventHandler(InnerDuplexChannel_Faulted);
            proxy.InnerDuplexChannel.Opened += new EventHandler(InnerDuplexChannel_Opened);
            proxy.InnerDuplexChannel.Closed += new EventHandler(InnerDuplexChannel_Closed);

            System.Console.WriteLine("Uso sam u Connect");
            try
            {
                System.Console.WriteLine("Uso sam u Try");

                this.localclient = await proxy.LogInAsync(usrName, pass);
                System.Console.WriteLine(this.localclient.Nadimak);
            }
            catch (Exception EX)
            {
                
                System.Console.WriteLine("U catchu sam"+EX.Message);
            }

            System.Console.WriteLine("Prosao sam await" + localclient.Id_C.ToString());

            if (localclient.Id_C != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            

            
            // Moras da izmenjas na Servisu da Login Vraca Informecije lokalnog klijenta
            //I Posle da zavrsis msg

        }

        public async Task SendMsg(string text, int idReciever) 
        {
            // mozda malo drugacije
            Message msg = new Message();
            msg.Id_Pos = localclient.Id_C;
            msg.Id_Prim = idReciever;
            msg.MsgTxt = text;
            msg.MsgTime = DateTime.Now;
                    
            await proxy.sendMessageAsync(msg);

            //TODo
            //proxy.CreateUserAsync();
            //proxy.EditUserAsync();
            //proxy.GroupRequestApprovalAsync();
            //proxy.LogOutAsync();
        }


        public async Task<bool> CreateUsr(string usrName, string pass, string nick, string type)
        {
            Client c = new Client();
            if (nick != "")
                c.Nadimak = nick;
            if (usrName != "")
                c.UsrName = usrName;
            if (pass != "")
                c.Pass = pass;
            if (nick != "")
                c.Type = type;

            return await proxy.CreateUserAsync(c);
        }


        public async Task<bool> EditUsr(int id, string usrName, string pass, string nick, string type)
        {
            Client c = new Client();
            c.Id_C = id;
            c.Nadimak = nick;
            c.UsrName = usrName;
            c.Pass = pass;
            c.Type = type;
            return await proxy.EditUserAsync(c);

        }


        //TODO Change On service to take GroupClient as argument,
        //Change getClients 
        public async Task GroupRequest(bool b, int idC, int idG)
        {
            GroupClient gC = new GroupClient();
            if (b)
            {
                gC.Id_C = idC;
                gC.Id_G = idG;
                await proxy.GroupRequestApprovalAsync(gC, b);
            }
        }

                // moguce RESENJE DA SE SYAVI DA VRACA TASK<List<Groups>>,
                // pa onda u adminHome da foreach g in listG 
                //{thecontroller.GroupDictionary.add( makeitem(g.id), g)
        public async Task<List<Group>> GetGs()
        {
            System.Console.WriteLine("Usao sam u ge Gs");
            List<Group> tmpGs = new List<Group>();
            tmpGs = await proxy.GetGsAsync();

            foreach(Group g in tmpGs)
            {
                System.Console.WriteLine(g.GName, g.Id_G, g.MaxMembers);
            }



            return tmpGs;
            
            //int br = 0;
            //foreach(Group g in tmpGs)
            //{
            //    br++;
            //    GroupsDictionary.Add(br, g);
            //}
            //Ovde Se vrati!!

        }

        public async Task getClients()
        {
            clientList = await proxy.GetMyClientsAsync(localclient.Id_C);
        }

        public async Task<List<GroupClient>> getGroupRequests()
        {
            return await proxy.GetGroupRequestsAsync();
        }

        


        public void Disconnect()
        {
             proxy.LogOutAsync(this.localclient);
        }

        //Cbs
        public void GetMs(List<Message> msgs)
        {

            foreach(Message m in msgs)
            {
                messagesList.Add(m);
            }

        }

        

    }
}
