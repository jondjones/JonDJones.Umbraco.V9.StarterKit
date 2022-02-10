using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Models;

namespace JonDJones.Core.ViewComponents.MVC
{

    public class AnonymousComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new LoginModel());
        }
    }
}
