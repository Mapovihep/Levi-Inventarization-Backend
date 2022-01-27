using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReactASPCore.Models;
using ReactASPCore.EmployeesData;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Text;
using LeviInventarizationBackend.ContainerConfiguration;
using System.Security.Cryptography;
/*using Newtonsoft.Json;*/

namespace ReactASPCore.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private EmployeeData _authData = new EmployeeData();

        private GetJwtSettings settingsJwt;

        public LoginController(GetJwtSettings settings)
        {
            settingsJwt = settings;
        }

        /*[HttpDelete]
        [Route("api/deleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, [FromHeader] string Authorization)
        {
            return Ok(await _authData.DeleteEmployee(id, Authorization));
        }*/

        [HttpPost]
        [AllowAnonymous]
        [Route("api/sign_up")]
        public async Task<IActionResult> RegistrationNew([FromBody] Employee employee)
        {
            Employee? withHashedPass = await _authData.Registration(employee);
            if (withHashedPass != null)
            {
                try
                {
                    return Ok("Registration succeed - you can enter with mail " + withHashedPass.Email);
                }
                catch (Exception ex)
                {
                    return BadRequest("Invalid User" + ex);
                }
            }
            return BadRequest("User has been already found");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/sign_in")]
        public async Task<IActionResult> LoginNew([FromBody] Employee employee)
        {
            Employee? withHashedPass = await _authData.Login(employee);
            if (withHashedPass != null)
            {
                var token = await GenerateJwtToken(withHashedPass.Password, withHashedPass.Email);
                try
                {
                    return Ok(token);
                }
                catch (Exception ex)
                {
                    return BadRequest("Invalid User" + ex);
                }
            }
            return BadRequest("User was not found");
        }

        private async Task<string> GenerateJwtToken(string password, string mail)
        {
            string[] jwtInfo = settingsJwt.GetJWT();

            List<Claim> claims = new List<Claim>() {
            new Claim(ClaimTypes.Email, mail),
            new Claim(ClaimTypes.Hash, password),
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo[0]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken SecurityToken = new JwtSecurityToken(
                issuer: jwtInfo[1],
                audience: jwtInfo[2],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
                );

            return await Task.Run<string>(() =>
            {
                return jwtTokenHandler.WriteToken(SecurityToken);
            });
        }

        private ClaimsIdentity SetClaims(string mail, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, mail),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, password)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims);
                return claimsIdentity;
            }
            return null;
        }
    }
}

