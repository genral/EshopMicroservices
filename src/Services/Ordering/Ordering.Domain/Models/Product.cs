﻿using Ordering.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class Product:Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
          
        public static Product Create(ProductId id, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));    

            return new Product { Id=id, Name = name, Price = price };
        }
    }
}
