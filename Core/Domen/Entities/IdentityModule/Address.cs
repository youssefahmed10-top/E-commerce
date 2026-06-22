using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IdentityModule
{
    public class Address
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } =  string.Empty;
        public string LastName { get; set; }   =  string.Empty;
        public string City { get; set; }    =  string.Empty;
        public string Country { get; set; } =string.Empty;
        public string street { get; set; } = string.Empty;
        public User User { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
