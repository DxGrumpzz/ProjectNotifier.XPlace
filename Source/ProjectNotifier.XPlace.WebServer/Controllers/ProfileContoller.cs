namespace ProjectNotifier.XPlace.WebServer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using ProjectNotifier.XPlace.Core;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route(ApiRoutes.API_PROFILE_CONTROLLER)]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private readonly UserManager<AppUserModel> _userManager;
        private readonly AppDBContext _appDBContext;


        public ProfileController(UserManager<AppUserModel> userManager, AppDBContext appDBContext)
        {
            _userManager = userManager;
            _appDBContext = appDBContext;
        }


        [HttpPost(ApiRoutes.API_PROFILE_UPDATE_USER_PREFERENCES + "/{ProjectTypes}")]
        [Authorize()]
        public async Task<IActionResult> UpdateUserPreferencesAsync(IEnumerable<ProjectType> ProjectTypes)
        {
            // Find the user
            var user = _appDBContext.Users.Find((await _userManager.GetUserAsync(HttpContext.User)).Id);

            // Load-up user's project preferences
            _appDBContext.UserProjectPreferences
                // Find the user in question
                .Where(row => row.User == user)
                // Load it's project preferences
                .Include(p => p)
                // Activate query
                .ToList();

            // If user has preferences
            if(user.UserProjectPreferences != null)
                // Remove current preferences
                _appDBContext.UserProjectPreferences.RemoveRange(user.UserProjectPreferences);

            // Update user project preferences
            user.UserProjectPreferences = new List<UserProjectPreference>(
                ProjectTypes.Select(projectType => 
                new UserProjectPreference()
                {
                    ProjectType = projectType,
                    User = user,
                }));

            // Update database
            await _appDBContext.SaveChangesAsync();


            return Ok();
        }
    }
}