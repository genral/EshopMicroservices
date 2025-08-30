using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public interface IEnitity<T>: IEntity
    {
        T Id { get; set; }
    }
    public interface IEntity
    {
        DateTime? CreatedAt { get; set; }
        string? CreatedBy { get; set; }
        DateTime? LastModified { get; set; }
        string? LastModifiedBy { get; set; }
    }
}
