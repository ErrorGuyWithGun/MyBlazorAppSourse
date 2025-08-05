using System;
using System.Collections.Generic;
using System.Linq;
using MyBlazorAppSourse.Models;
using MyBlazorAppSourse.Data;

namespace MyBlazorAppSourse.Services
{
    public class UserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetAll() => _db.Users.ToList();

        public User GetById(Guid id) => _db.Users.Find(id);

        public void Add(User user)
        {
            user.Id = Guid.NewGuid();
            user.LastSeen = DateTime.UtcNow;
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }
    }
}