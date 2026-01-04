using ChatSharp.Classes;
using ChatSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatSharp.Controllers
{
    [ApiController]
    [Route("send")]


    public class Send : ControllerBase{

        [HttpPost]
        public IActionResult SendMessage([FromBody] Message requests)
        {
            var service = MServices.Instance;


            if (!service.SendMessage(requests)){
                return BadRequest("FATAL ERROR NO VALID DATA");
            };
            
            bool result = true;

            return Ok(result);


        }

    }

}