using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;

namespace JonDJones.Core.Controller
{
    /// <summary>
    /// umbraco/backoffice/Backend/index
    /// </summary>
    public class BackendController : UmbracoAuthorizedController
    {
        public IActionResult Index()
        {
            return Content("Hello from authorized controller");
        }
    }
}