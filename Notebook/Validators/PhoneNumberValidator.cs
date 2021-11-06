using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notebook.Validators
{
    class PhoneNumberValidator : IStringValidator
    {
        private static Regex regex = new Regex(@"^\+?[0-9]{11}$");
        public bool Validate(string s) => regex.IsMatch(s ?? "");
    }
}
