﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApplicationCore.Entities
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public DateTime DatePosted { get; set; }

        
        public double Price { get; set; }
        public string State { get; set; }
        
    }
}