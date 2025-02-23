﻿using ECommerceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Repositories
{
    internal interface IOrderWriteRepository:IWriteRepository<Order>
    {
    }
}
