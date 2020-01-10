using System;
namespace SodaVendingService.Models
{
    public class Promotion
    {
        public bool IsActive { get; set; }
        public string PromotionMessage { get; set; }
        public string PromotionHitMessage { get; set; }
        public string PromotionMissMessage { get; set; }
        public int ProbabilityRange { get; set; }
        public bool CustomerWon { get; set; }

        public void PlayPromotion()
        {
            var random = new Random();

            CustomerWon = ProbabilityRange > random.Next(100);
        }
    }

}
