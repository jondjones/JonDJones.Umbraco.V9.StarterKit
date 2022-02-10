using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace JonDJones.Core.ViewComponents
{
    public class MetaDataViewComponent : ViewComponent
    {
        private IUmbracoContextAccessor _context;

        public MetaDataViewComponent(IUmbracoContextAccessor context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var content = _context.GetRequiredUmbracoContext().PublishedRequest.PublishedContent;
            var metaData = new MetaDataViewModel
            {
                Title = content.Name
            };

            return View(metaData);
        }
    }
}
