using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Models.Options;
using ProductCatalog.Core.Storages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductCatalog.Core.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IUserStorage m_UserStorage;
        private readonly UserManager<UserEntity> m_UserManager;
        private readonly SignInManager<UserEntity> m_SignInManager;
        private readonly JwtOptions m_JwtOptions;
        public AuthService(
            IUserStorage userStorage,
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IOptions<JwtOptions> opts)
        {
            m_UserStorage = userStorage;
            m_UserManager = userManager;
            m_SignInManager = signInManager;
            m_JwtOptions = opts.Value;
        }

        public async Task<AuthResult> AuthenticateAsync(SignInUserDto model)
        {
            try
            {
                var existingUser = await m_UserManager.FindByEmailAsync(model.Email);
                if (existingUser is null)
                {
                    throw new Exception("User not found");
                }

                var signInResult = await m_SignInManager.CheckPasswordSignInAsync(existingUser, model.Password, false);
                if (!signInResult.Succeeded)
                {
                    throw new Exception("Wrong email or password");
                }

                var token = await CreateTokenAsync(model.Email);
                var user = await m_UserStorage.GetAsync(model.Email);

                AuthResult result = new() { User = user, Token = token };

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AuthResult> RegisterAsync(CreateUserDto model)
        {
            try
            {
                var registerResult = await m_UserStorage.CreateAsync(model);
                if (!registerResult)
                {
                    throw new Exception("Failed to create user");
                }

                var token = await CreateTokenAsync(model.Email);
                var user = await m_UserStorage.GetAsync(model.Email);

                AuthResult result = new() { User = user, Token = token };

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private async Task<string> CreateTokenAsync(string userEmail)
        {
            try
            {
                var user = await m_UserManager.FindByEmailAsync(userEmail);

                if (user is not null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    };

                    var userRoles = await m_UserManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwt = new JwtSecurityToken(
                        issuer: m_JwtOptions.ValidIssuer,
                        audience: m_JwtOptions.ValidAudience,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromDays(5)),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(m_JwtOptions.SecretKey)),
                            SecurityAlgorithms.HmacSha256));

                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            throw new Exception("No user found");
        }
    }
}
