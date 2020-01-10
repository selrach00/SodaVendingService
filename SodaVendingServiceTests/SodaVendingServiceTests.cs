using NUnit.Framework;
using SodaVendingService.Models;

namespace SodaVendingServiceTests
{
    public class SodaVendingServiceTests
    {
        // Requirements
        // 1. Four states will be maintained within the service
        //  -Sold Out
        //  -No Quarters
        //  -Has Quarters
        //  -Sold
        // 2. The service will facilitate four actions
        //  -Recognize a quarter has been inserted
        //  -Dispense quarter
        //  -Capture selection of soda
        //  -Dispensing of the soda
        // 3. A GUI is necessary to facilitate the actions within requirement #2
        // 4. Purchases made using the service will record transactional data
        // 5. A microservice that reports inventory of soda will be implemented
        // 6. An easily configurable/adaptable "event" (giveaways, contests) system will be implemented.

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInsertQuarterChangesVendingState()
        {
           
            VendingSession session = new VendingSession();
            session.VendingState = VendingStates.NoQuarters;
            session.InsertQuarter();
            Assert.AreEqual(session.VendingState, VendingStates.HasQuarters);
        }
        [Test]
        public void TestDispenseQuarterWhenAlreadyHasQuarter()
        {
            VendingSession session = new VendingSession();
            session.VendingState = VendingStates.HasQuarters;
            session.InsertQuarter();
            Assert.AreEqual(session.CoinsInTray, .25);
        }
  
        [Test]
        public void TestDispenseSoda()
        {
            VendingSession session = new VendingSession();
            session.VendingState = VendingStates.HasQuarters;
            //Set the SodaSelected property 
            //DispenseSoda()
            // Assert.AreEqual(session.VendingState, VendingStates.Sold);
        }

        [Test]
        public void TestRecordTransaction()
        {
            Assert.Pass();
        }

        [Test]
        public void TestRestock()
        {
            Assert.Pass();
        }

        [Test]
        public static void TestDestock()
        {
            Assert.Pass();
        }

        [Test]
        public static void TestEvent()
        {
            //Create a new Event
            //Event.TryYourLuck
            //Assert that the Event.Result property is True or False
        }
    }
}