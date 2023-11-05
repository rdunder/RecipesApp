using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesAppApiClientLibrary.Model
{
    public class Recipe
    {
        public int id { get; set; } = 0;
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string timeNeeded { get; set; } = string.Empty;
        public List<Ingredient>? ingredients { get; set; }
        public string procedure { get; set; } = string.Empty;
    }
}
