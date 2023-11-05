using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecipesAppApiClientLibrary;
using RecipesAppApiClientLibrary.Model;


RecipeApiClient recipeApiClient = new RecipeApiClient(@"https://localhost:7025/api/recipes");
var listOfRecipes = recipeApiClient.GetRecipes();


bool isRunning = true;
while (isRunning)
{
    Console.Clear();
    Console.WriteLine($@"
Welcome to Recipe CLI

S. Show recipes
N. Add new recipe
C. Change / Update a recipe
D. Delete a recipe

Q. Quit / Exit the program
");

    var menuChoice = Console.ReadKey();

    if (menuChoice.Key == ConsoleKey.S) 
    {
        bool isShowing = true;

        while (isShowing)
        {
            Console.Clear();
            PrintRecipesIdAndTitle(listOfRecipes);

            Console.WriteLine("\nType an ID to show recipe or M to go back to menu");
            var recipeChoice = Console.ReadLine().ToLower();

            if (recipeChoice == "m") { isShowing = false; break; }

            if ( int.TryParse(recipeChoice, out int recipeID))
            {
                foreach (Recipe recipe in listOfRecipes)
                {
                    if (recipeID == recipe.id)
                    {
                        PrintRecipe(recipe);
                    }
                }
            }
            else
            {
                Console.WriteLine("You have to enter a valid ID or M");
            }
            Console.ReadKey();
        }
        
    }

    else if (menuChoice.Key == ConsoleKey.N)
    {

    }
    else if (menuChoice.Key == ConsoleKey.C)
    {

    }
    else if (menuChoice.Key == ConsoleKey.D)
    {

    }
    else if (menuChoice.Key == ConsoleKey.Q)
    {
        isRunning = false;
    }
}





Recipe recipeToAdd = new()
{
    title = "Fisksoppa",
    description = "Soppa med fisk i",
    timeNeeded = "60",
    procedure = "Koka fisken mjuk, servera med något gött",

    ingredients = new List<Ingredient>()
    {
        new Ingredient()
        {
            name = "Fisk",
            amount = 500,
            measure = "gram"
        },
        new Ingredient()
        {
            name = "Buljong",
            amount = 1,
            measure = "st"
        }
    },
};
//await recipeApiClient.AddNewRecipeAsync(recipeToAdd);


//await recipeApiClient.DeleteRecipe(7);





static async Task UpdateRecipe(RecipeApiClient recipeApiClient, int id)
{
    var recipeToUpdate = recipeApiClient.GetRecipeById(id);
    string newTitle = recipeToUpdate.title + " CHANGE nr1";

    recipeToUpdate.title = newTitle;

    await recipeApiClient.UpdateRecipeAsync(recipeToUpdate);
}

static async Task AddNewRecipeAsync(RecipeApiClient recipeApiClient, Recipe recipeToAdd)
{
    await recipeApiClient.AddNewRecipeAsync(recipeToAdd);
}


void PrintRecipe(Recipe recipe)
{
    Console.WriteLine($@"
{recipe.title}
{recipe.timeNeeded}
{recipe.description}
");
    foreach (Ingredient i in recipe.ingredients)
    {
        Console.WriteLine($"{i.amount} {i.measure} {i.name}");
    }
    Console.WriteLine(recipe.procedure);
}
void PrintRecipesIdAndTitle(List<Recipe> listOfRecipes)
{
    foreach (Recipe recipe in listOfRecipes)
    {
        Console.WriteLine($"#ID - {recipe.id}.  {recipe.title}");
    }
}
void PrintRecipes(List<Recipe> listOfRecipes)
{
    foreach (Recipe recipe in listOfRecipes)
    {
        Console.WriteLine(recipe.title);
        Console.WriteLine(recipe.timeNeeded + " minuter");
        Console.WriteLine(recipe.description + "\n");

        foreach (Ingredient i in recipe.ingredients)
        {
            Console.WriteLine($"{i.amount} {i.measure} {i.name}");
        }

        Console.WriteLine("\n" + recipe.procedure);

        for (int i = 0; i < recipe.procedure.Count<char>(); i++)
        {
            Console.Write("_");
        }
        Console.WriteLine("\n\n");
    }
}