using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private Context db;

        public UserRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Include(usr => usr.Products).SingleOrDefault(x => x.Id == id);
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User book = db.Users.Find(id);
            if (book != null)
            {
                db.Users.Remove(book);
            }
        }
    }
}
