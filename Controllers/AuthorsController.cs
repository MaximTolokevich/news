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

        private readonly IService<Services.Models.Author> service;
        private readonly IMapper map;
        

        public AuthorsController(IService<Services.Models.Author> _service,IMapper mapper)
        {
            service = _service;
            map = mapper;
        }

        // GET: Authors
        public IActionResult Index()
        {
            var a = map.Map<IEnumerable<Services.Models.Author>, IEnumerable<Author>>(service.GetAll());
            return View(a);
        }

        // GET: Authors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var a = map.Map<Services.Models.Author, Author>(service.GetAll().FirstOrDefault(m => m.Id == id));
            
            if (a == null)
            {
                return NotFound();
            }

            return View(a);
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
                var a = map.Map<Author, Services.Models.Author>(author);
                service.Create(a);

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

            var a = map.Map<Services.Models.Author, Author>(service.Get((int)id));
            if (a == null)
            {
                return NotFound();
            }
            return View(a);
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
            var a = map.Map<Author, Services.Models.Author>(author);
            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(a);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(a.Id))
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

        // GET: Authors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var a = map.Map< Services.Models.Author,Author > (service.GetAll().FirstOrDefault(m => m.Id == id));
              
                
            if (a == null)
            {
                return NotFound();
            }

            return View(a);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var a = map.Map<Services.Models.Author, Author>(service.Get(id));
            service.Delete(a.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            var a = map.Map<IEnumerable<Services.Models.Author>, IEnumerable< Author>>(service.GetAll());
            return a.Any(e => e.Id == id);
        }
    }
}
