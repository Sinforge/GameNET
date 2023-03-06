using System.Security.Claims;
using AccountService.Data;
using AccountService.Dtos;
using AccountService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserRepo _userRepo;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;
    public AccountController(UserRepo userRepo, ILogger<AccountController> logger, IMapper mapper)
    {
        _userRepo = userRepo;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("/registration")]
    public ActionResult CreateUser(UserCreateDto userCreateDto)
    {


        User? user = _userRepo.FindByEmailAndId(userCreateDto.Email, userCreateDto.UserId);
        if (user != null)
        {
            _logger.LogInformation("User with such Id or Email exist");
            return BadRequest("User with such id exist");
        }
        user = _mapper.Map<User>(userCreateDto);
        _userRepo.CreateUser(user);
        _userRepo.SaveChanges();
        _logger.LogInformation("User successful registered");
        return Ok();
    }


    [HttpPost("/login")]
    public async Task<ActionResult> Login(HttpContext ctx, string password, string userId)
    {

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userId),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, Role.DefaultUser)
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await ctx.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));

        return Ok();

    }
