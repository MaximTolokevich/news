using AutoMapper;
using news.Repositories.Data;
using news.Repositories.Models;
using System;
using System.Linq;

namespace news.Repositories
{
    public class AuthorRepository : IRepository<Author>, IPasswordManager
    {
        private readonly NewsContext _context;
        
        public AuthorRepository(NewsContext newsContext)
        {
            _context = newsContext;
        }

        public bool ChangePassword(int id, string oldPass, string newPass)
        {
            if (id < 1 || string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass))
            {
                return false;
            }
            var findAuthor = _context.Authors.Find(id);
            if (findAuthor == null || oldPass != findAuthor.Password)
            {
                return false;
            }
            findAuthor.Password = newPass;
            _context.SaveChanges();
            return true;
        }

        public bool Create(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException();
            }
            var checkUniqueAuthor = _context.Authors.Find(author.Id);
            if (checkUniqueAuthor != null)
            {
                return false;
            }
            author.FullName = author.FirstName + " " + author.LastName;
            _context.Authors.Add(author);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            if (Id < 1)
            {
                return false;
            }
            var findAuthor = _context.Authors.Find(Id);
            if (findAuthor == null)
            {
                return false;
            }
            _context.Authors.Remove(findAuthor);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<Author> GetAll()
        {

            return _context.Authors;
        }

        public Author Get(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _context.Authors.First(x => x.Id == id);
        }

        public bool Update(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException();
            }
            var findAuthor = _context.Authors.Find(author.Id);
            if (findAuthor == null)
            {
                return false;
            }
            findAuthor.FirstName = author.FirstName;
            findAuthor.LastName = author.LastName;
            findAuthor.FullName = author.FirstName + " " + author.LastName;
            findAuthor.Email = author.Email;
            findAuthor.Password = author.Password;

            _context.SaveChanges();
            return true;
        }
    }
}
