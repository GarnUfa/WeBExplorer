using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Models
{
    public class ExplorerContext : DbContext
    {
        public DbSet<FoldersModel>Folders { get; set; }
        public DbSet<FilesModel> Files { get; set; }
        public DbSet<FileExtensionsModel> FileExtensions { get; set; }
        public ExplorerContext(DbContextOptions<ExplorerContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoldersModel>().HasData(new FoldersModel());
            modelBuilder.Entity<FilesModel>().HasData(new FilesModel());
            modelBuilder.Entity<FileExtensionsModel>().HasData(new FileExtensionsModel());
        }

    }
}
