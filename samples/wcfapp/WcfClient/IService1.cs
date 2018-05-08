using System.ServiceModel;

namespace WcfClient
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Hello(string name);
    }
}