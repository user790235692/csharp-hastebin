using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csharp_hastebin
{
    public class Program
    {
        public static HttpClient client = new HttpClient();

        
        public static async Task Main(string[] args)
        {
            try {
                string path = args[0];

                string readFiles = await File.ReadAllTextAsync(path);
                var response = await sendSourceCodeToHasteBin(readFiles);

                dynamic responseToJson = JObject.Parse(response);

                Console.WriteLine($"Votre code source se trouve à l'adresse : https://hastebin.com/{responseToJson.key}");
            }

            catch (IndexOutOfRangeException) {
                Console.WriteLine("Veuillez insérer un répertoire !");
            }

            catch (FileNotFoundException) {
                Console.WriteLine("Chemin vers le fichier inconnu. Veuillez vérfier le chemin que vous avez indiqué !");
            }   

            
            catch (JsonReaderException) {
                Console.WriteLine("Hastebin a rencontré un problème. Veuillez réessayer.");
            }            
        }

        /// <summary>
        /// Send a POST request to https://hastebin.com/documents to get the location of a source code.
        /// </summary>
        /// <param name="sourceCode">The source code extracted on a file as string</param>
        /// <returns>
        /// The return of the request to Hastebin.
        /// It return JSON like this : {"key": "kjdisdhdsi"}
        /// </returns>
            public static async Task<string> sendSourceCodeToHasteBin(string sourceCode)
            {
            var content = new FormUrlEncodedContent(new [] {new KeyValuePair<string, string> ("", sourceCode)});
            var response = await client.PostAsync("https://hastebin.com/documents", content);
            return await response.Content.ReadAsStringAsync(); // {'key': "ajdjdjdddj"}
            
            }

}

}

