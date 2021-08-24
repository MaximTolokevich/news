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
        private readonly NewsContext _context;
        
        public NewsRepository(NewsContext newsContext) 
        {
            _context = newsContext;
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
                _context.Authors.Attach(item);
                list.Add(_context.Authors.Local.Single(x => x.Id == item.Id));
            }
            Category category = news.Category;
            _context.Categories.Attach(category);
            news.Category = category;
            news.NewsAuthors = list;

            _context.News.Add(news);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            if (Id<1)
            {
                return false;
            }
            var CheckNewsexist = _context.News.Find(Id);
            if (CheckNewsexist==null)
            {
                return false;
            }
            _context.News.Remove(CheckNewsexist);
            _context.SaveChanges();
            return true;
        }

        public News Get(int Id)
        {
            if (Id<1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _context.News.Include(x=>x.Category)
                .Include(x=>x.NewsAuthors)
                .First(x=>x.Id==Id);
        }

        public IQueryable<News> GetAll()
        {
            
            return _context.News.Include(x => x.Category)
                .Include(x=>x.NewsAuthors);
                
        }

        public bool Update(News news)
        {
            if (news==null)
            {
                throw new ArgumentNullException();
            }
            var CheckNewExist = _context.News.Find(news.Id);
            if (CheckNewExist==null)
            {
                return false;
            }
            CheckNewExist.newsContent = news.newsContent;
            CheckNewExist.DateNews = news.DateNews;
            _context.SaveChanges();
            return true;
        }
    }
}
                       