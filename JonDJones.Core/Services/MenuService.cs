using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace JonDJones.Core.Services
{
    public class MenuService : IMenuService
    {
        private IUmbracoContextAccessor _context;

        public MenuService(IUmbracoContextAccessor context)
        {
            _context = context;
        }

        public IEnumerable<MenuItem> Menus => GetMenuItems();

        private IEnumerable<MenuItem> GetMenuItems()
        {
            var cache = _context.GetRequiredUmbracoContext();
            var container = cache.Content.GetAtRoot().DescendantsOrSelf<MenuConainer>().First();
            return container.Children<MenuItem>();
        }
    }
}
