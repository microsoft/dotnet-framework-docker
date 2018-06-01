using System.ServiceModel;

namespace WcfServiceConsoleApp
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Hello(string name);
    }
}
