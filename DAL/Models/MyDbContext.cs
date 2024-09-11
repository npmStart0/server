using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using dotenv.net;
using DotNetEnv;
using System.Diagnostics;

namespace DAL.Models
{
    public class MyDbContext : DbContext, IContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Check the environment
            #if DEBUG
                Env.Load("../.env.local");
            #else
                Env.Load("../.env.remote");
            #endif
            
            // Get the connection string from the file
            string connectionString = Env.GetString("DB_CONNECTION");

            // Configure MySQL with the connection string
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
