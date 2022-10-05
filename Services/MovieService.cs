using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApplicationTemplate;
using Microsoft.Extensions.Logging;

namespace ApplicationTemplate.Services;

public class MovieService : IMovieService
{
    private readonly ILogger<IMovieService> LOG;

    public MovieService(ILogger<IMovieService> logger)
    {
        LOG = logger;
    }

    public void Read()
    {
        string FILE = "Files/movies.csv";
        if (File.Exists(FILE))
        {
            StreamReader sr = new StreamReader(FILE);

            Console.WriteLine("HOW MANY RECORDS WOULD YOU LIKE TO VIEW? SELECT 'A' TO VIEW ALL RECORDS");
            var PICK = Console.ReadLine().ToUpper();
            sr.ReadLine();
            if (PICK != "A")
            {
                int NUMBER = 0;
                while (NUMBER < int.Parse(PICK))
                {
                    string LINE1 = sr.ReadLine();
                    Console.WriteLine(LINE1);
                    NUMBER++;
                }
            }
            else
            {
                while (!sr.EndOfStream)
                {
                    string LINE2 = sr.ReadLine();
                    Console.WriteLine(LINE2);
                }
            }


            sr.Close();
        }
        else
        {
            Console.WriteLine("NO FILE FOUND");
        }
    }


    public void Write()
    {
        try
        {
            List<Movie> movies = new List<Movie>();
            string file = "Files/movies.csv";
            using (var READ = new StreamReader(file))
            {
                Movie MOVIE = new Movie();

                while (!READ.EndOfStream)
                {
                    var line = READ.ReadLine();

                    string[] MOVIEINFO = line.Split(',');
                    MOVIE.ID = int.Parse(MOVIEINFO[0]);
                    MOVIE.TITLE = MOVIEINFO[1];
                    MOVIE.GENRE = MOVIEINFO[2].Split('|').ToList();
                    movies.Add(MOVIE);
                }

                READ.Close();


                StreamWriter STREAMWRITER = new StreamWriter(file, true);
                string RESPONSE = "";
                do
                {
                    MOVIE.ID = movies.Max(m => m.ID) + 1;

                    Console.WriteLine("ENTER TITLE OF MOVIE");
                    string MOVIETITLE = Console.ReadLine();
                    if (MOVIE.TITLE.Contains(MOVIETITLE))
                    {
                        Console.WriteLine("THIS MOVIE ALREADY EXISTS");
                        Console.WriteLine("ENTER ANOTHER MOVIE TITLE");
                        MOVIETITLE = Console.ReadLine();
                        if (MOVIETITLE.Contains(','))
                        {
                            MOVIETITLE = "\"title\"";
                        }
                    }


                    var choice = "";
                    var MOVIEGENRE = new List<string>();
                    do
                    {
                        Console.WriteLine("ENTER GENRE OF MOVIE");
                        MOVIEGENRE.Add(Console.ReadLine());
                        Console.WriteLine("WOULD YOU LIKE TO ADD ANOTHER GENRE? Y/N ");
                        choice = Console.ReadLine().ToUpper();
                    } while (choice != "N");

                    string genres = string.Join("|", MOVIEGENRE);

                    STREAMWRITER.WriteLine("{0},{1},{2}", MOVIE.ID, MOVIETITLE, genres);
                    Console.WriteLine("WOULD YOU LIKE TO ADD ANOTHER MOVIE (Y/N)?");
                    RESPONSE = Console.ReadLine().ToUpper();
                } while (RESPONSE != "N");

                STREAMWRITER.Close();

            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("NO FILE FOUND");
        }

    }
}
