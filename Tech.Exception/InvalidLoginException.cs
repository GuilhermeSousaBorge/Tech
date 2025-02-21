using System.Net;

namespace Tech.Exception
{
    public class InvalidLoginException : TechException
    {
        public InvalidLoginException() : base("Email ou senha invalidos")
        {
        }

        public override List<string> GetErrorMessage() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
