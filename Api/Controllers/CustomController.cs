using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CustomController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string GetUserId()
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
