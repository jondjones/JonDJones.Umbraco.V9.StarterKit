using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JonDJones.Core.ViewModel.Page
{
    public class SearchViewModel
    {
        public IEnumerable<IPublishedContent> Results { get; internal set; }
        public bool DisplayResults { get; internal set; }
    }
}
