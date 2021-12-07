using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace JonDJones.Core.Components
{
    public class LogPushlishNotification : INotificationHandler<ContentPublishingNotification>
    {
        private ILogger<LogPushlishNotification> _logger;

        public LogPushlishNotification(ILogger<LogPushlishNotification> logger)
        {
            _logger = logger;
        }

        public void Handle(ContentPublishingNotification notification)
        {
            foreach (var node in notification.PublishedEntities)
            {
                if (node.ContentType.Alias.Equals("home"))
                {
                    _logger.LogError($"{node.Id} has been published");
                }
            }
        }
    }
}
