using System.ServiceModel;

namespace WcfServiceWebApp
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Hello(string name);
    }
}
