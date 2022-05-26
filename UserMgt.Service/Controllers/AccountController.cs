using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Logger;
using UserMgt.Service.DtoModels;
using UserMgt.Shared.Entities.AspNetEntities;
using UserMgt.Service.Impl;

namespace UserMgt.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Route("[controller]")]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private readonly SignInManager<Aspnetusers> _signInManager;
        private readonly UserManager<Aspnetusers> _userManager;
        private IConfiguration _config;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILoggerManagerRepository _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        public AccountController(ILoggerManagerRepository logger, IRepositoryWrapper repository, SignInManager<Aspnetusers> signInManager, UserManager<Aspnetusers> userManager, IConfiguration config)
        {
            _logger = logger;
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            try
            {
                double tokenLifespan = double.Parse(_config["tokenlifespan"]);

                string baseURL = Convert.ToString(_config["baseURL"]);
                //string appBaseURL = (Url.Request.RequestUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped).TrimEnd('/') + System.Web.HttpContext.Current.Request.ApplicationPath).TrimEnd('/');
                ////string appURL = appBaseURL + "/Account/Login";
                //string appURL = "<a href='" + appBaseURL + "/Account/Login'>click here</a>";

                if (user == null)
                {
                    return BadRequest("Invalid client request");
                }

                var result = _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);


                //if (user.UserName == "test" && user.Password == "pass")
                if (result.Result.Succeeded)
                {
                    var cObj = new CustomClaims(_repository);
                    var authClaims = cObj.UserRolesGroupsClaims(user.UserName);

                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: baseURL,    //"https://localhost:44377",
                        audience: baseURL,  //"https://localhost:44377",
                                            //claims: new List<Claim>(),
                        claims: authClaims = authClaims.Count() > 0 ? authClaims : new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(tokenLifespan),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new
                    {
                        Token = tokenString,
                        expiration = tokeOptions.ValidTo
                    });
                }
                else
                {
                    //return Unauthorized();
                    _logger.LogWarn($"Invalid username or password");
                    return StatusCode(401, "Invalid username or password");
                }


                //var cObj = new CustomClaims(_repository);
                //var authClaims = cObj.UserRolesGroupsClaims(user.UserName);




            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Login action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Login action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
