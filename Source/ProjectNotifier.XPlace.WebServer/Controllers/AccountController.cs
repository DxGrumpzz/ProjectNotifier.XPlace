namespace ProjectNotifier.XPlace.WebServer.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IPasswordValidator<AppUserModel> _passwordValidator;

        public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager, IPasswordValidator<AppUserModel> passwordValidator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordValidator = passwordValidator;
        }


        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Content("Account index");
        }


        [HttpPost("Login")]
        public async Task<ActionResult<AppUserModel>> LoginAsync(LoginModel loginModel)
        {
            // Attemp user sing in
            var singInResult = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, true, false);

            // If login failed
            if (singInResult.Succeeded == false)
            {
                // Return a result indicating that the login failed
                return new ContentResult()
                {
                    Content = "Username or password was incorrect",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };
            }
            // Login was succesfull
            else
            {
                // Find user by username and return the user's data
                return await _userManager.FindByNameAsync(loginModel.Username);
            };
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel registerModel)
        {
            // Check if password and confirmation password match
            if (registerModel.Password.Equals(registerModel.ConfirmationPassword, StringComparison.Ordinal) == false)
            {
                return new ContentResult()
                {
                    Content = "Password doesn't match confirmation password",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };
            };

            var userModel = new AppUserModel()
            {
                UserName = registerModel.Username,
            };

            // Attemp to register the user
            var createResult = await _userManager.CreateAsync(userModel, registerModel.Password);


            // If user creation failed
            if (createResult.Succeeded == false)
            {
                // Return a result containing registration erorrs failed
                return new ContentResult()
                {
                    Content = $"Registration failed. {Environment.NewLine}{createResult.Errors.ToErrorString()}",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };
            }
            // Creation was succesfull
            else
            {
                // Find user by username and return the user's data
                return Content("Registerd succesfully");
            };
        }

    }
}