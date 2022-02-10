using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace JonDJones.Core.Controller
{
    /// <summary>
    /// /umbraco/backoffice/api/secure/index
    /// </summary>
    public class SecureController : UmbracoAuthorizedApiController
    {
        public IActionResult Index()
        {
            return Content("This is secure");
        }
    }
}