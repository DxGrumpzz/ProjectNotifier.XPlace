namespace ProjectNotifier.XPlace.WebServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectNotifier.XPlace.Core;

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public AccountController()
        {

        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Content("Account index");
        }


        [HttpGet("index/{num}")]
        public IActionResult Index(int num)
        {
            return Content($"Account index, {num}");
        }


        [HttpPost("Login")]
        public IActionResult LoginAsync(LoginModel loginModel)
        {
            return Content($"Login: {loginModel.Username}");
        }

        [HttpPost("Register")]
        public IActionResult LoginAsync(RegisterModel registerModel)
        {
            return Content($"Register: {registerModel.Username}");
        }

    }
}