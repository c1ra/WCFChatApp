using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace WCFChatApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple,
    UseSynchronizationContext = false)]
    public class Service1 : IAdmin, IClient, IWebClient
    {

        Dictionary<Client, IClientCB> onlineclients =
             new Dictionary<Client, IClientCB>();

        List<Client> clientList = new List<Client>();
        List<Message> messageList = new List<Message>();
        List<Group> groupList = new List<Group>();
        List<GroupClient> groupClientsList = new List<GroupClient>();
        List<GroupClient> groupRequestList = new List<GroupClient>();
        object syncObj = new object();

        public Service1()
        {

            clientList.Insert(0, new Client());
            clientList[0].Id_C = 1;
            clientList[0].UsrName = "zika";
            clientList[0].Pass = "zika123";
            clientList[0].Nadimak = "zile";
            clientList[0].Type = "Admin";

            clientList.Insert(1, new Client());
            clientList[1].Id_C = 2;
            clientList[1].UsrName = "petar";
            clientList[1].Pass = "petar123";
            clientList[1].Nadimak = "pera";
            clientList[1].Type = "Admin";

            messageList.Insert(0, new Message());
            messageList[0].Id_Msg = 1;
            messageList[0].Id_Pos = 1;
            messageList[0].Id_Prim = 2;
            messageList[0].MsgTxt = "Test Test";
            messageList[0].MsgTime = DateTime.Now;

            messageList.Insert(1, new Message());
            messageList[1].Id_Msg = 2;
            messageList[1].Id_Pos = 2;
            messageList[1].Id_Prim = 1;
            messageList[1].MsgTxt = "Test2 Test2dsadsadsa";
            messageList[1].MsgTime = DateTime.Now;

            groupList.Insert(0, new Group());
            groupList[0].Id_G = 1;
            groupList[0].GName = "GroupTest";

            groupList.Insert(1, new Group());
            groupList[1].Id_G = 2;
            groupList[1].GName = "Group2";

            groupList.Insert(2, new Group());
            groupList[2].Id_G = 3;
            groupList[2].GName = "Group3";
           

            groupClientsList.Insert(0, new GroupClient());
            groupClientsList[0].Id_G = 1;
            groupClientsList[0].Id_C = 1;

            groupClientsList.Insert(1, new GroupClient());
            groupClientsList[1].Id_G = 1;
            groupClientsList[1].Id_C = 2;

            groupRequestList.Insert(0, new GroupClient());
            groupRequestList[0].Id_C = 1;
            groupRequestList[0].Id_G = 1;

        }


        //getting current callback channel
        public IClientCB currentCallback
        {
            get
            {
                
                return OperationContext.Current.GetCallbackChannel<IClientCB>();
                
            }
        }
        //HEREEEEE

        //helper functions
        public List<Message> findMyMessages(int idReciever)
        {
            System.Diagnostics.Debug.WriteLine("Usao sam ovde");
            List<Message> tmpMsgs = new List<Message>();
            
            foreach(Message m in messageList)
            {
                if (m.Id_Prim == idReciever)
                {
                    tmpMsgs.Add(m);
                }
            }
            return tmpMsgs;
        }

        public List<Group> FindMyGroups(int idC)
        {
            List<Group> tmpGs = new List<Group>();

            foreach(GroupClient gC in groupClientsList)
            {
                if(gC.Id_C == idC)
                {
                    Group tmpG = new Group();
                    tmpG = groupList.Find((x) => x.Id_G == gC.Id_G);
                    tmpGs.Add(tmpG);
                }
            }

            return tmpGs;
        }
        
        
        public string GetPath()
        {
            string path = Directory.GetCurrentDirectory();
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }

        public void LogMToF(string msg)
        {
            System.IO.StreamWriter sw = System.IO.File.AppendText(GetPath() + "MyLOOOg.txt");
            try
            {
                string logLine = System.String.Format("{0:G}: {1}.", System.DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }



        //Service Implementation
        public bool CreateUser(Client usr)
        {
            int tmpId = 1;

            if(string.IsNullOrEmpty(usr.UsrName) & string.IsNullOrEmpty(usr.Pass) & string.IsNullOrEmpty(usr.Nadimak))
            {
                return false;
            }
            else
            {
                lock (syncObj)
                {
                    tmpId += clientList.Last().Id_C;
                    usr.Id_C = tmpId;
                    clientList.Add(usr);
                }
                return true;
            }
        }

        public bool EditUser(Client usr)
        {
            bool tmp = false;


            for (int i = 0; i < clientList.Count; i++)
            {
                if (clientList[i].Id_C == usr.Id_C)
                {
                    lock (syncObj)
                    {
                        if (!string.IsNullOrEmpty(usr.UsrName))
                        {
                            clientList[i].UsrName = usr.UsrName;
                        }
                        if (!string.IsNullOrEmpty(usr.Nadimak))
                        {
                            clientList[i].Nadimak = usr.Nadimak;
                        }
                        if (!string.IsNullOrEmpty(usr.Pass))
                        {
                            clientList[i].Pass = usr.Pass;
                        }
                        if (!string.IsNullOrEmpty(usr.Type))
                        {
                            clientList[i].Type = usr.Type;
                        }
                    }
                    tmp = true;
                }
            }
            return tmp;
        }

        public void GroupLeave(Client c, Group g)
        {
            GroupClient tmp = new GroupClient();
            if((c.Id_C != 0) & (g.Id_G != 0))
            {
                tmp.Id_C = c.Id_C;
                tmp.Id_G = g.Id_G;
                lock (syncObj)
                {
                    groupClientsList.Remove(tmp);
                }
            }
        }

        public void GroupRequest(Client c, Group g)
        {
            GroupClient tmp = new GroupClient();
            if ((c.Id_C != 0) & (g.Id_G != 0))
            {
                tmp.Id_C = c.Id_C;
                tmp.Id_G = g.Id_G;
                lock (syncObj)
                {
                    groupRequestList.Add(tmp);
                }

            }
            
        }

        public void GroupRequestApproval(GroupClient gC, bool approval)
        {
            GroupClient tmp = new GroupClient();

            if ((gC.Id_C != 0) & (gC.Id_G != 0))
            {
                tmp.Id_C = gC.Id_C;
                tmp.Id_G = gC.Id_G;
                lock (syncObj)
                {
                    groupRequestList.Remove(tmp);
                    if (approval)
                    {
                        groupClientsList.Add(tmp);
                    }
                }

            }
        }

        public Client LogIn(string UsrName, string Password)
        {
            LogMToF("Client incoming");
            Client tmpC = new Client();
            
                foreach (Client c in clientList)
                {
                    if (c.UsrName == UsrName && c.Pass == Password)
                    {

                        tmpC = c;
                        tmpC.Pass = "";

                        lock (syncObj)
                        {
                            onlineclients.Add(c, currentCallback);
                            currentCallback.GetMessages(findMyMessages(c.Id_C));

                        }
                        LogMToF(c.Nadimak + "LogIn Succes");
                        return tmpC;
                    }
                }
                LogMToF("LogIn Failed");
                
                return tmpC;
        }

        

        public void LogOut(Client client)
        {
            foreach (Client c in onlineclients.Keys)
            {
                if (client.Nadimak == c.Nadimak)
                {
                    lock (syncObj)
                    {

                        this.onlineclients.Remove(c);
                        // check later
                    }
                }
            }
        }

        public void sendMessage(Message msg)
        {
            List<Message> m = new List<Message>(); 
            msg.Id_Prim = messageList.Last().Id_Msg + 1;
            lock (syncObj)
            {
                messageList.Add(msg);
                foreach (Client c in onlineclients.Keys)
                {
                    m.Add(msg);
                    onlineclients[c].GetMessages(m);
                }
            }
        }


        public List<GroupClient> GetGroupRequests()
        {
            return groupRequestList;
        }

        public List<Group> GetGroups()
        {
            return groupList;
        }





        public List<Message> GetMessages(string usrName, int gID)
        {
            throw new NotImplementedException();
        }

        public void CreateMessage(string usrName, int id_Msg, int id_Pos, int id_Prim, string msgTxt, DateTime msgTime)
        {
            throw new NotImplementedException();
        }

        
        
        

        public List<Client> GetMyClients(int idC)
        {
            List<Client> tmpCs = new List<Client>();
            foreach (Client c in clientList)
            {
                c.Pass = "";
                c.Type = "";
                c.UsrName = "";
                tmpCs.Add(c);
                
            }

            return tmpCs;
        }
    }

    


}
