using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JonDJones.Core.ViewComponents
{
    public class contactUsFormViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
