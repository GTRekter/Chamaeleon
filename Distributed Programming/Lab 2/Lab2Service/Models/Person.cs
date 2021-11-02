using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lab2Service.Models
{
    [DataContract]
    [KnownType(typeof(Student))]
    [KnownType(typeof(Teacher))]
    public class Person
    {

        /// <summary>
        /// Person’s last name
        /// </summary>
        [DataMember]
        public string LastName { get; set; }
        /// <summary>
        /// Person’s first name
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }
        /// <summary>
        /// Date of birth
        /// </summary>
        [DataMember]
        public DateTime DOB { get; set; }
        /// <summary>
        /// Person’s gender
        /// </summary>
        [DataMember]
        public GenderEnum Gender { get; set; }
    }
}