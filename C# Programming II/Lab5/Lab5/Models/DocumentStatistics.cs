using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Lab5.Models
{
    [DataContract]
    public class DocumentStatistics
    {
        [DataMember]
        public int DocumentCount { get; set; }

        [DataMember]
        public List<string> Documents { get; set; }

        [DataMember]
        public Dictionary<string, int> WordCounts { get; set; }

        public DocumentStatistics()
        {
            DocumentCount = 0;
            Documents = new List<string>();
            WordCounts = new Dictionary<string, int>();
        }
    }
}
