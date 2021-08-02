using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Models
{
    public class Context : DbContext
    {

        public Context()
            : base()
        {
        }

        public DbSet<Autores> Autor { get; set; }
        public DbSet<Editoras> Editora { get; set; }
        public DbSet<Livros> Livro { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=(localdb)\mssqllocaldb;Database=Projeto;Integrated Security=True");
        }
    }
}
