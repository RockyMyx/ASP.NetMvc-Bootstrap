namespace MvcBootstrap.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public bool? CanAddRootNode { get; set; }
        public bool? CanAddChildNode { get; set; }
        public bool? CanRenameNode { get; set; }
        public bool? CanDeleteNode { get; set; }
        public bool? CanAddResource { get; set; }
        public bool? CanUpdateResource { get; set; }
        public bool? CanDeleteResource { get; set; }
    }
}