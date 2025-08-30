 
namespace Ordering.Domain.ValueObjects
{
    public class OrderItemId
    {
        public Guid Value { get; }
        private OrderItemId(Guid value){ this.Value = value; }

        public static OrderItemId Of(Guid value) {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItemId cannnot be Empty");
            }
            return new OrderItemId(value); 
        }

    }
}
