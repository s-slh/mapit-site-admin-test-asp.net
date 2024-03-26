using MapitSiteAdmin.Areas.Identity.Data;

namespace MapitSiteAdmin.ViewsModels
{
    public class ManageUserRolesViewModel
    {
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public IList<UserRolesViewModel>? UserRoles { get; set; }
    }
    public class UserRolesViewModel
    {
        public string? RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
