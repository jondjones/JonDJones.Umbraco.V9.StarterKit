using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace JonDJones.Core.ViewModel.Poco
{
    public class XmlSitemapViewModel
    {
        public List<SitemapItemViewModel> SitemapItems { get; set; }

        public string Url { get; set; }

        public HttpContext Context { get; internal set; }
    }
}
