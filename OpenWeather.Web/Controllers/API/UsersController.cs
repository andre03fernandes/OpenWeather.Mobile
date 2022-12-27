using Microsoft.AspNetCore.Mvc;
using OpenWeather.Web.Data;
using OpenWeather.Web.Data.Entities;
using OpenWeather.Web.Helpers;
using OpenWeather.Web.Models;
using System.Threading.Tasks;

namespace OpenWeather.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public UsersController(DataContext context, IUserHelper userHelper, IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        // GET: api/Users/
        [HttpGet]
        [Route("{email}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            return await _userHelper.GetUserByEmailAsync(email);

        }

        [HttpGet]
        [Route("{email}/{password}")]
        public async Task<bool> CheckifUserExist(string email, string password)
        {
            return await _userHelper.CheckifUserAndPasswordIsCorrect(email, password);

        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(RegisterNewUserMobileViewModel user)
        {
            var newuser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Email,
                Email = user.Email
            };

            await _userHelper.AddUserAsync(newuser, user.Password);

            string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(newuser);

            string tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userid = newuser.Id,
                token = myToken
            }, protocol: HttpContext.Request.Scheme);

            Response response = _mailHelper.SendEmail(newuser.Email, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                       $"To allow the user, " +
                       $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

            if (response.IsSuccess)
            {
                return CreatedAtAction("GetUser", new { id = newuser.Id }, newuser);
            }

            return CreatedAtAction("GetUser", new { id = newuser.Id }, newuser);
        }

    }
}
