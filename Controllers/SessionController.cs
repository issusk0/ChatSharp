using ChatSharp.Classes;
using ChatSharp.Services;
using Microsoft.AspNetCore.Mvc;


namespace ChatSharp.Controllers
{
    [ApiController]
    [Route("login")]

    public class Login : ControllerBase
    {

        [HttpPost]
        public IActionResult Login_function([FromBody]Users users)
        {
            var uservice = new UServices();
            var sservice = new SService();

            if(!uservice.validate_function(users.Username, users.password_hash))
            {
                
                return StatusCode(401);

            }

            string token = TokenGeneratorService.GenerateSecureToken();


            var session_object = new Sessions(users.User_Id, token, 24 );
            if (!sservice.save_session(session_object))
            {
                return StatusCode(401);
            }

            return Ok(new
            {
                token = token,
                expiresAt= session_object.expires_at,
                userId = users.User_Id
            });
        }

    }



}