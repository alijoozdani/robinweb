using RobinWeb.Core.DTOs;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        User GetUserDeleted(int userId);
        User GetUserByUserName(string username);
        void UpdateUser(User user);
        int GetUserIdByUserName(string username);
        bool DeleteUser(int userId);
        bool RestoreUser(int userId);

        #region AdminPanel
        UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        int AddUserFromAdmin(CreateUserViewModel user);
        EditUserViewModel GetUserForShowInEditMode(int userId);
        void EditUserFromAdmin(EditUserViewModel editUser);
        #endregion
    }
}
