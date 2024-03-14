using System;
using SFG.Core.Domains.Shared;
using Microsoft.EntityFrameworkCore;

namespace SFG.Core.Commons
{
	public static class IQueryableExtension
	{
		public static async Task<PageList<T>> ToPageListAsync<T>(this IQueryable<T> source, int page, int take)
        {
            var skip = (page - 1) * take;
            var total = await source.CountAsync();
            var items = await source.Skip(skip).Take(take).ToListAsync();
            return new PageList<T>(total, items);
        }
	}
}

