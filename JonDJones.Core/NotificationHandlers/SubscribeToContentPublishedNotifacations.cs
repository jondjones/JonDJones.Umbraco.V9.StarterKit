using JonDJones.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Components
{
    public class SubscribeToContentPublishedNotifacations : INotificationHandler<ContentPublishedNotification>
    {
        public void Handle(ContentPublishedNotification notification)
        {
            foreach (var node in notification.PublishedEntities)
            {
                if (node.ContentType.Alias != Blog.ModelTypeAlias)
                    continue;

                node.SetValue(DocumentTypeConstants.PropertyAlias.UnpublishedTutorial, false, "en-us");
            }
        }


        public void Terminate(){}
    }
}
