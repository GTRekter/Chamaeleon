using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Lab5Client
{
	public class HttpResults
	{
		public HttpStatusCode StatusCode { get; set; }
		public string Error { get; set; }
		public string RawData { get; set; }

        public HttpResults(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public HttpResults(HttpStatusCode statusCode, string error)
        {
            StatusCode = statusCode;
            Error = error;
        }
    }
}
