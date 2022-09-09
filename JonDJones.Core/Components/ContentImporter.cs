using JonDJones.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Services
{


    public class ContentImporter : IComponent
    {
        private IContentService _contentService;
        private ISettingsService _settingsService;

        public ContentImporter(
            IContentService contentService,
            ISettingsService settingsService)
        {
            _contentService = contentService;
            _settingsService = settingsService;
        }

        public void Initialize()
        {
            var myJsonString = System.IO.File.ReadAllText(@"example.json");
            var myJObject = JObject.Parse(myJsonString);

            var getParentItem = _settingsService?.Homepage?.Key;

            // ImportContent(myJObject, getParentItem);
        }

        public void ImportContent(JObject importedContent, Guid parentGuid)
        {
            if (importedContent == null)
            {
                return;
            }

            var newPage = _contentService.Create(
                "New Node Name",
                parentGuid,
                Blog.ModelTypeAlias);

            // Add Properties Here
            newPage.SetValue("Title", importedContent.Value<string>("name"));

            _contentService.SaveAndPublish(newPage);

        }

        public void Terminate()
        {
        }
    }
}
