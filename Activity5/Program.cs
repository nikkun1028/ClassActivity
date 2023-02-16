using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;



namespace GenderAPIClient
{

    class Person
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("probability")]
        public double Prob { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }





    class Program
    {
        private static readonly HttpClient client = new HttpClient();



        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }


        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a name. Press Enter without writing a name to Exit the Program.");
                    var personName = Console.ReadLine(); // read user-input.

                    if (string.IsNullOrEmpty(personName))
                    {
                        Console.WriteLine("\nGoodbye!");
                        break;
                    }

                    // send GET request
                    var result = await client.GetAsync("https://api.genderize.io/?name=" + personName.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();


                    var person = JsonConvert.DeserializeObject<Person>(resultRead);
                    int prob = Convert.ToInt32(person.Prob * 100);
                    Console.WriteLine("____________");
                    Console.WriteLine("Name: " + person.Name);
                    Console.WriteLine("Gender (estimated): " + person.Gender);
                    Console.WriteLine("Probability: " + prob + "%");
                    Console.WriteLine("Count: " + person.Count);
                    Console.WriteLine("\n____________");



                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR: unable to find the name, please try different name...");
                }
            } // end while-loop
        }



    } // end Program class


} // end namespace GenderAPIClient