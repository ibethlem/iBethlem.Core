using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Data;

public abstract class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
}
