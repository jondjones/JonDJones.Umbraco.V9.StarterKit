using System.Collections.Generic;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.ViewComponents
{
    public class HeaderViewModel
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}