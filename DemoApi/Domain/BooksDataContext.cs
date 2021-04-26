using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Domain
{
    public class BooksDataContext :DbContext
    {

        public BooksDataContext(DbContextOptions<BooksDataContext> options): base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey("ISBN").IsClustered(true);
            modelBuilder.Entity<Author>().HasKey("Id").IsClustered(false);
            modelBuilder.Entity<Author>().HasIndex("LastName", "FirstName").IsUnique().IsClustered(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}
