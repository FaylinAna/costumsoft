namespace CUSTOMSOFT.API.Models
{
    public class RequestResponseLog
    {
        public DateTime DateTime { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string RequestParams { get; set; }
        public string RequestBody { get; set; } 
        public string ResponseStatusCode { get; set; }
        public string ResponseBody { get; set; } 

        public RequestResponseLog(DateTime dateTime, string requestMethod, string requestPath, string responseStatusCode, string requestParams = null, string requestBody = null, string responseBody = null)
        {
            DateTime = dateTime;
            RequestMethod = requestMethod;
            RequestPath = requestPath;
            ResponseStatusCode = responseStatusCode;
            RequestParams = requestParams;
            RequestBody = requestBody;
            ResponseBody = responseBody;
        }

        public override string ToString()
        {
            string logString = $"[{DateTime}] {RequestMethod} {RequestPath} => {ResponseStatusCode}";

            if (!string.IsNullOrEmpty(RequestParams))
            {
                logString += Environment.NewLine + $"Request Params: {RequestParams}";
            }

            if (!string.IsNullOrEmpty(RequestBody))
            {
                logString += Environment.NewLine + $"Request Body: {RequestBody}";
            }

            if (!string.IsNullOrEmpty(ResponseBody))
            {
                logString += Environment.NewLine + $"Response Body: {ResponseBody}";
            }

            return logString;
        }
    }

}
