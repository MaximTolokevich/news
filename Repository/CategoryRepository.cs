using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news.Interfaces;
using news.Models;

namespace news.Repository
{
    public class CategoryRepository : IRepository<Category>

    {
        private NewsContext context;
        public CategoryRepository(NewsContext newsContext)
        {
            context = newsContext;
        }
        bool IRepository<Category>.Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            var CheckCategoryExist = context.Categories.Find(category.CategoryName);
            if (CheckCategoryExist!=null)
            {
                return false;
            }
            Category newCategory = new Category
            {
                CategoryName = category.CategoryName
            };
            context.Categories.Add(category);
            context.SaveChanges();
            return true;
        }

        bool IRepository<Category>.Delete(int Id)
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
            context.Remove(CheckCategoryExist);
            context.SaveChanges();
            return true;
        }

        Category IRepository<Category>.Get(int Id)
        {
            return  context.Categories.First(x => x.Id == Id);
        }

        IEnumerable<Category> IRepository<Category>.GetAll()
        {
            return context.Categories;
        }

        bool IRepository<Category>.Update(Category category)
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
            return true;
        }
    }
}
