namespace ProjectNotifier.XPlace.WebServer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    [Route("[controller]")]
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


        [HttpPost("UpdateUserPreferences/{projectTypes}")]
        [Authorize()]
        public async Task<IActionResult> UpdateUserPreferencesAsync(IEnumerable<ProjectTypes> projectTypes)
        {
            // Find the user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            // Update user project preferences
            _appDBContext.Users.Find(user.Id).UserProjectPreferences = new List<UserProjectPreference>(
                projectTypes.Select(projectType => 
                new UserProjectPreference()
                {
                    ProjectType = projectType,
                    User = user,
                }));

            await _appDBContext.SaveChangesAsync();

            return Ok();
        }
    }
}