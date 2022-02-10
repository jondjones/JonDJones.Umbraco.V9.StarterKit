using JonDJones.Core.ViewModel.Poco;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.Common.Models;

namespace JonDJones.Core.ViewComponents.MVC
{

    public class AuthenicatedComponent : ViewComponent
    {
        private IMemberManager _memberManager;

        public AuthenicatedComponent(
                IMemberManager memberManager
             )
        {
            _memberManager = memberManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var profileData = await _memberManager.GetCurrentMemberAsync();
            return View(new ProfileViewModel
                {
                    Name = profileData.Name,
                    Email = profileData.Email,
                    Comments = profileData.Comments,
                    Roles = profileData.Roles,
                }
            );
        }
    }
}
