namespace ProjectNotifier.XPlace.WebServer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectNotifier.XPlace.Core;

    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly ProjectList _projectList;
        private readonly AppDBContext _appDBContext;

        public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager, ProjectList projectList, AppDBContext appDBContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _projectList = projectList;
            _appDBContext = appDBContext;
        }



        [HttpPost("Login/{loginModel}")]
        public async Task<ActionResult<LoginResponseModel>> LoginAsync(LoginRequestModel loginModel)
        {
            // Attemp user sing in
            var singInResult = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);

            // If login failed
            if (singInResult.Succeeded == false)
            {
                // Return a result indicating that the login failed
                return new ContentResult()
                {
                    Content = "התחברות נכשלה." + $"{Environment.NewLine}" + "שם המשתמש או סיסמא אינם נכונים.",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };
            }
            // Login was succesfull
            else
            {
                // Find user by username and return the user's data
                var user = await _userManager.FindByNameAsync(loginModel.Username);

                // Load user project preferences
                _appDBContext.UserProjectPreferences.Include(property => property.User)
                    // Where the project in question references the current user
                    .Where(project => project.User == user)
                    // "Activate" the query
                    .ToList();


                return new LoginResponseModel()
                {
                    // Create user profile
                    UserProfile = new UserProfileModel()
                    {
                        // Assign user properties
                        Username = user.UserName,
                        UserID = user.Id,
                        // Only retrieve project preferences if it isn't null
                        UserProjectPreferences = user.UserProjectPreferences?.Select(projctType => projctType.ProjectType),
                    },

                    // Load projects
                    Projects = _projectList.Projects,
                };
            };
        }


        [HttpGet("Login")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<LoginResponseModel>> CookieLoginAsync()
        {
            // Find user using cookie/connection context
            var user = await _userManager.GetUserAsync(HttpContext.User);

            // Load user project preferences
            _appDBContext.UserProjectPreferences.Include(property => property.User)
                // Where the project in question references the current user
                .Where(project => project.User == user)
                // "Activate" the query
                .ToList();

            return new LoginResponseModel()
            {
                // Create user profile
                UserProfile = new UserProfileModel()
                {
                    Username = user.UserName,
                    UserID = user.Id,
                    // Only retrieve project preferences if it isn't null
                    UserProjectPreferences = user.UserProjectPreferences?.Select(projctType => projctType.ProjectType),
                },

                // Load projects
                Projects = _projectList.Projects,
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
                    Content = "הרשמה נכשלה." + $"{Environment.NewLine}{new HebrewIdentityErrorDescriber().PasswordMismatch().Description}",
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
                    Content = "הרשמה נכשלה." + $"{Environment.NewLine}{createResult.Errors.ToErrorString()}",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };
            }
            // Creation was succesfull
            else
            {
                // Add newly registered user to User role
                await _userManager.AddToRoleAsync(userModel, "User");

                // Add user id claim to new user
                await _userManager.AddClaimAsync(userModel, new Claim("UserID", userModel.Id));


                return Content("נרשמת בהצלחה");
            };
        }

    }
}