using System;
using System.Collections.Generic;

namespace SodaVendingService.Models
{
    public class IndexViewModel
    {
        public VendingSession VendingSession { get; set; }
        public IList<Soda> Sodas { get; set; }
        public IList<Promotion> Promotions { get; set; }
    }
}
