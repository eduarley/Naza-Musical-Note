using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Infraestructure.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            string clave = (String)value;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{9,25}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (value == null || !(value is string) || string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }

            if(hasNumber.IsMatch(clave) && hasUpperChar.IsMatch(clave) && hasMiniMaxChars.IsMatch(clave) && hasLowerChar.IsMatch(clave) && hasSymbols.IsMatch(clave))
            {
                return true;
            }

            return false;
        }
    }
}
