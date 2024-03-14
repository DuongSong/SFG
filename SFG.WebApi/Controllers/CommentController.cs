using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SFG.WebApi.Controllers
{
    [Authorize]
    [Route("sfg/comment")]
    public class CommentController : Controller
    {
        
    }
}

