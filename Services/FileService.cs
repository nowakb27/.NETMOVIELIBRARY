using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApplicationTemplate.Services;

/// <summary>
///     This concrete service and method only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public class FileService : IFileService
{
    private readonly ILogger<IFileService> _logger;

    public FileService(ILogger<IFileService> logger)
    {
        _logger = logger;
    }
    public void Read()
    {
        try
        {
            List<Movie> movies = new List<Movie>();
            string fileName = "Files/movies.json";
            StreamReader STREAMREADER = new StreamReader(fileName);
            while (!STREAMREADER.EndOfStream)
            {
                string json = STREAMREADER.ReadLine();
                var m = JsonConvert.DeserializeObject<Movie>(json);
                movies.Add(m);
            }

            foreach (Movie movie in movies)
            {
                Console.Write("Id:{0} Title:{1} Genres: ", movie.ID, movie.TITLE);
                movie.GENRE.ForEach(g => Console.Write(g + ", "));
                Console.WriteLine();
            }

            STREAMREADER.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("NO MOVIES TO SHOW");
        }

    }

    public void Write()
    {
        List<Movie> movies = new List<Movie>();
        string fileName = "Files/movies.json";
        StreamReader r = new StreamReader(fileName);
        while (!r.EndOfStream)
        {
            string jsonString = r.ReadLine();
            var m = JsonConvert.DeserializeObject<Movie>(jsonString);
            movies.Add(m);
        }

        r.Close();

        Movie movie = new Movie();
        movie.ID = movies.Count > 0 ? movies.Max(m => m.ID + 1) : 1;
        Console.WriteLine("ENTER TITLE OF MOVIE");
        movie.TITLE = Console.ReadLine();
        var choice = "";
        movie.GENRE = new List<string>();
        do
        {
            Console.WriteLine("ENTER GENRE OF MOVIE");
            movie.GENRE.Add(Console.ReadLine());
            Console.WriteLine("WOULD YOU LIKE TO ADD ANOTHER GENRE? Y/N ");
            choice = Console.ReadLine().ToUpper();
        } while (choice != "N");

        string JSON = JsonConvert.SerializeObject(movie);

        File.WriteAllText(fileName, JSON);
    }


}
