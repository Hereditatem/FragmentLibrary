using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FragmentLibrary.Controllers
{
    [Route("api/[controller]")]
    public class FragmentController : Controller
    {
        // GET: /<controller>/
        public IEnumerable< Index()
        {
            return View();
        }
    }
}
