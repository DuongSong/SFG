using System;
namespace SFG.Core.Domains.Shared
{
	public class QueryParam
	{
		public string Search { get; set; } = string.Empty;
		public int Page { get; set; }
		public int Take { get; set; }
	}
}

