using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lab2Service.Models
{
    [DataContract]
    public class Student : Person
    {
        /// <summary>
        /// Student’s alpha-numeric ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }
        /// <summary>
        /// Student’s major
        /// </summary>
        [DataMember]
        public string Major { get; set; }
        /// <summary>
        /// Number of units the student has completed
        /// </summary>
        [DataMember]
        public float Units { get; set; }
        /// <summary>
        /// Student’s GPA
        /// </summary>
        [DataMember]
        public float GPA { get; set; }
    }
}