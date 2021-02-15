using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Data
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public bool HasNext => CurrentPage < PageCount;
        public bool HasPrevious => CurrentPage > 1;
        public int PageCount { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        
        public PagedList(List<T> items, int count, int page, int pageSize)
        {
            CurrentPage = page;
            PageSize = pageSize;
            TotalCount = count;
            PageCount = (int) Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> sequence, int page, int pageSize)
        {
            var count = sequence.Count();
            var skip = (page - 1) * pageSize;
            var items = sequence.Skip(skip).Take(pageSize).ToList();

            return new PagedList<T>(items, count, page, pageSize);
        }
    }
}