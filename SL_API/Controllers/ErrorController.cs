using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SL_API.Errors;

namespace SL_API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController:BaseApiController
    {
        public IActionResult Error(int code){
            return new ObjectResult(new ApiResponse(code));
        }
    }
}