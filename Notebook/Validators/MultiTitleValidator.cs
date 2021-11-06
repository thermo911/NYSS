using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notebook.Validators
{
    class MultiTitleValidator : IStringValidator
    {
        private static Regex _regex = new Regex(@"^[a-zA-Zа-яА-Я]+([-\s]+[a-zA-Zа-яА-Я]+)*$");
        public bool Validate(string s) => _regex.IsMatch(s ?? "");
    }
}
