using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.DTOs;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Web.Pages.Admin.Users
{
    [Authorize]
    public class ListDeleteUsersModel : PageModel
    {
        private IUserService _userService;
        public ListDeleteUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel UserForAdminViewModel { get; set; }

        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = _userService.GetDeleteUsers(pageId, filterEmail, filterUserName);
        }

        public IActionResult OnGetRestoreUser(int userId)
        {
            var res = _userService.RestoreUser(userId);
            return res ? Content("Restored") : Content("Error");
        }
    }
}
