using System.Net;

namespace Tech.Exception
{
    public class ConflictException : TechException
    {
        public ConflictException(string message) : base(message) { }
        public override List<string> GetErrorMessage() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;
    }
}
