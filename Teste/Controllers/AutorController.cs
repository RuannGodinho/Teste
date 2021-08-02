using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teste.Models;

namespace Teste.Controllers
{
    public class AutorController : Controller
    {
        private readonly Context db = new Context();


        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CPFSortParm = sortOrder == "CPF" ? "CPF_desc" : "CPF";
            ViewBag.CelularSortParm = sortOrder == "Celular" ? "Celular_desc" : "Celular";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "";


            var autores = from s in db.Autor
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                autores = autores.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    autores = autores.OrderByDescending(s => s.Nome);
                    break;
                case "CPF":
                    autores = autores.OrderBy(s => s.CPF);
                    break;
                case "CPF_desc":
                    autores = autores.OrderByDescending(s => s.CPF);
                    break;
                case "Celular":
                    autores = autores.OrderBy(s => s.Celular);
                    break;
                case "Celular_desc":
                    autores = autores.OrderByDescending(s => s.Celular);
                    break;
                case "Email":
                    autores = autores.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    autores = autores.OrderByDescending(s => s.Email);
                    break;
                default:
                    autores = autores.OrderBy(s => s.Nome);
                    break;
            }
            return View(autores.ToList());
        }


        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await db.Autor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CPF,Celular,Email")] Autores autores)
        {
            if (ModelState.IsValid)
            {
                db.Autor.Add(autores);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autores);
        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await db.Autor.FindAsync(id);
            if (autores == null)
            {
                return NotFound();
            }
            return View(autores);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,Celular,Email")] Autores autores)
        {
            if (id != autores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Autor.Update(autores);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoresExists(autores.Id))
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
            return View(autores);
        }

        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await db.Autor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autores = await db.Autor.FindAsync(id);
            db.Autor.Remove(autores);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoresExists(int id)
        {
            return db.Autor.Any(e => e.Id == id);
        }
    }
}
