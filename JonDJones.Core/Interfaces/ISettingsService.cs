﻿using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Interfaces
{
    public interface ISettingsService
    {
        Settings SettingsPage { get; }

        Home Homepage { get; }

        string HompageAbsoluteUrl { get; }
    }
}