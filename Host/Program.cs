using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(WCFChatApp.Service1));
            serviceHost.Open();
            Console.WriteLine("Service Started");
            Console.WriteLine("Press <Enter> to terminate.\n\n");
            Console.ReadLine();
            serviceHost.Close();
        }

        public static void log(String txt)
        {
            Console.WriteLine(txt);
        }
    }
}
