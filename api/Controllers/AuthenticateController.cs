using AutoMapper;
using Forum.Dtos;
using Forum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized("n ai parola buna");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(ApplicationUserRegisterDto registerDto)
        {
            if (await userManager.FindByNameAsync(registerDto.Username) != null)
            {
                return Conflict("Username already exists");
            }
            if (await userManager.FindByEmailAsync(registerDto.Email) != null)
            {
                return Conflict("Email already in use");
            }

            var user = CreateUser(registerDto);
            var registrationResult = await userManager.CreateAsync(user, registerDto.Password);

            if (!registrationResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "User creation failed" });
            }

            return Ok(_mapper.Map<ApplicationUserReadDto>(user));
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin(ApplicationUserRegisterDto registerDto)
        {
            if (await userManager.FindByNameAsync(registerDto.Username) != null)
            {
                return Conflict("Username already exists");
            }
            if (await userManager.FindByEmailAsync(registerDto.Email) != null)
            {
                return Conflict("Email already in use");
            }

            var user = CreateUser(registerDto);
            var result = await userManager.CreateAsync(user, registerDto.Password);
            
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "User creation failed" });
            }

            AddAdminRole(user);

            return Ok(_mapper.Map<ApplicationUserReadDto>(user));
        }

        [NonAction]
        private ApplicationUser CreateUser(ApplicationUserRegisterDto registerDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username
            };

            return user;
        }

        [NonAction]
        private async void AddAdminRole(ApplicationUser user)
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
        }
    }
}