using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Exceptions
{
    class ParsingException : Exception
    {
        public ParsingException() : base()
        {
        }

        public ParsingException(string message) : base(message)
        {
        }
    }
}
