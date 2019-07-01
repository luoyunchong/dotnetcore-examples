using System.Collections.Generic;

namespace Webapi.FreeSql.Web
{
    public class PagedResultDto<T>
    {
        public long TotalCount { get; set; }

        public IReadOnlyList<T> Items
        {
            get => _items ?? (_items = new List<T>());
            set => _items = value;
        }
        private IReadOnlyList<T> _items;
        public PagedResultDto()
        {

        }
        public PagedResultDto(IReadOnlyList<T> items)
        {
            TotalCount = items.Count;
            Items = items;
        }
     

        public PagedResultDto(long totalCount, IReadOnlyList<T> items) : this(items)
        {
            TotalCount = totalCount;
        }
    }
}
