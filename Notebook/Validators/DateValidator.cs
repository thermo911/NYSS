using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notebook.Validators
{
    class DateValidator : IStringValidator
    {
        private static Regex _regex = new Regex(@"[0-9]{2}[./-][0-9]{2}[./-][0-9]{4}");
        public bool Validate(string s) => _regex.IsMatch(s ?? "");
    }
}
