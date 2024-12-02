
using API.Business.DTO;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly TokenService _tokenService;

        private readonly UserManager<User> _userManager;
        public AccountController(UserManager<User> userManager, TokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return Unauthorized();

            // var userBasket = await RetrieveBasket(loginDTO.Username);
            // var anonBasket = await RetrieveBasket(Request.Cookies["buyerId"]);

            // if (anonBasket != null)
            // {
            //     if (userBasket != null) _context.Baskets.Remove(userBasket);
            //     anonBasket.BuyerId = user.UserName;
            //     Response.Cookies.Delete("buyerId");
            //     await _context.SaveChangesAsync();
            // }

            // return new UserDTO
            // {
            //     Email = user.Email,
            //     Token = await _tokenService.GenerateToken(user),
            //     Basket = anonBasket != null ? anonBasket.MapBasketToDto() : userBasket?.MapBasketToDto()
            // };
            return new UserDTO
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO registerDTO)

        {

            var user = new User { UserName = registerDTO.UserName, Email = registerDTO.Email };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return StatusCode(201);

        }
        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);


            return new UserDTO
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
            };
        }
    }


}