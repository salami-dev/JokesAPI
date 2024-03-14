using JokesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace JokesAPI.Data
{
    public class JokeContext: DbContext
    {
        public JokeContext(DbContextOptions<JokeContext> options) : base(options) { }

        public DbSet<JokeCategory> JokeCategories { get; set; } = null!;
        public DbSet<Joke> Jokes { get; set; } = null!;

        private void SetTimestamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseModel && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var now = DateTime.UtcNow;
                var entity = (BaseModel)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }

                entity.UpdatedAt = now;
            }
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
