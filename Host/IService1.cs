using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.


    [ServiceContract(CallbackContract =typeof(IClientCB),SessionMode = SessionMode.Required)]
    public interface IAdmin : IClient
    {
        [OperationContract]
        bool CreateUser(Client usr);
        [OperationContract]
        bool EditUser(Client usr);
        [OperationContract(IsOneWay = true)]
        void GroupRequestApproval(GroupClient gC, bool approval);
        [OperationContract]
        List<GroupClient> GetGroupRequests();
        

    }

    [ServiceContract(CallbackContract =typeof(IClientCB),SessionMode = SessionMode.Required)]
    public interface IClient
    {
        [OperationContract(IsInitiating = true)]
        Client LogIn(string UsrName, string Password);
        [OperationContract(IsTerminating = true, IsOneWay = true)]
        void LogOut(Client client);
        [OperationContract(IsOneWay = true)]
        void sendMessage(Message msg);
        [OperationContract(IsOneWay = true)]
        void GroupRequest(Client c,Group g);
        [OperationContract(IsOneWay = true)]
        void GroupLeave(Client c, Group g);
        [OperationContract(Name = "GetGs")]
        List<Group> GetGroups();
        [OperationContract]
        List<Client> GetMyClients(int idC);
        
    }
    [ServiceContract]
    public interface IClientCB
    {
        [OperationContract(Name = "GetMs")]
        void GetMessages(List<Message> msgs);
        
        
    }

    [ServiceContract]
    public interface IWebClient
    {
        //Message
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/{usrName}/Messages/getMessages?gID={gID}")]
        List<Message> GetMessages(string usrName, int gID);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/{usrName}/Messages/createMessage?id_Msg={id_Msg}&id_Pos={id_Pos}&id_Prim={id_Prim}&msgTxt={msgTxt}&msgTime={msgTime}")]
        void CreateMessage(string usrName, int id_Msg, int id_Pos, int id_Prim, string msgTxt, DateTime msgTime);
        //[OperationContract]
       // [WebInvoke(Method = "GET", UriTemplate = "/{usrName}/Groups/getGroups?cID={cID}")]
        //List<Group> GetGroups(string usrName, int cID); 

    }
    


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WCFChatApp.ContractType".
    [DataContract]
    public class Client
    {
        private int id_C;
        private string usrName;
        private string pass;
        private string nadimak;
        private string type;

        [DataMember]
        public int Id_C
        {
            get { return id_C; }
            set { id_C = value; }
        }
        [DataMember]
        public string UsrName
        {
            get { return usrName; }
            set { usrName = value; }
        }
        [DataMember]
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        [DataMember]
        public string Nadimak
        {
            get { return nadimak; }
            set { nadimak = value; }
        }
        [DataMember]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
    [DataContract]
    public class Message
    {
        private int id_Msg;
        private int id_Pos;
        private int id_Prim;
        private string msgTxt;
        private DateTime msgTime;
        [DataMember]
        public int Id_Msg { get => id_Msg; set => id_Msg = value; }
        [DataMember]
        public int Id_Pos { get => id_Pos; set => id_Pos = value; }
        [DataMember]
        public int Id_Prim { get => id_Prim; set => id_Prim = value; }
        [DataMember]
        public string MsgTxt { get => msgTxt; set => msgTxt = value; }
        [DataMember]
        public DateTime MsgTime { get => msgTime; set => msgTime = value; }
        

    }
    [DataContract]
    public class Group
    {
        private int id_G;
        private string gName;
        private int maxMembers;

        [DataMember]
        public int Id_G { get => id_G; set => id_G = value; }
        [DataMember]
        public int MaxMembers { get => maxMembers; set => maxMembers = value; }
        [DataMember]
        public string GName { get => gName; set => gName = value; }
    }
    [DataContract]
    public class GroupClient
    {
        private int id_C;
        private int id_G;

        [DataMember]
        public int Id_C { get => id_C; set => id_C = value; }
        [DataMember]
        public int Id_G { get => id_G; set => id_G = value; }
    }
}
