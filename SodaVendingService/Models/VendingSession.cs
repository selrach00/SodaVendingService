using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SodaVendingService.Models
{
    public class VendingSession
    {
        public VendingStates VendingState { get; set; }
        public double CoinsInTray { get; set; }
        public int SelectedSodaId { get; set; }

        public void InsertQuarter()
        {
            if (VendingState != VendingStates.HasQuarters)
            {
                VendingState = VendingStates.HasQuarters;
            }
            else
            {
                DispenseQuarter();
            }
        }

        public void DispenseQuarter()
        {
            CoinsInTray += .25;
        }

        // Can't differentiate between the state being out of quarters and sold out at the same time. Recommend changing design to using two booleans, HasQuarters and SoldOut
        public void DispenseSoda(int sodaId, IList<Soda> sodas)
        {
            var soda = sodas.First(x => x.SodaId == sodaId);

            if (soda.SodaStock < 1)
            {
                VendingState = VendingStates.SoldOut;
                return;
            }

            if (VendingState == VendingStates.HasQuarters)
            {
                VendingState = VendingStates.Sold;
                CoinsInTray = 0;
                DecrementSodaStock(sodaId, sodas);
                UpdateSodaInventory(sodas);
            }
            else
            {
                VendingState = VendingStates.NoQuarters;
            }
        }

        private void UpdateSodaInventory(IList<Soda> sodas)
        {
           var json = Newtonsoft.Json.JsonConvert.SerializeObject(sodas);
           File.WriteAllText("wwwroot/inventory.json", json);
        }

        private void DecrementSodaStock(int sodaId, IList<Soda> sodas)
        {
            foreach (var soda in sodas)
            {
                if (soda.SodaId == sodaId)
                {
                    soda.SodaStock -= 1;
                }
            }
        }
    }

}
