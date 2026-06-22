using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.IdentityModel
{
    public record LoginDto 
    {
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
