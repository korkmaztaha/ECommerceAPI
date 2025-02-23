using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Repositories
{
    public interface ICustomerWriteRepository:IWriteRepository<Customer>
    {
    }
}
