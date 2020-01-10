using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SodaVendingService.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;

namespace SodaVendingService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Soda> sodas = JsonConvert.DeserializeObject<List<Soda>>(System.IO.File.ReadAllText("wwwroot/inventory.json"));
        private List<Promotion> promotions = JsonConvert.DeserializeObject<List<Promotion>>(System.IO.File.ReadAllText("wwwroot/promotions.json"));

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel
            {
                Promotions = promotions,
                Sodas = sodas,
                VendingSession = new VendingSession()
            };

            return View("Index", indexViewModel);
        }

        public IActionResult InsertQuarter(VendingSession session)
        {
            session.InsertQuarter();

            var indexViewModel = new IndexViewModel
            {
                Promotions = promotions,
                Sodas = sodas,
                VendingSession = session
            };

            return View("Index", indexViewModel);
        }

        public IActionResult DispenseSoda(VendingSession session)
        {
            session.DispenseSoda(session.SelectedSodaId, sodas);

            if (session.VendingState == VendingStates.Sold)
            {
                foreach (var promotion in promotions)
                {
                    if (promotion.IsActive)
                    {
                        promotion.PlayPromotion();
                    }
                }

                AddTransaction(session.SelectedSodaId);

            }

            var indexViewModel = new IndexViewModel
            {
                Promotions = promotions,
                Sodas = sodas,
                VendingSession = session
            };
            return View("Index", indexViewModel);
        }

        private void AddTransaction(int sodaId)
        {
            List<SodaTransaction> transactions;
            if (System.IO.File.Exists("wwwroot/transactions.json"))
            {
                var text = System.IO.File.ReadAllText("wwwroot/transactions.json");
                transactions = JsonConvert.DeserializeObject<List<SodaTransaction>>(text);

                if (transactions.Any(x => x.SodaId == sodaId))
                {
                    foreach (var transaction in transactions)
                    {
                        if (transaction.SodaId == sodaId)
                        {
                            transaction.TransactionDates.Add(DateTime.Now);
                        }
                    }
                }
                else
                {
                    transactions.Add(
                        new SodaTransaction
                        {
                            SodaId = sodaId,
                            TransactionDates = new List<DateTime>
                                {
                                    DateTime.Now
                                }
                        });
                }
            }
            else
            {
                transactions = new List<SodaTransaction>
                {
                    new SodaTransaction
                    {
                        SodaId = sodaId,
                        TransactionDates = new List<DateTime>
                        {
                            DateTime.Now
                        }
                    }
                };
            }
            var json = JsonConvert.SerializeObject(transactions);
            System.IO.File.WriteAllText("wwwroot/transactions.json", json);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
