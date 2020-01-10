using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SodaVendingService.Models
{
    public class SodaTransaction
    {
        public int SodaId { get; set; }
        public IList<DateTime> TransactionDates { get; set; }
    }
}
