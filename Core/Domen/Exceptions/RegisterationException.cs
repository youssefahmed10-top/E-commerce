using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class RegisterationException: Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public RegisterationException(IEnumerable<string>error ):base("Validation Filed")
        {
            Errors = error;
        }
    }
}
