using System.Net;

namespace Tech.Exception
{
    public class InvalidLoginException : TechException
    {
        public override List<string> GetErrorMessage() => ["Email ou senha invalidos"];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
