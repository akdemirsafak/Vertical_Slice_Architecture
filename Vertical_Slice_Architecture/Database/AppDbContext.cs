using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Entities;

namespace Vertical_Slice_Architecture.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Music> Musics { get; set; }



    }
}
