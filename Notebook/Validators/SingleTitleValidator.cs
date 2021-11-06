using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notebook.Validators
{
    class SingleTitleValidator : IStringValidator
    {
        private static Regex regex = new Regex(@"^[a-zA-Zа-яА-Я]+$");
        public bool Validate(string s) => regex.IsMatch(s ?? "");
    }
}
