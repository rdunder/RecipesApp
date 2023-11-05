namespace RecipesAPI.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TimeNeeded { get; set; } = string.Empty;
        public List<Ingredient>? Ingredients { get; set; }
        public string Procedure { get; set; } = string.Empty;
    }
}
