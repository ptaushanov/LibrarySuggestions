using LibraryWPF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWPF.DAL
{
    public class LibraryContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public LibraryContext() : base(Properties.Settings.Default.DbConnect) { }
    }
}
