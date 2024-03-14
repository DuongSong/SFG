using System;
namespace SFG.Core.Domains.Shared
{
	public class PageList<T>
	{
		public int Total { get; set; }
		public List<T> Items { get; set; }

		public PageList() { }

		public PageList(int total, List<T> items)
		{
			this.Total = total;
			this.Items = items;
		}
	}
}

