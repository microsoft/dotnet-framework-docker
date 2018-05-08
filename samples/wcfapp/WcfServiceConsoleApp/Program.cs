using System;
using System.ServiceModel;
using System.Threading;

namespace WcfServiceConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the ServiceHost
            ServiceHost host = new ServiceHost(typeof(Service1));

            try {
                // Open the ServiceHost to start listening for messages
                host.Open();

                foreach (var uri in host.BaseAddresses)
                {
                    Console.WriteLine("The service is ready at {0}", uri);
                }

                // Leave the service running
                Console.WriteLine("The service is running...");
                Thread.Sleep(-1);
            }
            catch
            {
                host?.Close();
                Console.WriteLine("The service is closed");
            }

        }
    }
}
