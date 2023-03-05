using System;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class DatabaseContext: DbContext
    {
		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

        public DbSet<Employee> Employee { get; set; }
    }
}

