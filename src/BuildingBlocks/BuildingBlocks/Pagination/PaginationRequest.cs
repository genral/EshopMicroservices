using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Pagination
{
    public record PaginationRequest(int pageIndex=0, int pageSize=10);
}
