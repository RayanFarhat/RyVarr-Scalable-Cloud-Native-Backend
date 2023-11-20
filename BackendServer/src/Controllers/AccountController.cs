using BackendServer.Authentication;
using BackendServer.DB;
using BackendServer.DistributedGrains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountDataCache accountDataCache;

    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration,
     RyvarrDb db, IClusterClient clusterClient)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        _configuration = configuration;
        accountDataCache = new AccountDataCache(clusterClient, db, userManager);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null && user.UserName != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.DateOfBirth, DateTime.Now.AddDays(7).ToString("dd-MM-yyyy hh:mm:ss")),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                status = 200,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Username already exists!" });

        userExists = await userManager.FindByEmailAsync(model.Email);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email already exists!" });

        var user = new IdentityUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await userManager.CreateAsync(user, model.Password);

        await CreateRoles();
        //add the user role as User
        await userManager.AddToRoleAsync(user, UserRoles.User);

        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        // when all ok add to Acountdata with isPro == false
        await accountDataCache.AddOrUpdate(new DTOs.AccountData(user.Id, user.UserName, false, DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy hh:mm:ss")));
        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAccountData()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        string expireDateString = User.FindFirst(ClaimTypes.DateOfBirth)?.Value!;
        DateTime expireDate = DateTime.ParseExact(expireDateString, "dd-MM-yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        if (expireDate < DateTime.Now)
            // date has passed.
            return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "Error", Message = "Your token has been expired !!!" });
        else
            // the date is in the future
            // then get the data
            return Ok(await accountDataCache.Get(userId));
    }

    private async Task CreateRoles()
    {
        // create roles if they are not exist
        if (!await roleManager.RoleExistsAsync(UserRoles.UserPro))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.UserPro));
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
    }
}