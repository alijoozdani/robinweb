using Microsoft.EntityFrameworkCore;
using RobinWeb.Core.Convertors;
using RobinWeb.Core.DTOs;
using RobinWeb.Core.Generator;
using RobinWeb.Core.SaveAndDelete;
using RobinWeb.Core.Security;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Context;
using RobinWeb.DataLayer.Entities;
using System.Reflection.Metadata;

namespace RobinWeb.Core.Services
{
    public class UserService : IUserService
    {
        private RobinWebDBContext _context;

        public UserService(RobinWebDBContext context)
        {
            _context = context;
        }


        public bool IsExistUserName(string userName)
        {
            return _context.UsersTbl.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _context.UsersTbl.Any(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.UsersTbl.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return _context.UsersTbl.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public User GetUserByEmail(string email)
        {
            return _context.UsersTbl.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByUserName(string username)
        {
            return _context.UsersTbl.SingleOrDefault(u => u.UserName == username);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public int GetUserIdByUserName(string username)
        {
            return _context.UsersTbl.Single(u => u.UserName == username).UserId;
        }

        public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.UsersTbl;

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            int take = 20;
            int skip = (pageId - 1) * take;

            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User();
            addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            addUser.ActiveCode = NameGenerator.GenerateUniqCode();
            addUser.Email = user.Email;
            addUser.IsActive = true;
            addUser.RegisterDate = DateTime.Now;
            addUser.UserName = user.UserName;
            addUser.UserAvatar = "Defult.jpg";
            #region SaveAvatar
            if (user.UserAvatar!=null)
            {
                var fileName = SaveFileInServer.SaveFile(user.UserAvatar, "wwwroot/img/userAvatar");
                addUser.UserAvatar = fileName;
            }
            #endregion

            return AddUser(addUser);
        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.UsersTbl.Where(u => u.UserId == userId).Select(u => new EditUserViewModel()
            {
                UserId=u.UserId,
                AvatarName=u.UserAvatar,
                Email=u.Email,
                UserName=u.UserName,
                IsActive=u.IsActive
            }).Single();
        }

        public User GetUserById(int userId)
        {
            return _context.UsersTbl.Find(userId);
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            user.IsActive = editUser.IsActive;

            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != "Defult.jpg")
                {
                    DeleteFileFromServer.DeleteFile(user.UserAvatar, "wwwroot/img/userAvatar");
                }

                //Save New Image
                var fileName = SaveFileInServer.SaveFile(editUser.UserAvatar, "wwwroot/img/userAvatar");
                user.UserAvatar = fileName;
            }

            UpdateUser(user);
        }

        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.UsersTbl.IgnoreQueryFilters().Where(u => u.IsDelete);

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public bool DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;

            UpdateUser(user);
            return true;
        }

        public bool RestoreUser(int userId)
        {
            var user = GetUserDeleted(userId);
            user.IsDelete = false;

            UpdateUser(user);
            return true;
        }

        public User GetUserDeleted(int userId)
        {
            return _context.UsersTbl.IgnoreQueryFilters().FirstOrDefault(u => u.UserId == userId);
        }
    }
}