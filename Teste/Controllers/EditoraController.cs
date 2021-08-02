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
    public class EditoraController : Controller
    {

        private readonly Context db = new Context();


        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CNPJSortParm = sortOrder == "CNPJ" ? "CNPJ_desc" : "CNPJ";
            ViewBag.EnderecoSortParm = sortOrder == "Endereco" ? "Endereco_desc" : "Endereco";
            


            var editoras = from s in db.Editora
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                editoras = editoras.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    editoras = editoras.OrderByDescending(s => s.Nome);
                    break;
                case "CNPJ":
                    editoras = editoras.OrderBy(s => s.CNPJ);
                    break;
                case "CNPJ_desc":
                    editoras = editoras.OrderByDescending(s => s.CNPJ);
                    break;
                case "Endereco":
                    editoras = editoras.OrderBy(s => s.Endereco);
                    break;
                case "Endereco_desc":
                    editoras = editoras.OrderByDescending(s => s.Endereco);
                    break;
                default:
                    editoras = editoras.OrderBy(s => s.Nome);
                    break;
            }
            return View(editoras.ToList());
        }

        // GET: Editora


        // GET: Editora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoras = await db.Editora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editoras == null)
            {
                return NotFound();
            }

            return View(editoras);
        }

        // GET: Editora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CNPJ,Nome,Endereco")] Editoras editoras)
        {
            if (ModelState.IsValid)
            {
                db.Editora.Add(editoras);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editoras);
        }

        // GET: Editora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoras = await db.Editora.FindAsync(id);
            if (editoras == null)
            {
                return NotFound();
            }
            return View(editoras);
        }

        // POST: Editora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CNPJ,Nome,Endereco")] Editoras editoras)
        {
            if (id != editoras.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Editora.Update(editoras);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorasExists(editoras.Id))
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
            return View(editoras);
        }

        // GET: Editora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoras = await db.Editora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editoras == null)
            {
                return NotFound();
            }

            return View(editoras);
        }

        // POST: Editora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editoras = await db.Editora.FindAsync(id);
            db.Editora.Remove(editoras);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorasExists(int id)
        {
            return db.Editora.Any(e => e.Id == id);
        }
    }
}
