using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1.Interfaces
{
    interface IMedia
    {
        /// <summary>
        /// Identification value for a media object 
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Title for the media 
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Name of the media’s publisher 
        /// </summary>
        string Publisher { get; set; }

        /// <summary>
        /// Name of the media’s creator 
        /// </summary>
        string Creator { get; set; }

        /// <summary>
        /// Date the media was published 
        /// </summary>
        DateTime PublishDate { get; set; }

        /// <summary>
        /// Method that returns the age of the media in years based on the difference between the current date and PublishDate
        /// </summary>
        /// <returns>Age of the media in years</returns>
        int GetAge();

        /// <summary>
        /// Method that outputs a string representation of the media object to the console window 
        /// </summary>
        void Print();
    }
}
