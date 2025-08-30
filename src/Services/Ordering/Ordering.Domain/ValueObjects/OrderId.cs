 

namespace Ordering.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; }

        private OrderId(Guid value)=>Value=value;

        public static OrderId Of(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            if (id == Guid.Empty)
            {
                throw new DomainException("OrderId cannnot be Empty");
            }
            return new OrderId(id);
        }
    }
}
