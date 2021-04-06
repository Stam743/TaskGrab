using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskGrab.Data
{
    public class TaskGrabContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<CommunityLocation> CommunityLocations { get; set; }
        public DbSet<UserInfo> UserInformation { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=tasksGrab.db");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
