﻿using JonDJones.Core.Resources;
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
    public class SubscribeToContentSavingNotifacations
        : INotificationHandler<ContentSavingNotification>
    {
        public void Handle(ContentSavingNotification notification)
        {
            foreach (var node in notification.SavedEntities)
            {
                SetBlogPostDate(node);
            }
        }

        private void SetBlogPostDate(IContent node)
        {
            if (node.ContentType.Alias != Blog.ModelTypeAlias)
                return;

            var date = node.GetValue<DateTime?>(DocumentTypeConstants.PropertyAlias.PostDate);
            var unpublished = false;

            if (!date.HasValue && !node.IsCulturePublished("en-GB"))
            {
                // If the contents published use that date for sorting
                var postDate = node.GetPublishDate("en-GB");

                if (!postDate.HasValue)
                {
                    unpublished = true;

                    // If the content is scheduled, use the schedule data
                    var scheduledDate = node.ContentSchedule
                        .FullSchedule.FirstOrDefault(x => x.Action == ContentScheduleAction.Release);
                    if (scheduledDate != null)
                    {
                        postDate = scheduledDate.Date;
                    } else
                    {
                        // If content saved only, use the current time
                        postDate = DateTime.Now;
                    }
                }

                node.SetValue(DocumentTypeConstants.PropertyAlias.PostDate, postDate);
                node.SetValue(DocumentTypeConstants.PropertyAlias.UnpublishedTutorial, unpublished);
            }
        }

        public void Terminate(){}
    }
}