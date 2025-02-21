using System.Net;

namespace Tech.Exception
{
    public class ErrorOnValidationException : TechException
    {

        private readonly List<string> _errors;

        public ErrorOnValidationException(List<string> errorsList) : base(string.Empty)
        {
            _errors = errorsList;
        }
        public override List<string> GetErrorMessage() => _errors;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
