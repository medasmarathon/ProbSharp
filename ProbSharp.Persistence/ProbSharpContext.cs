using Microsoft.EntityFrameworkCore;

namespace ProbSharp.Persistence
{
    public class ProbSharpContext(DbContextOptions<ProbSharpContext> options) : DbContext(options)
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Node>()
                .Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Node>()
                .Property(e => e.Attributes).HasColumnType("jsonb");
            modelBuilder.Entity<Node>()
                .HasMany(e => e.Has).WithOne(r => r.Owner);
            modelBuilder.Entity<Node>()
                .HasMany(e => e.BelongsTo).WithOne(r => r.Related);


            modelBuilder.Entity<Relationship>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Relationship>()
                .Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Relationship>()
                .Property(e => e.Attributes).HasColumnType("jsonb");
        }
    }
}
