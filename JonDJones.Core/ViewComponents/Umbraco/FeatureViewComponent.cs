using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JonDJones.Core.ViewComponents
{
    public class featureViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
