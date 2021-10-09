using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Infraestructure.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MembershipPasswordRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            int minimo = 9;

            if (value == null || !(value is string) || string.IsNullOrEmpty(value.ToString()))
            {
                return false;
            }



            return true;
        }
    }
}
