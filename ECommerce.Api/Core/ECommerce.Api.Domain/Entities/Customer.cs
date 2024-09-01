using ECommerce.Api.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
