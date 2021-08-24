using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using news.Repositories.Data;
using news.Repositories.Models;

namespace news.Repositories
{
    public class CategoryRepository : IRepository<Category>

    {
        private readonly NewsContext _context;
        public CategoryRepository(NewsContext newsContext)
        {
            _context = newsContext;
        }
        public bool Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            var CheckCategoryExist = _context.Categories.Find(category.Id);
            if (CheckCategoryExist!=null)
            {
                return false;
            }
            Category newCategory = new Category
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            if (Id<1)
            {
                return false;
            }
            var CheckCategoryExist = _context.Categories.Find(Id);
             
            if (CheckCategoryExist == null)
            {
                return false;
            }

            _context.Categories.Remove(CheckCategoryExist);
            _context.SaveChanges();
            return true;
        }

        public Category Get(int Id)
        {
            return _context.Categories.First(x => x.Id == Id);
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public bool Update(Category category)
        {
            if (category==  null)
            {
                return false;
            }
            var findAuthor = _context.Categories.Find(category.Id);
            if (findAuthor== null)
            {
                return false;
            }
            findAuthor.CategoryName = category.CategoryName;
            _context.SaveChanges();
            return true;
        }
    }
}
