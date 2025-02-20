using System.Net;

namespace Tech.Exception
{
    public abstract class TechException : SystemException
    {

        public abstract List<string> GetErrorMessage();

        public abstract HttpStatusCode GetStatusCode();
    }
}
