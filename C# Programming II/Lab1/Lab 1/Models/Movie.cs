using Lab_1.Enums;
using Lab_1.Interfaces;
using System;
using Utilities;

namespace Lab_1.Models
{
    class Movie : IMedia
    {
        #region Fields

        private int id;

        private string title;

        private string publisher;

        private string creator;

        private int runLength;

        private DateTime publishDate;

        private Ratings rating;

        #endregion

        #region Properties

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if (value <= 0)
                {
                    throw new IndexOutOfRangeException("ID must be highter than zero");
                }
                id = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title cannot be blank");
                }
                title = value;
            }
        }

        public string Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Publisher cannot be blank");
                }
                publisher = value;
            }
        }

        public string Creator
        {
            get
            {
                return creator;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Creator cannot be blank");
                }
                creator = value;
            }
        }

        public DateTime PublishDate
        {
            get
            {
                return publishDate;
            }
            set
            {
                if (value < new DateTime(1878, 01, 01))
                {
                    throw new ArgumentOutOfRangeException("Must be greater than January 1, 1878");
                }
                publishDate = value;
            }
        }

        public int RunLength
        {
            get
            {
                return runLength;
            }
            set
            {
                if (value <= 0)
                {
                    throw new IndexOutOfRangeException("ID must be highter than zero");
                }
                runLength = value;
            }
        }

        public Ratings Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
            }
        }

        #endregion

        #region Public Methods

        public Movie()
        {
        }

        public Movie(int id, string title, string publisher, string creator, DateTime publishDate, int runLenght, Ratings rating)
        {
            ID = id;
            Title = title;
            Publisher = publisher;
            Creator = creator;
            PublishDate = publishDate;
            RunLength = runLength;
            Rating = rating;
        }

        public int GetAge()
        {
            return DateTime.Now.Year - PublishDate.Year;
        }

        public void Print()
        {
            Console.WriteLine("{0,10}:{1,11}{2,11}{3,11}{4,11}{5,11}{6,11}{7,11}",
                "Movie",
                ID,
                TextHelpers.SafeTrim(Title, 10),
                TextHelpers.SafeTrim(Publisher, 10),
                TextHelpers.SafeTrim(Creator, 10),
                PublishDate.ToString("yyyy-MM-dd"),
                RunLength,
                TextHelpers.WordBreakMixedCase(Rating));
        }

        #endregion
    }
}
