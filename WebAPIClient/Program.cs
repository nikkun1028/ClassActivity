using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace WebAPIClient
{

    class Pokemon
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        public List<Types> Types { get; set; }

    }

    public class Type
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Types
    {
        [JsonProperty("type")]
        public Type Type;
    }



    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        // allows to send HTTP request, then receive HTTP response.
        



        // async stops until await Task is completed
        static async Task Main(string[] args)
        {
            await ProcessRepositories();

        }


        // returns a Task
        private static async Task ProcessRepositories()
        {
            while (true)
            {

                try
                {
                    Console.WriteLine("Enter Pokemon Name. Press Enter without writing a name to Exit the Program.");
                    var pokemonName = Console.ReadLine(); // read user-input.

                    if (string.IsNullOrEmpty(pokemonName))
                    {
                        Console.WriteLine("\nGoodBye!");
                        break;
                    }

                    // send GET request to given URI
                    var result = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/" + pokemonName.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();


                    // deserialize and list pokemon attributes from Pokemon class
                    var pokemon = JsonConvert.DeserializeObject<Pokemon>(resultRead);
                    Console.WriteLine("______________");
                    Console.WriteLine("Pokemon #" + pokemon.Id);
                    Console.WriteLine("Name: " + pokemon.Name);
                    Console.WriteLine("Weight: " + pokemon.Weight + "lb");
                    Console.WriteLine("Height: " + pokemon.Height + "ft");
                    Console.Write("Type(s):");
                    pokemon.Types.ForEach(t => Console.Write(" " + t.Type.Name));
                    Console.WriteLine("\n______________");

                }
                catch (Exception)
                { // this is when API couldn't find Pokemon and throws exception
                    Console.WriteLine("ERROR: invalid Pokemon name, please try again.");
                }

            } // end while-loop
        }
    } // end Program class

} // end namespace WebAPIClient