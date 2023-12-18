using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.DTOs;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Web.Pages.Admin.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel UserForAdminViewModel { get; set; }
        public void OnGet(int pageId=1,string filterEmail="",string filterUserName="")
        {
            UserForAdminViewModel = _userService.GetUsers(pageId,filterEmail,filterUserName);
        }
        public IActionResult OnGetDeleteUser(int userId)
        {
            var res = _userService.DeleteUser(userId);
            return res ? Content("Deleted") : Content("Error");
        }
    }
}
