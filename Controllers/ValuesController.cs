using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShottBowing;

namespace ShottBowing.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [Route("Validate")]
        [HttpGet]
        public ResponseVM Validate(string token, string username)
        {
            int UserId = BowingSecurity.GetUser(username);
            if (UserId == 0) return new ResponseVM
            {
                Status = "Invalid",
                Message = "Invalid User."
            };
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
            {
                return new ResponseVM
                {
                    Status = "Success",
                    Message = "OK",
                };
            }
            return new ResponseVM
            {
                Status = "Invalid",
                Message = "Invalid Token."
            };
        }
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

        [HttpGet]
        public  object GenerateToken()
        {
            byte[] key = Convert.FromBase64String(Secret);// secrete kye
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {//payload
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, "admin")}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature) //signature using Algo
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public object GetToken()
        {
            var issuer = "http://mysite.com";
            var secretekey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var credentials = new SigningCredentials(secretekey, SecurityAlgorithms.HmacSha256);
            var permclaim = new List<Claim>();
            permclaim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permclaim.Add(new Claim("valid","1"));
            permclaim.Add(new Claim("userid", "1"));
            permclaim.Add(new Claim("name","surendra"));

            var token = new JwtSecurityToken(issuer,issuer,permclaim,expires: DateTime.Now.AddDays(1),signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new {data= jwt_token };
         }

    }
}
