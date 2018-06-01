namespace WcfServiceConsoleApp
{
    public class Service1 : IService1
    {
        public string Hello(string name)
        {
            return string.Format("Hello {0} from Container!", name);
        }
    }
}
