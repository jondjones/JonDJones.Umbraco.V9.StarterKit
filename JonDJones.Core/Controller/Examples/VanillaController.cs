using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace JonDJones.Core.Controller
{
    /// <summary>
    /// /vanilla
    /// </summary>
    public class VanillaController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}