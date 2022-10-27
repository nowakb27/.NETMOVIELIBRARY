using System;
using ApplicationTemplate.Services;

namespace ApplicationTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var SEARCH = new MediaOrchestrator();

            Console.Write("SEARCH (TYPE 'THE' TO TEST): ");
            var searchString = Console.ReadLine().ToLower();
            var results = SEARCH.SearchAllMedia(searchString);

            Console.WriteLine("SEARCH RESULTS:");
            results.ForEach(Console.WriteLine);

        }
    }
}
