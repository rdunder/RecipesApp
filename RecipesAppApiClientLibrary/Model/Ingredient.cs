using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesAppApiClientLibrary.Model
{
    public class Ingredient
    {
        public int id { get; set; } = 0;
        public string name { get; set; } = string.Empty;
        public int amount { get; set; }
        public string measure { get; set; } = string.Empty;
        public int recipeId { get; set; } = 0;
    }
}
