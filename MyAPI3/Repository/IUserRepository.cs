using MyAPI3.Models;
using System.Collections.Generic;

namespace MyAPI3.Repository
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetUserById(string id);
        bool AddUser(User user);
        bool EditUser(User user);
        bool DeleteUser(string id);
        User UserLogin(string username, string password);
    }
}
