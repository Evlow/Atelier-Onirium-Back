using API.Business.DTO;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Ajout de l'espace de noms pour le logging

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly TokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger; // Déclaration du logger

        // Injection du logger dans le constructeur
        public AccountController(UserManager<User> userManager, TokenService tokenService, ILogger<AccountController> logger)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _logger = logger;
        }

        // Méthode pour l'enregistrement d'un utilisateur
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO registerDTO)
        {
            _logger.LogInformation("Attempting to register user with username: {UserName}", registerDTO.UserName); // Log d'information

            var user = new User { UserName = registerDTO.UserName, Email = registerDTO.Email };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                    _logger.LogError("Registration failed for username: {UserName}, Error: {Error}", registerDTO.UserName, error.Description); // Log d'erreur
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Member");

            _logger.LogInformation("User {UserName} successfully registered", registerDTO.UserName); // Log d'information
            return StatusCode(201);
        }

        // Méthode pour la connexion de l'utilisateur
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            _logger.LogInformation("Attempting login for user: {UserName}", loginDTO.UserName); // Log d'information

            var user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (user == null)
            {
                _logger.LogWarning("User not found for login attempt: {UserName}", loginDTO.UserName); // Log d'avertissement
                return Unauthorized();
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                _logger.LogWarning("Invalid password for user: {UserName}", loginDTO.UserName); // Log d'avertissement
                return Unauthorized();
            }

            _logger.LogInformation("User {UserName} successfully logged in", loginDTO.UserName); // Log d'information

            return new UserDTO
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }

        // Méthode pour obtenir l'utilisateur actuel (authentifié)
        [Authorize]
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
