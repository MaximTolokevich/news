using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using news.Controllers.Models;
using news.Services;
using System.Collections.Generic;
using System.Linq;

namespace news.Controllers
{
    public class AuthorsController : Controller
    {

        private readonly IService<Services.Models.Author> _service;
        private readonly IMapper _map;
        

        public AuthorsController(IService<Services.Models.Author> service,IMapper mapper)
        {
            _service = service;
            _map = mapper;
        }

        // GET: Authors
        public IActionResult Index()
        {
            var model = _map.Map<IEnumerable<Services.Models.Author>, IEnumerable<Author>>(_service.GetAll());
            return View(model);
        }

        // GET: Authors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _map.Map<Services.Models.Author, Author>(_service.GetAll().FirstOrDefault(m => m.Id == id));
            
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Password,Email")] Author author)
        {
            if (ModelState.IsValid)
            {
                var model = _map.Map<Author, Services.Models.Author>(author);
                _service.Create(model);

                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _map.Map<Services.Models.Author, Author>(_service.Get((int)id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,FullName,Password,Email")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }
            if (!AuthorExists(author.Id))
            {
                return NotFound();
            }
            var model = _map.Map<Author, Services.Models.Author>(author);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;              
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _map.Map< Services.Models.Author,Author > (_service.GetAll().FirstOrDefault(m => m.Id == id));
              
                
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var model = _map.Map<Services.Models.Author, Author>(_service.Get(id));
            _service.Delete(model.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            var model = _map.Map<IEnumerable<Services.Models.Author>, IEnumerable< Author>>(_service.GetAll());
            return model.Any(e => e.Id == id);
        }
    }
}
