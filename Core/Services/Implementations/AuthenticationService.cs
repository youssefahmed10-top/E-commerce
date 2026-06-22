using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction.Contracts;
using Shared.Common;
using Shared.Dtos.IdentityModel;
using static System.Net.WebRequestMethods;

namespace Services.Implementations
{
    public class AuthenticationService (UserManager<User> _UserManager ,IOptions<JwtOptions> _options): IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
           //Check Email exist or not [Exist => Get Acount]
           var UserAcount= await _UserManager.FindByEmailAsync(loginDto.Email);
            if (UserAcount is null) throw new UnauthorizedException();

            //check Password
            var Password = await _UserManager.CheckPasswordAsync(UserAcount,loginDto.Password);
            if (!Password) throw new UnauthorizedException(); 

            return new UserResultDto(UserAcount.DisplayName, await CreateTokenAsync(UserAcount), UserAcount.Email);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto register)
        {
            var User = new User()
            {
                DisplayName = register.DisplayName,
                UserName = register.userName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber
            };

           var Result = await _UserManager.CreateAsync(User, register.Password);

            //Validations
            if (!Result.Succeeded) 
            {
                var error = Result.Errors.Select(e=>e.Description).ToList();
                throw new RegisterationException(error);
            
            }

            return new UserResultDto(User.DisplayName,await CreateTokenAsync(User), User.Email);
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var JwtOptions = _options.Value;

            //1] Claims ==> Name ,Email ,Role
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var roles = await _UserManager.GetRolesAsync(user);
            foreach (var Roles in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Roles));
            }

            //2]Key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecuretyKey));

            //3]Algorithem [SigningCredentials] (Key + Algorathim);
            var signCredential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            //4] Create Token
            var Token = new JwtSecurityToken(issuer: JwtOptions.Issuer, audience: JwtOptions.Audience, claims: Claims, expires: DateTime.UtcNow.AddDays(JwtOptions.ExpirationInDays), signingCredentials: signCredential);

            //5] Return Token
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
