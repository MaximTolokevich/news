using news.Interfaces;
using news.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace news.Repository
{
    public class AuthorRepository : IRepository<Author>, IChangePassword
    {
        private NewsContext context;
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
            if (findAuthor == null || oldPass!=findAuthor.Password)
            {
                return false;
            }
            findAuthor.Password = newPass;
            context.SaveChanges();
            return true;
        }

        bool IRepository<Author>.Create(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException();
            }
            var checkUniqueAuthor = context.Authors.Where(x => x.Email == author.Email);
            if (checkUniqueAuthor != null)
            {
                return false;
            }
            Author newAuthor = new Author
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                FullName = author.FullName,
                Password = author.Password

            };
            context.Authors.Add(newAuthor);
            context.SaveChanges();
            return true;
        }

        bool IRepository<Author>.Delete(int Id)
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

        IEnumerable<Author> IRepository<Author>.GetAll()
        {
            return context.Authors;
        }

        Author IRepository<Author>.Get(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return context.Authors.First(x => x.Id == id);
        }

        bool IRepository<Author>.Update(Author author)
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
            findAuthor.FullName = author.LastName;
            findAuthor.Email = author.Email;
            return true;
        }
    }
}
