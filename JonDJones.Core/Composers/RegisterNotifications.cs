using JonDJones.Core.Components;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace JonDJones.Core.Composers
{
    public class RegisterNotifications : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            // Register Notifcation Handlers
            builder.AddNotificationHandler<ContentSavingNotification, SubscribeToContentSavingNotifacations>();
            builder.AddNotificationHandler<ContentPublishedNotification, SubscribeToContentPublishedNotifacations>();

            // Register Components
            builder.Components().Append<ExamineIndexManager>();
        }
    }
}
