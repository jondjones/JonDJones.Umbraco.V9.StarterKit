using JonDJones.Core.Interfaces;
using JonDJones.Core.ViewModel.Component;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace JonDJones.Core.Services
{
    public class BlogService : IBlogService
    {
        private IUmbracoContextAccessor _context;

        public BlogService(IUmbracoContextAccessor context)
        {
            _context = context;
        }

        public IEnumerable<BlogItemViewModel> Blogs => GetBlogs();

        private IEnumerable<BlogItemViewModel> GetBlogs()
        {
            var cache = _context.GetRequiredUmbracoContext();
            var home = cache.Content.GetAtRoot().DescendantsOrSelf<Home>().First();
            var listing = home.DescendantsOrSelf<BlogListing>().First();
            return listing.Descendants<Blog>().Select(ConvertToBlogItemViewModel);
        }

        private BlogItemViewModel ConvertToBlogItemViewModel(Blog blog)
        {
            return new BlogItemViewModel
            {
                Title = blog.Title,
                ImageUrl = blog?.Image?.Url() ?? string.Empty,
                LinkUrl = blog.Url(),
                Content = new HtmlString(blog.Content.ToString())
            };
        }
    }
}
