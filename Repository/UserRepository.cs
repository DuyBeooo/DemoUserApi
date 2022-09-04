using MyAPI3.DBContext;
using MyAPI3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAPI3.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext _context;

        public UserRepository(MyDBContext context)
        {
            _context = context;
        }

        public bool AddUser(User user)
        {
            try
            {
                var newUser = new User()
                {
                    Username = user.Username,
                    Password = user.Password,
                    Roll = user.Roll
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(string id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(id));
                if (user == null)
                    return false;
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditUser(User user)
        {
            try
            {
                var editUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (editUser == null)
                    return false;
                editUser.Username = user.Username;
                editUser.Password = user.Password;
                editUser.Roll = user.Roll;
                _context.Users.Update(editUser);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetAll()
        {
            var list = _context.Users.ToList();
            return list;
        }

        public User GetUserById(string id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(id));
                if (user == null)
                    return null;
                return user;
            }
            catch
            {
                return null;
            }

        }

        public User UserLogin(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }
    }
}
