namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectNotifier.XPlace.Core;
    using System.Collections.Generic;
    using System.Linq;

    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly ProjectList _projectList;

        public ProjectsController(ProjectList projectList)
        {
            _projectList = projectList;
        }

            
        [HttpGet("{count?}")]
        public IEnumerable<ProjectModel> Get(int count = 100)
        {
            // Get some projects
            return _projectList.Projects.Take(count);
        }

    };
};