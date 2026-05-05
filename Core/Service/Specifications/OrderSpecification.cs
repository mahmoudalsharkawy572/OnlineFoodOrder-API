using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class OrderSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderSpecification(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(O => O.OrderItems);
        }
        public OrderSpecification(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(O => O.OrderItems);

            AddOrderBy(OrderBy => OrderBy.OrderDate);
        }


    }
}
