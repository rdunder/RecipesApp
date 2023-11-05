using Newtonsoft.Json;
using RecipesAppApiClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RecipesAppApiClientLibrary
{
    public class RecipeApiClient
    {
        static HttpClient _httpClient;

        string _connectionString;


        public RecipeApiClient(string connectionString) 
        {  
            _connectionString = connectionString;

            var _socketsHttpHandler = new SocketsHttpHandler()
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(20),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            _httpClient = new HttpClient(_socketsHttpHandler);
        }



        /// <summary>
        /// Gets the Json data from the recipe API and return it as a string
        /// </summary>
        /// <param name="_conString"></param>
        /// <returns></returns>
        private async Task<string> GetJsonDataAsync(string _conString)
        {
            HttpResponseMessage _respons = await _httpClient.GetAsync(_conString);
            string jsonString = await _respons.Content.ReadAsStringAsync();

            return jsonString;
        }

        /// <summary>
        /// Gets a list of all the recipes currently available in the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public List<Recipe> GetRecipes()
        {
            Task<string> jsonString = GetJsonDataAsync(_connectionString);
            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonString.Result);

            return recipes ?? throw new InvalidOperationException("Connection Error");
        }

        /// <summary>
        /// Gets the recipe with stated ID if it exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Recipe GetRecipeById(int id)
        {
            string _connStringWithId = _connectionString + $"/{id}";
            
            Task<string> jsonString = GetJsonDataAsync(_connStringWithId);
            Recipe recipe = JsonConvert.DeserializeObject<Recipe>(jsonString.Result);

            return recipe ?? throw new InvalidOperationException("Connection Error");
        }

        /// <summary>
        /// Adds a recipe to the database
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public async Task AddNewRecipeAsync(Recipe recipe)
        {

            string jsonString = JsonConvert.SerializeObject(recipe);

            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_connectionString, recipe);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Updates the inserted recipe if the ID exists
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            int recipeID;

            if (recipe.id != null)
            {
                recipeID = recipe.id;
            }
            else { return; }

            string _connStringWithId = _connectionString + $"/{recipeID}";

            string jsonString = JsonConvert.SerializeObject(recipe);
            using HttpResponseMessage respons = await _httpClient.PutAsJsonAsync(_connStringWithId, recipe);
            respons.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Deletes the recipe with stated ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRecipe(int id)
        {
            string _connStringWithId = _connectionString + $"/{id}";

            using HttpResponseMessage respons = await _httpClient.DeleteAsync(_connStringWithId);
            respons.EnsureSuccessStatusCode();
        }
    }
}
