using System.Collections.Generic;

namespace E_Coffee.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        void SaveOrder(Order order);
    }

}
