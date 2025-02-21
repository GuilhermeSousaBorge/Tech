using System.Net;

namespace Tech.Exception
{
    public abstract class TechException : SystemException
    {
        protected TechException(string message) : base(message){ }
        public abstract List<string> GetErrorMessage();

        public abstract HttpStatusCode GetStatusCode();
    }
}
