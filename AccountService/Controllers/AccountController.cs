using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AccountService.Data;
using AccountService.Dtos;
using AccountService.Models;
using Microsoft.Extensions.Options;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using AccountService.Services;

namespace AccountService.Controllers;

[ApiController]
public class AccountController : Controller
{
    private readonly IUserRepo _userRepo;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;
    private readonly IOptions<Audience> _settings;
   // private readonly IMessageReceiver _messageReceiver;
    public AccountController(IUserRepo userRepo, ILogger<AccountController> logger, IMapper mapper, IOptions<Audience> settings) //IMessageReceiver messageReceiver)
    {
        //_messageReceiver = messageReceiver;
        _settings = settings;
        _userRepo = userRepo;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("/helloworld")]
    public ActionResult HelloWorld()
    {
        return Content("HEllo world");
    }




    [HttpPost("/Registration")]
    public async  Task<ActionResult> CreateUser(UserCreateDto userCreateDto, bool isAdmin=false)
    {


        User? user = await _userRepo.FindByEmailAndId(userCreateDto.Email, userCreateDto.UserId);
        if (user != null)
        {
            _logger.LogInformation("User with such Id or Email exist");
            return BadRequest("User with such id exist");
        }

        user = _mapper.Map<User>(userCreateDto);
        user.Role = isAdmin ? Role.Admin : Role.DefaultUser;
        await _userRepo.CreateUser(user);
        _logger.LogInformation("User successful registered");
        return Ok();
    }


    /// <summary>
    /// Get Jwt Token
    /// </summary>
    /// <remark>
    /// Sample of request:
    /// {
    ///     "passowrd": "string"
    ///     "userId": "string"
    /// }
    /// </remark>
    /// <returns>
    /// {
    ///     "access_token" : "YOUR JWT token"
    ///     "expires_in": "Time of expire"
    /// }
    /// </returns>
    /// 


    [HttpGet("/login")]
    public async Task<ActionResult> Login(string password, string userId)
    {
        User user = await _userRepo.FindById(userId);
        if (user != null && user.Password == password)
        {


            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userId),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, Role.DefaultUser)
            };
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = _settings.Value.Iss,
                ValidateAudience = true,
                ValidAudience = _settings.Value.Aud,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            var jwt = new JwtSecurityToken(
                issuer: _settings.Value.Iss,
                audience: _settings.Value.Aud,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJWT,
                expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds
            };
            return Json(response);
        }
        else
        {
            return BadRequest();
        }

    }

    [HttpGet]
    [Authorize(Roles= "admin")]
    [Route("/user")]
    public async Task<IActionResult> GetUserDataById(string userId)
    {
        return Ok(await _userRepo.GetUserDataById(userId));
    }


    [HttpPost]
    [Route("/subscribe")]
    public async Task<IActionResult> SubscribeToUser(string userId1, string userId2)
    {
        _logger.LogInformation("subsribing to user");
        await _userRepo.SubsribeToUser(userId1, userId2);
        return Ok();
    }
}
public class Audience
{
    public string Secret { get; set; }
    public string Iss { get; set; }
    public string Aud { get; set; }
}