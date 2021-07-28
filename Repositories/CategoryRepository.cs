using System;
using System.Linq;
using news.Repositories.Data;
using news.Repositories.Models;

namespace news.Repositories
{
    public class CategoryRepository : IRepository<Category>

    {
        private NewsContext context;
        public CategoryRepository(NewsContext newsContext)
        {
            context = newsContext;
        }
        public bool Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            var CheckCategoryExist = context.Categories.Find(category.Id);
            if (CheckCategoryExist!=null)
            {
                return false;
            }
            Category newCategory = new Category
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
            context.Categories.Add(newCategory);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            if (Id<1)
            {
                return false;
            }
            var CheckCategoryExist = context.Categories.Find(Id);
             
            if (CheckCategoryExist == null)
            {
                return false;
            }
            
            context.Categories.Remove(CheckCategoryExist);
            context.SaveChanges();
            return true;
        }

        public Category Get(int Id)
        {
            return  context.Categories.First(x => x.Id == Id);
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;
        }

        public bool Update(Category category)
        {
            if (category==  null)
            {
                return false;
            }
            var findAuthor = context.Categories.Find(category.Id);
            if (findAuthor== null)
            {
                return false;
            }
            findAuthor.CategoryName = category.CategoryName;
            context.SaveChanges();
            return true;
        }
    }
}
