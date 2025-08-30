using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEnitity>
        (int pageIndex, int pageSize, long count, IEnumerable<TEnitity> data)
        where TEnitity : class 
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; }=pageSize;
        public long Count { get; } = count;
        public IEnumerable<TEnitity> Data { get; } = data;
    } 
}
