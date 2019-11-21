namespace ProjectNotifier.XPlace.WebServer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
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
                projectTypes.Select(projectType => 
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