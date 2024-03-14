using System;
namespace SFG.Core.Commons
{
	public class CustomExceptionHandler : Exception
	{
		public string ErrorMessage { get; set; }

		public CustomExceptionHandler(string errorMessage)
			:base(errorMessage)
		{
			ErrorMessage = errorMessage;
		}
	}
}

