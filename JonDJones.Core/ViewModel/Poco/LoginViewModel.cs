using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JonDJones.Core.ViewModel.Poco
{
    public class ProfileViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Comments { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
