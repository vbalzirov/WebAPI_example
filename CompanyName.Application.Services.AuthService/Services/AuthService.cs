﻿using AutoMapper;
using CompanyName.Application.Dal.Auth.Configurations;
using CompanyName.Application.Dal.Auth.Models;
using CompanyName.Application.Dal.Auth.Repository;
using CompanyName.Application.Services.AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace CompanyName.Application.Services.AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> manager;
        private readonly IAuthRepository repository;
        private readonly IJwtConfigurationSettings settings;
        private readonly IMapper mapper;

        public AuthService(
            UserManager<IdentityUser> userManager,
            IAuthRepository authRepository,
            IJwtConfigurationSettings jwtConfigurationSettings,
            IMapper autoMapper) 
        {
            repository = authRepository;
            manager = userManager;
            settings = jwtConfigurationSettings;
            mapper = autoMapper;
        }

        public async Task<AuthResult> RegisterUser(UserRegister userRegister)
        {
            var existingEmail = await manager.FindByEmailAsync(userRegister.Email);
            if (existingEmail != null)
            {
                return new AuthResult
                {
                    Success = false,
                    Error = new List<string> { "Email already registred" }
                };
            }

            var user = new IdentityUser
            {
                Email = userRegister.Email,
                UserName = userRegister.UserName
            };

            var isUserCreated = await manager.CreateAsync(user, userRegister.Password);

            if (!isUserCreated.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Error = isUserCreated.Errors.Select(error => error.Description)
                };
            }

            var token = GetJwtToken(user);

            var userDal = mapper.Map<UserRegister, UserDal>(userRegister);
            userDal.Role = GetRole();

            var userId = repository.AddUser(userDal);

            return new AuthResult
            {
                Success = true,
                Token = token
            };
        }

        public async Task<AuthResult> LoginUser(UserLogin userLogin)
        {
            var existringUser = await manager.FindByNameAsync(userLogin.UserName);

            if (existringUser == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Error = new[] { "User not found" }
                };
            }

            var isCredentialsCorrect = await manager.CheckPasswordAsync(existringUser, userLogin.Password);
            if (!isCredentialsCorrect)
            {
                return new AuthResult
                {
                    Success = false,
                    Error = new[] { "invalid credentials" }
                };
            }

            var token = GetJwtToken(existringUser);
            
            return new AuthResult
            {
                Success = true,
                Token = token
            };
        }

        private string GetRole()
        {
            return "user";
        }

        private string GetJwtToken(IdentityUser user)
        {
            var key = Encoding.UTF8.GetBytes(settings.Key);

            // Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),

                Expires = DateTime.UtcNow.AddMinutes(settings.TokenTimeToLiveMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var jwtTokenaHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenaHandler.CreateToken(tokenDescriptor);
            return jwtTokenaHandler.WriteToken(token);
        }
    }
}