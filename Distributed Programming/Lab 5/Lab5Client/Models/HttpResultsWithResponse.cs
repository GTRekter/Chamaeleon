using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Lab5Client
{
	public class HttpResultsWithResponse<OutputType> : HttpResults
	{
		public OutputType Result { get; set; }

        public HttpResultsWithResponse(HttpStatusCode statusCode, OutputType result) 
            : base(statusCode)
        {
            Result = result;
        }

        public HttpResultsWithResponse(HttpStatusCode statusCode, string error)
            : base(statusCode, error)
        {
        }
    }
}
