using System;
using System.ServiceModel;

namespace WcfClient
{
    class Program
    {
        static string host;

        static void Main(string[] args)
        {
            Console.WriteLine("Client OS: {0}", Environment.OSVersion);
            host = Environment.GetEnvironmentVariable("host") ?? "localhost";
            Console.WriteLine("Service Host: {0}", host);

            CallViaHttp();

            CallViaNetTcp();
        }

        static void CallViaHttp()
        {
            var address = string.Format("http://{0}/Service1.svc", host);
            var binding = new BasicHttpBinding();
            var factory = new ChannelFactory<IService1>(binding, address);
            var channel = factory.CreateChannel();

            Console.WriteLine(channel.Hello("WCF via Http"));
        }

        static void CallViaNetTcp()
        {
            var address = string.Format("net.tcp://{0}/Service1.svc", host);
            var binding = new NetTcpBinding(SecurityMode.None);
            var factory = new ChannelFactory<IService1>(binding, address);
            var channel = factory.CreateChannel();

            Console.WriteLine(channel.Hello("WCF via Net.Tcp"));
        }
    }
}
