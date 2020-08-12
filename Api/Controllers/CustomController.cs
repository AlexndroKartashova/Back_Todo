using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        private string GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals("id"));

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return claim.Value;
        }
    }
}
