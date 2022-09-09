using Examine;
using JonDJones.Core.Resources;
using Microsoft.Extensions.Logging;
using System;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Web.Common.PublishedModels;
using static Umbraco.Cms.Core.Constants;

namespace JonDJones.Core.Components
{
    public class ExamineIndexManager : IComponent
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private readonly IExamineManager _examineManager;
        private readonly ILogger<ExamineIndexManager> _logger;

        public ExamineIndexManager(
            IExamineManager examineManager,
            ILogger<ExamineIndexManager> logger,
            IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
            _examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                if (!_examineManager.TryGetIndex(UmbracoIndexes.InternalIndexName, out var index) || !(index is IUmbracoIndex umbIndex))
                {
                    throw new InvalidOperationException($"No index found by name ExternalIndex or is not of type {typeof(IUmbracoIndex)}");
                }

                index.TransformingIndexValues += IndexProviderTransformingIndexValues;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExamineIndexManager:Initialize");
            }
        }

        private void IndexProviderTransformingIndexValues(object sender, IndexingItemEventArgs args)
        {
            // args.ValueSet.Add("key", "data);
            if (args.ValueSet.ItemType == Blog.ModelTypeAlias)
            {
                using var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext();
            }
        }

        public void Terminate()
        {
        }
    }
}
