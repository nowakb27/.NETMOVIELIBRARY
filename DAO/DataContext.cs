using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationTemplate.Models;

namespace ApplicationTemplate.DAO
{
    public class DataContext
    {
        public List<Movie> MovieList { get; set; }
        public List<Show> ShowList { get; set; }
        public List<Video> VideoList { get; set; }

        public void ReadMovieData()
        {
            string filePath = $"{AppContext.BaseDirectory}/Data/movies.csv";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("NO EXISTING FILE: {File}", filePath);
                return;
            }

            try
            {
                MovieList = new List<Movie>();
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    var movie = new Movie();
                    
                    int index = line.IndexOf('"');
                    if (index == -1)
                    {
                        string[] movieDetails = line.Split(',');

                        
                        movie.Id = Convert.ToInt32((movieDetails[0]));

                        
                        movie.Title = movieDetails[1];

                       
                        movie.Genres = movieDetails[2].Replace("|", ", ");
                    }
                    else
                    {
                        
                        
                        movie.Id = Convert.ToInt32(line.Substring(0, index - 1));
                        
                        line = line.Substring(index + 1);
                        
                        index = line.IndexOf('"');
                        
                        movie.Title = line.Substring(0, index);
                        
                        line = line.Substring(index + 2);

                        
                        movie.Genres = line.Replace("|", ", ");

                    }

                    MovieList.Add(movie);

                }
                
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MovieList = null; 
            }

        }

        public void ReadShowData()
        {
            string filePath = $"{AppContext.BaseDirectory}/Data/shows.csv";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("NO EXISTING FILE: {File}", filePath);
                return;
            }

            try
            {
                ShowList = new List<Show>();
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    var show = new Show();

                    string[] showDetails = line.Split(',');

                    show.Id = Convert.ToInt32((showDetails[0]));
                    show.Title = showDetails[1];
                    show.Season = Convert.ToInt32((showDetails[2]));
                    show.Episode = Convert.ToInt32((showDetails[3]));
                    string[] writer = showDetails[4].Split("|");
                    show.Writers = writer;

                    ShowList.Add(show);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ReadVideoData()
        {
            string filePath = $"{AppContext.BaseDirectory}/Data/videos.csv";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("NO EXISTING FILE: {File}", filePath);
                return;
            }

            try
            {
                VideoList = new List<Video>();
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] videoDetails = line.Split(",");

                    var video = new Video();
                    video.Id = Int32.Parse(videoDetails[0]);
                    video.Title = videoDetails[1];
                    video.Format = videoDetails[2];
                    video.Length = Int32.Parse(videoDetails[3]);
                    var regionDetails = videoDetails[4].Split("|");
                    int[] region = Array.ConvertAll(regionDetails, s => int.Parse(s));
                    video.Regions = region;

                    VideoList.Add(video);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public DataContext()
        {
            ReadMovieData();
            ReadShowData();
            ReadVideoData();
        }
    }
}
