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
        private readonly IService<Services.Models.Category> _service;
        private readonly IMapper _map;

        public CategoriesController(IService<Services.Models.Category> service, IMapper mapper)
        {
            _service = service;
            _map = mapper;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var model = _map.Map<IEnumerable<Services.Models.Category>, IEnumerable<Category>>(_service.GetAll());
            return View(model);
        }

        // GET: Categories/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _map.Map<Services.Models.Category, Category>(_service.GetAll()
                .FirstOrDefault(m => m.Id == id));  
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
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
                var model = _map.Map<Category, Services.Models.Category>(category);
                _service.Create(model);
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

            var model = _map.Map<Services.Models.Category, Category>(_service.Get((int)id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
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
            var model = _map.Map<Category, Services.Models.Category>(category);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(model);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(model.Id))
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

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _map.Map<Services.Models.Category, Category>(_service.GetAll().FirstOrDefault(m => m.Id == id));
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var model = _map.Map<Services.Models.Category, Category>(_service.Get(id));
            _service.Delete(model.Id);
            return (RedirectToAction(nameof(Index)));
        }

        private bool CategoryExists(int id)
        {
            var model = _map.Map<IEnumerable<Services.Models.Category>, IEnumerable<Category>>(_service.GetAll());
            return model.Any(x => x.Id == id);
        }
    }
}
