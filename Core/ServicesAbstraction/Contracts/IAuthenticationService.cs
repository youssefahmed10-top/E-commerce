using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.IdentityModel;

namespace ServicesAbstraction.Contracts
{
    public interface IAuthenticationService
    {
        //Login ==> Return : UserResultDTO [Display Name , Token , Email]  , Parameter : [Email , Password]
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        //Register ==> Return : UserResultDto [Display Name , Token , Email] , parameter : [DisplayName , Email , Password ,UserName , PhoneNumber] 
        Task<UserResultDto> RegisterAsync(RegisterDto register);
    }
}
