using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"Order with Id {id} was not found")
    {
    }
}
