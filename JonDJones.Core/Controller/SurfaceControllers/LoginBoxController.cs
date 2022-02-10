using JonDJones.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Models;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Extensions;

namespace JonDJones.Core.Controller.SurfaceControllers
{
    public class LoginBoxController : SurfaceController
    {
        IMemberSignInManager _memberSignInManager;
        IMemberManager _memberManager;
        ISettingsService _settingsService;

        public LoginBoxController(
            AppCaches appCaches,
            ServiceContext services,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            IMemberManager memberManager,
            IMemberSignInManager memberSignInManager,
            ISettingsService settingsService)
                : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _memberManager = memberManager;
            _memberSignInManager = memberSignInManager;
            _settingsService = settingsService;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticate(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Entry Denied Pal!");
                return CurrentUmbracoPage();
            }

            var isValid = await _memberManager.ValidateCredentialsAsync(model.Username, model.Password);
            if (isValid)
            {
                var isSignedResult = await _memberSignInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    true,
                    false);

                if (isSignedResult.Succeeded)
                {
                    return Redirect(_settingsService.SettingsPage.MemberPageUrl.Url());

                } else if (isSignedResult.IsLockedOut)
                {
                    // do something
                }

            }

            return CurrentUmbracoPage();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _memberSignInManager.SignOutAsync();
            return RedirectToCurrentUmbracoPage();
        }
    }
}
