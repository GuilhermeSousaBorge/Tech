using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Exception
{
    public abstract class TechException : SystemException
    {

        public abstract List<string> GetErrorMessage();

        public abstract HttpStatusCode GetStatusCode();
    }
}
