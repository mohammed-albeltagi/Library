using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Library.DTO;
using Library.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationSettings _appSettings;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ApplicationUserController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> PostUserData(UserDTO model)
        {
            try
            {

            var user = await _userManager.FindByNameAsync(model.UserName);
            var chkPass = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && chkPass)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });

            }
            catch (Exception)
            {

                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> RegisterUserData(UserDTO model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    // Copy data from RegisterViewModel to IdentityUser
                    var user = new IdentityUser
                    {
                        UserName = model.UserName
                    };

                    // Store user data in AspNetUsers database table
                    var result = await _userManager.CreateAsync(user, model.Password);

                    // If user is successfully created, sign-in the user using
                    // SignInManager and redirect to index action of HomeController
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        if (user != null)
                        {
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                        new Claim("UserID",user.Id.ToString())
                                }),
                                Expires = DateTime.UtcNow.AddDays(1),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                            };
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                            var token = tokenHandler.WriteToken(securityToken);
                            return Ok(new { token });
                        }
                    }

                    // If there are any errors, add them to the ModelState object
                    // which will be displayed by the validation summary tag helper
                    if(result.Errors.Any())
                    {
                        return BadRequest();
                    }
                }

                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

    }
}