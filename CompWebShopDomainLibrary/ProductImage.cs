﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompWebShopDomainLibrary
{
    public class ProductImage
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; } = default!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}
