using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    public class UsersDbContext:DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {

        }

        public DbSet<UserDetailsModel> UserDetails { get; set; }
        public DbSet<QuestionsModel> QuestionsDetails { get; set; }
        //public DbSet<Role> userRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mapping to Db
            modelBuilder.Entity<UserDetailsModel>().ToTable("Users");
            modelBuilder.Entity<QuestionsModel>().ToTable("Questions");
        }
    }
}
