using JonDJones.Core.Interfaces;
using JonDJones.Core.ViewModel.Poco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JonDJones.Core.Services
{
    public class XmlSitemapService : IXmlSitemapService
    {
        private ISettingsService _settingsService;
        private IHttpContextAccessor _httpContextAccessor;

        public XmlSitemapService(
            ISettingsService settingsService,
            IHttpContextAccessor httpContextAccessor)
        {
            _settingsService = settingsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public XmlSitemapViewModel GetXmlSitemapViewModel()
        {
            return new XmlSitemapViewModel
            {
                SitemapItems = GetSitemapItems(),
                Context = _httpContextAccessor.HttpContext,
                Url = _httpContextAccessor.HttpContext.Request.GetDisplayUrl()
            };
        }

        private List<SitemapItemViewModel> GetSitemapItems()
        {
            var results = _settingsService.Homepage;
            var list = new List<SitemapItemViewModel>();

            foreach (var result in results.Descendants())
            {
                var dateString = XmlConvert.ToString(result.UpdateDate, XmlDateTimeSerializationMode.Utc);

                var item = new SitemapItemViewModel
                {
                    Url = result.Url(null, UrlMode.Absolute),
                    Date = dateString
                };

                list.Add(item);
            }

            return list;
        }
    }
}