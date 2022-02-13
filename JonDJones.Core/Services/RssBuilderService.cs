using JonDJones.Core.Interfaces;
using JonDJones.Core.ViewModel.Component;
using JonDJones.Core.ViewModel.Poco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JonDJones.Core.Services
{
    public class RssBuilderService : IRssBuilderService
    {
        private ISettingsService _settingsService;
        private IBlogService _blogService;

        public RssBuilderService(
            ISettingsService settingsService,
            IBlogService blogService)
        {
            _settingsService = settingsService;
            _blogService = blogService;
        }

        public SyndicationFeed CreateRssFeed(
            string feedName,
            string description,
            string feedUrl,
            string authorEmail)
        {
            var feedBaseUrl = $"{_settingsService.HompageAbsoluteUrl}{feedUrl}";
            var uri = new Uri(feedBaseUrl);

            var person = new SyndicationPerson(
                authorEmail,
                feedName,
                _settingsService.HompageAbsoluteUrl);

            var feed = new SyndicationFeed()
            {
                Title = new TextSyndicationContent(feedName),
                Description = new TextSyndicationContent(description),
                BaseUri = uri,
                LastUpdatedTime = DateTime.Now,
                Language = "en-us",
                Items = _blogService.Blogs.Select(x => ConvertToSyndicationItem(x, person))
            };

            XNamespace atom = "http://www.w3.org/2005/Atom";
            feed.AttributeExtensions.Add(
                new XmlQualifiedName(
                    "atom",
                    XNamespace.Xmlns.NamespaceName),
                    atom.NamespaceName);

            feed.ElementExtensions.Add(
                new XElement(atom + "link",
                new XAttribute("href", feedBaseUrl),
                new XAttribute("rel", "self"),
                new XAttribute("type", "application/rss+xml")));

            var formatter = feed.GetRss20Formatter();
            formatter.SerializeExtensionsAsAtom = false;

            return formatter.Feed;
        }

        private SyndicationItem ConvertToSyndicationItem(
            BlogItemViewModel item,
            SyndicationPerson person)
        {
            var uri = new Uri(item.LinkUrl);

            var stripped = StringHelper.StripHtml(item.Content.ToString());
            var content = StringHelper.CreatePreviewText(stripped);

            var syndicationItem = new SyndicationItem
            {
                Title = new TextSyndicationContent(item.Title),
                Id = item.Id,
                Content = new TextSyndicationContent(content),
                BaseUri = uri,
                PublishDate = item.Date,
            };

            syndicationItem.Authors.Add(person);

            var link = new SyndicationLink(uri);
            syndicationItem.Links.Add(link);
            syndicationItem.AddPermalink(uri);

            var category = new SyndicationCategory("Blogs");
            syndicationItem.Categories.Add(category);

            return syndicationItem;
        }
    }
}