using JonDJones.Core.Components;
using JonDJones.Core.ContentFinder;
using JonDJones.Core.Section;
using JonDJones.Core.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace JonDJones.Core.Composers
{
    public class RegisterUmbracoComponents : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            // Register Notification Handlers
            builder.AddNotificationHandler<ContentSavingNotification, SubscribeToContentSavingNotifacations>();
            builder.AddNotificationHandler<ContentPublishedNotification, SubscribeToContentPublishedNotifacations>();

            // Register components
            builder.Components().Append<ExamineIndexManager>();

            // Example of how to import content on start-up
            builder.Components().Append<ContentImporter>();

            // Example of to register last chance finder
            builder.SetContentLastChanceFinder<PageNotFoundContentFinder>();

            // Registering a section
            builder.Sections().Insert<MyCustomSection>(1);
        }
    }
}
