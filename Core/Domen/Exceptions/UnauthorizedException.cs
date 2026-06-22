using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string Massage = "IVaild Email Or Password"):base(Massage) 
        { 
        }
    }
}
