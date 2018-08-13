using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Google_Keep_ToDo.Models
{
    public class Google_Keep_Context : DbContext
    {
        public Google_Keep_Context (DbContextOptions<Google_Keep_Context> options)
            : base(options)
        {
        }

        public DbSet<Google_Keep_ToDo.Models.Note> Note { get; set; }
    }
}
