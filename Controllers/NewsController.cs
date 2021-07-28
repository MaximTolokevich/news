using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using news.Services;
using news.Controllers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using news.Services.Models;

namespace news.Controllers
{
    public class NewsController : Controller
    {
        private readonly IService<News> Service;
        private readonly IService<Services.Models.Category> CategoryService;
        private readonly IService<Services.Models.Author> AuthorService;
        private readonly IMapper map;

        public NewsController(IService<News> _service, IService<Services.Models.Category> _CategoryService, IService<Services.Models.Author> _AuthorService,IMapper mapper)
        {
            Service = _service;
            CategoryService = _CategoryService;
            AuthorService = _AuthorService;
            map = mapper;
        }

        // GET: News
        public IActionResult Index()
        {
            var a = map.Map<IEnumerable<Services.Models.News>, IEnumerable<NewsViewModel> >(Service.GetAll());
            return View(a);
        }

        // GET: News/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var a = map.Map< News, NewsViewModel> (Service.GetAll().FirstOrDefault(m => m.Id == id));
            
            if (a == null)
            {
                return NotFound();
            }

            return View(a);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            //ViewBag.Category = CategoryService.GetAll()
            //    .Select(x => new SelectListItem
            //    {
            //        Text = x.CategoryName,
            //        Value = x.CategoryId.ToString()
            //    }).ToList();
            ViewBag.Category = new SelectList(CategoryService.GetAll(), "CategoryId", "CategoryName");

            var authors =  AuthorService.GetAll()
                .Select(a => new
                {
                    a.Id,
                    a.FullName
                }).ToList();
            ViewBag.Authors = new MultiSelectList(authors, "Id", "FullName");

            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( NewsViewModel news)
        {

            if (ModelState.IsValid)

            {
                var a = map.Map<  NewsViewModel, News> (news);
                Service.Create(a);
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(news);
        }

        // GET: News/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var a = map.Map<News, NewsViewModel >(Service.Get((int)id));
            
            if (a == null)
            {
                return NotFound();
            }
            return View(a);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  NewsViewModel news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }
            var a = map.Map<NewsViewModel,News >(news);
            if (ModelState.IsValid)
            {
                
                try
                {
                    Service.Update(a);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(a);
        }

        // GET: News/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news =  Service.GetAll()
                .FirstOrDefault(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return Service.GetAll().Any(x => x.Id == id);
        }
    }
}
