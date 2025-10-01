using CoworkingReservation.Data;
using CoworkingReservation.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingReservation.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AppDBContext _context { get; set; }
        private JwtTokenCreator _jwtTokenCreator { get; set; }
        public AuthController(AppDBContext context, JwtTokenCreator jwtTokenCreator)
        {
            _context = context;
            _jwtTokenCreator = jwtTokenCreator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] inputUserData inputUserData)
        {
            AuthUserData? account = _context.AuthUsersData.FirstOrDefault(q => q.UserName == inputUserData.UserName);
            if(account != null)
            {

                var passHash = new PasswordHasher<AuthUserData>().VerifyHashedPassword(account, account.PasswordHash, inputUserData.Password);
                if (passHash == PasswordVerificationResult.Success || passHash == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    return Ok(_jwtTokenCreator.GenerateNewToken(account));
                }
                else
                {

                    return BadRequest("Username or password is wrong");
                    
                }
            }
            else
            {
                return NotFound("Username doesnt exist");
            }
                


            
        }
    }
}
