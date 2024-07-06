using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;

namespace CUSTOMSOFT.API.Models
{
    public class ErrorDetails : ProblemDetails
    {
        public new int? StatusCode { get; set; }
        public new string Title { get; set; }
        public new string Detail { get; set; }

        public ErrorDetails(int statusCode, string message,string instance, object content = null)
        {
            StatusCode = statusCode;
            Title = message;
            Detail = content?.ToString();
            Instance = instance ?? string.Empty;
            
        }

        public ErrorDetails(Exception ex)
        {
            StatusCode = ex.HResult;
            Title = ex.Message;
            Detail = ex.ToString();
        }

        public ErrorDetails(WebException ex)
        {
            StatusCode = (int)((HttpWebResponse)ex.Response).StatusCode;
            Title = ex.Message;

            var responseStream = ex.Response.GetResponseStream();
            if (responseStream != null)
            {
                using (var reader = new StreamReader(responseStream))
                {
                    Detail = reader.ReadToEnd();
                }
            }
        }
    }
}
