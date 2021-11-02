using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lab2Service.Models
{
    [DataContract]
    public class Teacher : Person
    {
        /// <summary>
        /// Teacher’s numeric ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// Date teacher was hired
        /// </summary>
        [DataMember]
        public DateTime DateOfHire { get; set; }
        /// <summary>
        /// Teacher’s salary rounded to nearest dollar
        /// </summary>
        [DataMember]
        public int Salary { get; set; }
    }
}