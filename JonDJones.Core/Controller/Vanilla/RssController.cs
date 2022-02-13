using JonDJones.Core.Interfaces;
using JonDJones.Core.Services;
using JonDJones.Core.ViewModel.Component;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Umbraco.Extensions;

namespace JonDJones.Core.Controller.Vanilla
{
    public class RssController : Microsoft.AspNetCore.Mvc.Controller
    {
        IRssBuilderService _rssBuilderService;

        string FeedDescription = "Jon D Jones CMS Tutorials";
        string FeedUrl = "rss";
        string FeedName = "Jon D Jones";
        string AuthorEmail = "test@test.com";

        public RssController(
            IRssBuilderService rssBuilderService)
        {
            _rssBuilderService = rssBuilderService;
        }

        public IActionResult Index()
        {
            var feed = _rssBuilderService.CreateRssFeed(FeedName, FeedDescription, FeedUrl, AuthorEmail);

            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(stream, GetSettings()))
                {
                    var rssFormatter = new Rss20FeedFormatter(feed, false);
                    rssFormatter.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }

                // Could also use application/atom+xml or application/rss+xml
                return File(stream.ToArray(), "text/xml; charset=utf-8");
            }
        }

        private XmlWriterSettings GetSettings()
        {
            return new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true
            };
        }
    }
}
