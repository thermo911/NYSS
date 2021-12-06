using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Exceptions
{
    class LoadingException : Exception
    {
        public LoadingException()
        {
        }

        public LoadingException(string message) : base(message)
        {
        }
    }
}
