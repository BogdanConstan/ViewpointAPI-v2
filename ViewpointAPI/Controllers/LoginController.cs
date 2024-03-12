using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ViewpointAPI.Models;


namespace ViewpointAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly string _jwtIssuer;
        private readonly string _jwtKey;
        public LoginController(IConfiguration config) 
        {
            _config = config;
            _jwtIssuer = Environment.GetEnvironmentVariable("ISSUER");
            _jwtKey = Environment.GetEnvironmentVariable("KEY");
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {

            if (loginRequest.Username != "testname" || loginRequest.Password != "testpassword") 
            {
                return BadRequest("Invalid credentials");
            }

            else {

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(_jwtIssuer,
                _jwtIssuer,
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

                var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return Ok(token);
            }

        }
    }
}