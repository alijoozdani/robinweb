using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.DTOs;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Web.Pages.Admin.Users
{
    [Authorize]
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        public CreateUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            int userId = _userService.AddUserFromAdmin(CreateUserViewModel);


            return Redirect("/Admin/Users");
        }
    }
}
