using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using news.Controllers.Models;
using news.Services;
using AutoMapper;

namespace news.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IService<Services.Models.Category> service;
        private readonly IMapper map;

        public CategoriesController(IService<Services.Models.Category> _service, IMapper _mapper)
        {
            service = _service;
            map = _mapper;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var a = map.Map<IEnumerable<Services.Models.Category>, IEnumerable<Category>>(service.GetAll());
            return View(a);
        }

        // GET: Categories/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var a = map.Map<Services.Models.Category, Category>(service.GetAll()
                .FirstOrDefault(m => m.Id == id));  
            if (a == null)
            {
                return NotFound();
            }
            return View(a);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                var a = map.Map<Category, Services.Models.Category>(category);
                service.Create(a);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var a = map.Map<Services.Models.Category, Category>(service.Get((int)id));
            if (a == null)
            {
                return NotFound();
            }
            return View(a);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CategoryName")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            var a = map.Map<Category, Services.Models.Category>(category);
            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(a);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(a.Id))
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

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var a = map.Map<Services.Models.Category, Category>(service.GetAll().FirstOrDefault(m => m.Id == id));
            if (a == null)
            {
                return NotFound();
            }

            return View(a);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var a = map.Map<Services.Models.Category, Category>(service.Get(id));
            service.Delete(a.Id);
            return (RedirectToAction(nameof(Index)));
        }

        private bool CategoryExists(int id)
        {
            var a = map.Map<IEnumerable<Services.Models.Category>, IEnumerable<Category>>(service.GetAll());
            return a.Any(x => x.Id == id);
        }
    }
}
