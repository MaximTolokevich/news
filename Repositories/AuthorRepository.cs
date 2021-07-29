using AutoMapper;
using news.Repositories.Data;
using news.Repositories.Models;
using System;
using System.Linq;

namespace news.Repositories
{
    public class AuthorRepository : IRepository<Author>, IChangePassword
    {
        private readonly NewsContext context;
        
        public AuthorRepository(NewsContext newsContext)
        {
            context = newsContext;
        }

        public bool ChangePass(int id, string oldPass, string newPass)
        {
            if (id < 1 || string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass))
            {
                return false;
            }
            var findAuthor = context.Authors.Find(id);
            if (findAuthor == null || oldPass != findAuthor.Password)
            {
                return false;
            }
            findAuthor.Password = newPass;
            context.SaveChanges();
            return true;
        }

        public bool Create(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException();
            }
            var checkUniqueAuthor = context.Authors.Find(author.Id);
            if (checkUniqueAuthor != null)
            {
                return false;
            }
            author.FullName = author.FirstName + " " + author.LastName; 
            context.Authors.Add(author);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            if (Id < 1)
            {
                return false;
            }
            var findAuthor = context.Authors.Find(Id);
            if (findAuthor == null)
            {
                return false;
            }
            context.Authors.Remove(findAuthor);
            context.SaveChanges();
            return true;
        }

        public IQueryable<Author> GetAll()
        {

            return context.Authors;
        }

        public Author Get(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return context.Authors.First(x => x.Id == id);
        }

        public bool Update(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException();
            }
            var findAuthor = context.Authors.Find(author.Id);
            if (findAuthor == null)
            {
                return false;
            }
            findAuthor.FirstName = author.FirstName;
            findAuthor.LastName = author.LastName;
            findAuthor.FullName = author.FirstName + " " + author.LastName;
            findAuthor.Email = author.Email;
            findAuthor.Password = author.Password;

            context.SaveChanges();
            return true;
        }
    }
}
