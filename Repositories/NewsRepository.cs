using Microsoft.EntityFrameworkCore;
using news.Repositories.Data;
using news.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace news.Repositories
{
    public class NewsRepository : IRepository<News>
    {
        private readonly NewsContext context;
        
        public NewsRepository(NewsContext newsContext) 
        {
            context = newsContext;
        }
        public bool Create(News news)
        {
            if (news== null)
            {
                return false;
            }

            List<Author> list = new List<Author>();
            foreach (var item in news.NewsAuthors)
            {
                context.Authors.Attach(item);
                list.Add(context.Authors.Local.Single(x => x.Id == item.Id));
            }
            Category category = news.Category;
            context.Categories.Attach(category);
            news.Category = category;
            news.NewsAuthors = list;

            context.News.Add(news);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            if (Id<1)
            {
                return false;
            }
            var CheckNewsexist = context.News.Find(Id);
            if (CheckNewsexist==null)
            {
                return false;
            }
            context.News.Remove(CheckNewsexist);
            context.SaveChanges();
            return true;
        }

        public News Get(int Id)
        {
            if (Id<1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return context.News.Include(x=>x.Category)
                .Include(x=>x.NewsAuthors)
                .First(x=>x.Id==Id);
        }

        public IQueryable<News> GetAll()
        {
            
            return context.News.Include(x => x.Category)
                .Include(x=>x.NewsAuthors);
                
        }

        public bool Update(News news)
        {
            if (news==null)
            {
                throw new ArgumentNullException();
            }
            var CheckNewExist = context.News.Find(news.Id);
            if (CheckNewExist==null)
            {
                return false;
            }
            CheckNewExist.newsContent = news.newsContent;
            CheckNewExist.DateNews = news.DateNews;
            context.SaveChanges();
            return true;
        }
    }
}
                       