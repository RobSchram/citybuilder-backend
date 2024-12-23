﻿using Microsoft.EntityFrameworkCore;
using Logic.model;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<GameField> GameFields { get; set; }
        public DbSet<GameFieldCell> GameFieldCells { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameField>()
                .HasMany(g => g.Cells)
                .WithOne()
                .HasForeignKey(c => c.GameFieldId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
