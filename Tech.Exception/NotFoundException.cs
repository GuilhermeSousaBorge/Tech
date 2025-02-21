using System.Net;

namespace Tech.Exception
{
    public class NotFoundException : TechException
    {

        public NotFoundException(string message) : base(message) { }

        public override List<string> GetErrorMessage() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
