using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using news.Services;
using news.Controllers.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using news.Services.Models;

namespace news.Controllers
{
    public class NewsController : Controller
    {
        
        private readonly IService<News> _service;
        private readonly IService<Services.Models.Category> _categoryService;
        private readonly IService<Services.Models.Author> _authorService;
        private readonly IMapper _map;

        public NewsController(IService<News> Service, IService<Services.Models.Category> CategoryService, IService<Services.Models.Author> AuthorService,IMapper mapper)
        {
            _service = Service;
            _categoryService = CategoryService;
            _authorService =AuthorService ;
            _map = mapper;
            
        }

        // GET: News
        public IActionResult Index()
        {
            var model = _map.Map<IEnumerable<News>,
                            IEnumerable<GetOptionsListsViewcs> >(_service.GetAll());
            return View(model);
        }

        // GET: News/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _map.Map< News,
                                GetOptionsListsViewcs> (_service.GetAll()
                                                            .FirstOrDefault(m => m.Id == id));
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            GetOptionsListsViewcs getOptions = new GetOptionsListsViewcs();   
            getOptions.CategoryList = new SelectList(_categoryService.GetAll(),
                                                        nameof(Models.Category.Id),
                                                        nameof(Models.Category.CategoryName));
            getOptions.NewsAuthors = _map.Map<IEnumerable<Services.Models.Author>,
                                                IEnumerable<Models.Author>> (_authorService.GetAll());
            return View(getOptions);
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( GetOptionsListsViewcs news)
        {
            var authors = _map.Map<IEnumerable<Services.Models.Author>, IEnumerable<Models.Author>>(_authorService.GetAll().Where(x => news.SelectedAuthors.Any(y => y == x.Id))).Select(x => x);
            news.NewsAuthors = authors;
            var category = _map.Map<IEnumerable<Services.Models.Category>, IEnumerable<Models.Category>>(_categoryService.GetAll().Where(x => news.CategoryId == x.Id));
            news.Category = category.First();
            var model = _map.Map<GetOptionsListsViewcs, News>(news);
            if (ModelState.IsValid)

            {      
                _service.Create(model);
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        // GET: News/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _map.Map<News, GetOptionsListsViewcs>(_service.Get((int)id));
            
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GetOptionsListsViewcs news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }
            var model = _map.Map<GetOptionsListsViewcs, News >(news);
            if (ModelState.IsValid)
            {
                
                try
                {
                    _service.Update(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(model.Id))
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
            return View(model);
        }

        // GET: News/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _map.Map<News, GetOptionsListsViewcs>(_service.GetAll().FirstOrDefault(m => m.Id == id));
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var model = _map.Map<News, GetOptionsListsViewcs>(_service.Get(id));
            _service.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            var model = _map.Map<IEnumerable<News>, IEnumerable<GetOptionsListsViewcs>>(_service.GetAll());
            return model.Any(x => x.Id == id);
        }
    }
}
