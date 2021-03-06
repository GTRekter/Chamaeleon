using Lab_1.Enums;
using Lab_1.Interfaces;
using System;
using Utilities;

namespace Lab_1.Models
{
    class Book : IMedia
    {
        #region Fields

        private int id;

        private string title;

        private string publisher;

        private string creator;

        private int pages;

        private string illustrator;

        private DateTime publishDate;
        
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

        public string Illustrator
        {
            get
            {
                return illustrator;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Illustrator cannot be blank");
                }
                illustrator = value;
            }
        }

        public int Pages
        {
            get
            {
                return pages;
            }
            set
            {
                if (value <= 0)
                {
                    throw new IndexOutOfRangeException("Pages must be highter than zero");
                }
                pages = value;
            }
        }
        
        #endregion

        #region Public Methods

        public Book()
        {
        }

        public Book(int id, string title, string publisher, string creator, DateTime publishDate, string illustrator, int pages)
        {
            ID = id;
            Title = title;
            Publisher = publisher;
            Creator = creator;
            PublishDate = publishDate;
            Illustrator = illustrator;
            Pages = pages;
        }

        public int GetAge()
        {
            return DateTime.Now.Year - PublishDate.Year;
        }

        public void Print()
        {
            Console.WriteLine("{0,10}:{1,11}{2,11}{3,11}{4,11}{5,11}{6,11}{7,11}",
                "Book",
                ID,
                TextHelpers.SafeTrim(Title, 10),
                TextHelpers.SafeTrim(Publisher, 10),
                TextHelpers.SafeTrim(Creator, 10),
                PublishDate.ToString("yyyy-MM-dd"),
                TextHelpers.SafeTrim(Illustrator,10),
                Pages);
        }

        #endregion
    }
}
