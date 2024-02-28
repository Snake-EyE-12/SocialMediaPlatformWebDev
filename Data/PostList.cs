using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Data
{
    public class PostList : DataAccessLayer<Post>
    {
        ApplicationDbContext db;
        public PostList(ApplicationDbContext context)
        {
            db = context;
        }

        public void Add(Post entity)
        {
            db.Posts.Add(entity);
            db.SaveChanges();
        }

        public Post Get(int id)
        {
            Post? found = db.Posts.FirstOrDefault(p => p.Id == id);
            return found;
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts;
        }

        public void Remove(int id)
        {
            Post? foundMovie = Get(id);
            if (foundMovie != null)
            {
                db.Posts.Remove(foundMovie);
                db.SaveChanges();
            }
        }

        public IEnumerable<Post> Search(string filters)
        {
            throw new NotImplementedException();
        }

        public void Update(Post entity)
        {
            db.Posts.Update(entity);
            db.SaveChanges();
        }
    }
}
