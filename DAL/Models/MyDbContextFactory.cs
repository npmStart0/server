using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

            // Load the connection string from the environment variable
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

            // Configure the options
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

            // Return a DbContext with the configured options
            return new MyDbContext(optionsBuilder.Options);
        }
    }
}
