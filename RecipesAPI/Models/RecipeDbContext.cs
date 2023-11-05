using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace RecipesAPI.Models
{   

    public class RecipeDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; } = default;
        public DbSet<Ingredient> Ingredients { get; set; } = default;

        private readonly IConfiguration _config;
        public string DbConnectionString { get; }

        public RecipeDbContext(IConfiguration config)
        {
            _config = config;
            DbConnectionString = 
                _config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connectionstring not found");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(DbConnectionString);
        }
    }
}
