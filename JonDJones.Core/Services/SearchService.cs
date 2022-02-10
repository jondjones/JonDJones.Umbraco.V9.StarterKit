using Examine;
using Examine.Search;
using JonDJones.Core.Interfaces;
using JonDJones.Core.Resources;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;
using static Umbraco.Cms.Core.Constants;

namespace JonDJones.Core.Services
{
        public class SearchService : ISearchService
        {
            private IExamineManager _examineManager;
            private ILogger<SearchService> _logger;
            private IUmbracoContextAccessor _context;
            private IContentService _contentService;

            public SearchService(
                IExamineManager examineManager,
                ILogger<SearchService> logger,
                IUmbracoContextAccessor context,
                IContentService contentService)
            {
                _examineManager = examineManager;
                _logger = logger;
                _context = context;
                _contentService = contentService;
            }

            public IEnumerable<IPublishedContent> QueryUmbraco(string searchTerm)
            {
                IEnumerable<IPublishedContent> results = null;

                try
                {
                    if (!_examineManager.TryGetIndex(UmbracoIndexes.ExternalIndexName, out var index) || !(index is IUmbracoIndex umbIndex))
                    {
                        throw new InvalidOperationException($"No index found by name ExternalIndex or is not of type {typeof(IUmbracoIndex)}");
                    }

                var searcher = index.Searcher;
                    var resultsAsSearchItems = searcher.CreateQuery(IndexTypes.Content)
                        .Field("nodeName", searchTerm)
                        //.And().Field("__NodeTypeAlias", Blog.ModelTypeAlias)
                        //.And().GroupedOr(new[] { "nodeName" }, searchTerm.Split(' '))
                        //.And().ParentId(1105).And().Field("mainContent", searchTerm)
                        //.OrderByDescending(new SortableField[] { new SortableField("customField") })
                        .Execute();

                var sortedResults = resultsAsSearchItems.OrderByDescending(x => x.Score);
                    results = sortedResults.Select(x => GetUmbracoObject(int.Parse(x.Id)));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "QueryUmbraco Error");
                }

                return results;
            }

            public IEnumerable<IContent> QueryInternalUmbracoIndexForUnPublishedPages()
            {
                IEnumerable<IContent> results = null;

                try
                {
                    if (!_examineManager.TryGetIndex(UmbracoIndexes.InternalIndexName, out var index) || !(index is IUmbracoIndex umbIndex))
                    {
                        throw new InvalidOperationException($"No index found by name ${UmbracoIndexes.InternalIndexName} or is not of type {typeof(IUmbracoIndex)}");
                    }

                    var searcher = index.Searcher;
                    var resultsAsSearchItems = searcher.CreateQuery(IndexTypes.Content)
                        .NodeTypeAlias(Blog.ModelTypeAlias)
                        .OrderBy(new SortableField[] { new SortableField(DocumentTypeConstants.PropertyAlias.PostDate) })
                        .Execute();

                    results = resultsAsSearchItems.Select(x => GetUmbracoInternel(int.Parse(x.Id)));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "QueryUmbraco Error");
                }

                return results;
            }

            private IContent GetUmbracoInternel(int id)
            {
                return _contentService.GetById(id);
            }

            private IPublishedContent GetUmbracoObject(int id)
            {
                var cache = _context.GetRequiredUmbracoContext();
                return cache.Content.GetById(id);
            }
    }
}