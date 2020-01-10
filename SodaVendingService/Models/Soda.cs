using System;
namespace SodaVendingService.Models
{
    public class Soda
    {
        public int SodaId { get; set; }
        public string SodaName { get; set; }
        public string SodaImageURL { get; set; }
        public int SodaStock { get; set; }
    }

}
