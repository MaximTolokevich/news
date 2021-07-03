using news.Interfaces;
using news.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news.Repository
{
    public class NewsRepository : IRepository<News>
    {
        private NewsContext context;
        public NewsRepository(NewsContext newsContext) 
        {
            context = newsContext;
        }
        bool IRepository<News>.Create(News news)
        {
            if (news== null)
            {
                return false;
            }
            News addNew = new News
            {
                newsContent = news.newsContent,
                DateNews = news.DateNews
            };
            context.News.Add(addNew);
            context.SaveChanges();
            return true;
        }

        bool IRepository<News>.Delete(int Id)
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

        News IRepository<News>.Get(int Id)
        {
            if (Id<1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return context.News.First(x=>x.Id==Id);
        }

        IEnumerable<News> IRepository<News>.GetAll()
        {
            return context.News;
        }

        bool IRepository<News>.Update(News news)
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
            return true;
        }
    }
}
