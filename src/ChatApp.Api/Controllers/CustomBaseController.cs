using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        protected int GetUserId()
        {
            if (HttpContext.Items["userId"] is not null)
            {

                int userId1 = Convert.ToInt32(HttpContext.Items["userId"]);
                return userId1;
            }
            var userId = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            return Convert.ToInt32(userId);
        }
    }
}
