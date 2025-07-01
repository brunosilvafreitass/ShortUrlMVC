using Microsoft.EntityFrameworkCore;
using ShortUrlMvc.Models;

namespace ShortUrlMvc.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UrlModel> Urls { get; set; }
    }
}