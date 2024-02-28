using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Data
{
    public class ProfileList : DataAccessLayer<Profile>
    {
        ApplicationDbContext db;
        public ProfileList(ApplicationDbContext context)
        {
            db = context;
        }

        public void Add(Profile entity)
        {
            db.Profiles.Add(entity);
            db.SaveChanges();
        }

        public Profile Get(int id)
        {
            Profile? found = db.Profiles.FirstOrDefault(p => p.Id == id);
            return found;
        }

        public IEnumerable<Profile> GetAll()
        {
            return db.Profiles;
        }

        public void Remove(int id)
        {
            Profile? foundMovie = Get(id);
            if (foundMovie != null)
            {
                db.Profiles.Remove(foundMovie);
                db.SaveChanges();
            }
        }

        public IEnumerable<Profile> Search(string filters)
        {
            if (filters == null) return GetAll();
            return GetAll().Where(x => x.Name.ToLower().Contains(filters.ToLower()));
        }

        public void Update(Profile entity)
        {
            db.Profiles.Update(entity);
            db.SaveChanges();
        }
    }
}
