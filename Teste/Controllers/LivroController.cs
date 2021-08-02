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
    public class LivroController : Controller
    {
        private readonly Context db = new Context();


        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EdiçãoSortParm = sortOrder == "Edição" ? "Edição_desc" : "Edição";
            ViewBag.Data_de_LancamentoSortParm = sortOrder == "Data_de_Lancamento" ? "Data_de_Lancamento_desc" : "Data_de_Lancamento";
            ViewBag.EditoraSortParm = sortOrder == "Editora" ? "Editora_desc" : "Editora";
            ViewBag.AutorSortParm = sortOrder == "Autor" ? "Autor_desc" : "Autor";


            var livros = from s in db.Livro
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                livros = livros.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    livros = livros.OrderByDescending(s => s.Nome);
                    break;
                case "Edição":
                    livros = livros.OrderBy(s => s.Edição);
                    break;
                case "Edição_desc":
                    livros = livros.OrderByDescending(s => s.Edição);
                    break;
                case "Data_de_Lancamento":
                    livros = livros.OrderBy(s => s.Data_de_Lancamento);
                    break;
                case "Data_de_Lancamento_desc":
                    livros = livros.OrderByDescending(s => s.Data_de_Lancamento);
                    break;
                case "Editora":
                    livros = livros.OrderBy(s => s.Editora);
                    break;
                case "Editora_desc":
                    livros = livros.OrderByDescending(s => s.Editora);
                    break;
                case "Autor":
                    livros = livros.OrderBy(s => s.Autor);
                    break;
                case "Autor_desc":
                    livros = livros.OrderByDescending(s => s.Autor);
                    break;
                default:
                    livros = livros.OrderBy(s => s.Nome);
                    break;
            }
            return View(livros.ToList());
        }

            // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livros = await db.Livro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livros == null)
            {
                return NotFound();
            }

            return View(livros);
        }

        // GET: Livro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Edição,Data_de_Lancamento,Editora,Autor")] Livros livros)
        {
            if (ModelState.IsValid)
            {
                db.Livro.Add(livros);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livros);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livros = await db.Livro.FindAsync(id);
            if (livros == null)
            {
                return NotFound();
            }
            return View(livros);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Edição,Data_de_Lancamento,Editora,Autor")] Livros livros)
        {
            if (id != livros.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Livro.Update(livros);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivrosExists(livros.Id))
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
            return View(livros);
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livros = await db.Livro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livros == null)
            {
                return NotFound();
            }

            return View(livros);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livros = await db.Livro.FindAsync(id);
            db.Livro.Remove(livros);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivrosExists(int id)
        {
            return db.Livro.Any(e => e.Id == id);
        }
    }
}
