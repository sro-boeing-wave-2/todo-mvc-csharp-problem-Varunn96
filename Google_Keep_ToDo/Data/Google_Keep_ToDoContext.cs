using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Google_Keep_ToDo.Models
{
    public class Google_Keep_ToDoContext : DbContext
    {
        public Google_Keep_ToDoContext (DbContextOptions<Google_Keep_ToDoContext> options)
            : base(options)
        {
        }

        public DbSet<Google_Keep_ToDo.Models.MyNote> MyNote { get; set; }
        public DbSet<Google_Keep_ToDo.Models.CheckList> CheckList { get; set; }
        public DbSet<Google_Keep_ToDo.Models.Label> Label { get; set; }
    }
}
