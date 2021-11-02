using Lab_1.Enums;
using Lab_1.Interfaces;
using Lab_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            List<IMedia> medias = new List<IMedia>();
           
            do
            {
                DisplayMenu();
                choice = ConsoleHelpers.ReadInt("Value: ", 0, 3);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        medias.Add(AddNewMovie());
                        break;
                    case 2:
                        medias.Add(AddNewBook());
                        break;
                    case 3:
                        medias.Add(AddNewVideoGame());
                        break;
                }

                Console.WriteLine();
                PrintSummary(medias);
                Console.WriteLine();
                Console.WriteLine("Press <ENTER> to continue... ");
                Console.ReadLine();

            } while (0 != choice);

            Console.Write("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Display the menu
        /// </summary>
        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select an action: ");
            Console.WriteLine("[0] - Quit");
            Console.WriteLine("[1] - Add Movie");
            Console.WriteLine("[2] - Add Book");
            Console.WriteLine("[3] - Add VideoGame");
        }

        public static void PrintSummary(List<IMedia> medias)
        {
            foreach (var media in medias)
            {
                var type = media.GetType();

                Console.WriteLine(new string('=', 100));
                switch(type.Name)
                {
                    case "Book":
                        ((Book)media).Print();
                        break;
                    case "Movie":
                        ((Movie)media).Print();
                        break;
                    case "Soundtrack":
                        ((Soundtrack)media).Print();
                        break;
                    case "VideoGame":
                        ((VideoGame)media).Print();
                        break;
                }
            }
        }

        public static Movie AddNewMovie()
        {
            Movie movie = new Movie();

            Console.WriteLine("<< New Movie >> ");
            movie.ID = ConsoleHelpers.ReadInt("Enter movie ID:  ", 1);
            movie.Title = ConsoleHelpers.ReadString("Enter movie title:  ");
            movie.Publisher = ConsoleHelpers.ReadString("Enter movie producer/publisher: ");
            movie.Creator = ConsoleHelpers.ReadString("Enter movie creator: ");
            movie.PublishDate = ConsoleHelpers.ReadDate("Enter publish date: ", new DateTime(1878, 01, 01));
            movie.RunLength = ConsoleHelpers.ReadInt("Enter run length: ", 1, 999);
            movie.Rating = ConsoleHelpers.ReadEnum<Ratings>("Movie rating: ");

            return movie;
        }

        public static Book AddNewBook()
        {
            Book book = new Book();

            Console.WriteLine("<< New Book >> ");
            book.ID = ConsoleHelpers.ReadInt("Enter book ID: ", 1);
            book.Title = ConsoleHelpers.ReadString("Enter book title:  ");
            book.Publisher = ConsoleHelpers.ReadString("Enter book producer/publisher: ");
            book.Creator = ConsoleHelpers.ReadString("Enter book creator: ");
            book.PublishDate = ConsoleHelpers.ReadDate("Enter publish date: ", new DateTime(1878, 01, 01));
            book.Illustrator = ConsoleHelpers.ReadString("Enter illustrator: ");
            book.Pages = ConsoleHelpers.ReadInt("Enter pages: ");

            return book;
        }

        public static VideoGame AddNewVideoGame()
        {
            VideoGame videoGame = new VideoGame();

            Console.WriteLine("<< New VideoGame >> ");
            videoGame.ID = ConsoleHelpers.ReadInt("Enter videogame ID: ", 1);
            videoGame.Title = ConsoleHelpers.ReadString("Enter videogame title:  ");
            videoGame.Publisher = ConsoleHelpers.ReadString("Enter videogame producer/publisher: ");
            videoGame.Creator = ConsoleHelpers.ReadString("Enter videogame creator: ");
            videoGame.PublishDate = ConsoleHelpers.ReadDate("Enter publish date: ", new DateTime(1878, 01, 01));
            videoGame.Multiplayer = ConsoleHelpers.ReadBool("Enter if videogame has multiplayer feature: ");
            videoGame.Soundtrack = AddNewSoundTrack();

            return videoGame;
        }

        public static Soundtrack AddNewSoundTrack()
        {
            Soundtrack soundtrack = new Soundtrack();

            Console.WriteLine("<< New Soundtrack >> ");
            soundtrack.ID = ConsoleHelpers.ReadInt("Enter soundtrack ID: ", 1);
            soundtrack.Title = ConsoleHelpers.ReadString("Enter soundtrack title:  ");
            soundtrack.Publisher = ConsoleHelpers.ReadString("Enter soundtrack producer/publisher: ");
            soundtrack.Creator = ConsoleHelpers.ReadString("Enter soundtrack creator: ");
            soundtrack.PublishDate = ConsoleHelpers.ReadDate("Enter publish date: ", new DateTime(1878, 01, 01));
            soundtrack.BeatsPerMinute = ConsoleHelpers.ReadInt("Enter beats per minute: ");

            return soundtrack;
        }
    }
}
