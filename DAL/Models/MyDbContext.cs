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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            // בדיקת הסביבה
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            // טעינת הקובץ הנכון לפי הסביבה
            if (environment == "Development")
            {
                Env.Load("../.env.local");
            }
            else
            {
                Env.Load("../.env.remote");
            }

            // קבלת מחרוזת החיבור מהקובץ
            string connectionString = Env.GetString("DB_CONNECTION");

            // הגדרת MySQL עם מחרוזת החיבור
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
