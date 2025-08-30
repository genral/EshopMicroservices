
 
namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get;}
        private CustomerId(Guid id) => Value = id;

        public static CustomerId Of(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            if (id == Guid.Empty)
            {
                throw new DomainException("CustomerId cannnot be Empty");
            }
            return new CustomerId(id);
        }
    }
}
