using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            //ex: 13/5 (count/pageSize) would return 2.xx - Ceiling would give 3
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, 
                                            int pageNumber, int pageSize)
        {
            // this is actually calling the BE for count
            var count = await source.CountAsync();
            
            // the method below is actually executing a query, that's why we have await and ToListAsync()
            //ex: if there are 13 items and you want to go to the 2nd page then
            //pageNumber would be 2, so (pageNumber -1 ) would be 1 * pageSize = 5, so skip first 5
            //then take 5 after the first 5
            var items = await source.Skip((pageNumber - 1)* pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}