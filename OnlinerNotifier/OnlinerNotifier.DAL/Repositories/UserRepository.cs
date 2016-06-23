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

        public IEnumerable<User> GetAllDeep()
        {
            return db.Users.Include(usr => usr.UserProducts.Select(up => up.Product).Select(prod => prod.PriceChanges));
        }

        public User Get(int id)
        {
            return db.Users.Include(usr => usr.UserProducts.Select(up => up.Product)).SingleOrDefault(x => x.Id == id);
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public bool Update(User item)
        {
            if (db.Users.Contains(item))
            {
                db.Entry(item).State = EntityState.Modified;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                return true;
            }
            return false;
        }
    }
}
