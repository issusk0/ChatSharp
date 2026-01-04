using ChatSharp.Classes;
using ChatSharp.Services;
using Microsoft.AspNetCore.Mvc;


namespace ChatSharp.Controllers
{

    [ApiController]
    [Route("chat")]

    public class ChatController : ControllerBase
    {
        [HttpPost]
        public IActionResult UpdateChat([FromBody]Chat chat, Message message)
        {
            var service = CServices.Instance;
            
            if (!service.SendMessageChat(chat,message))
            {
                return BadRequest("Failed to send the message to the chat");
            }
            
            bool result = true;

            return Ok(result);
        }
    }
    


}