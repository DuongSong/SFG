using System;
namespace SFG.WebApi.Model
{
	public class ErrorMessage
	{
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

