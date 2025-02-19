using iBethlem.Core.Data;
using iBethlem.Core.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Tests.Data;

public class MockContext(DbContextOptions<DataContext> options) : DataContext(options)
{
    public DbSet<MockEntity> MockEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MockEntity>().HasData(new MockEntity { Id = 1, Name = "MockEntity1", MockProperty = "MockProperty1" });
        base.OnModelCreating(modelBuilder);
    }
}
