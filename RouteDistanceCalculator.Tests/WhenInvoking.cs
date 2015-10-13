using System.Collections.Generic;
using NUnit.Framework;
using Thoughtworks_Train;

namespace RouteDistanceCalculator.Tests
{
    [TestFixture]

    public class WhenInvoking
    {
        private Calculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator();
        }

        [TearDown]
        public void TearDown()
        {

        }
 
        [TestCase("ABC")]
        [TestCase("AD")]
        [TestCase("ADC")]
        [TestCase("ADEBCD")]
        [TestCase("AED")]
        public void NoSuchRouteOnEmptyInput(string requestedRoute)
        {
            var noExistingRoutes = new Dictionary<string, int>();

            var result = _calculator.Invoke(noExistingRoutes, requestedRoute);

            Assert.That(result, Is.EqualTo("NO SUCH ROUTE"));
        }

        [TestCase("Z")]
        [TestCase("C")]
        public void NoSuchRouteOnTooShortInput(string requestedRoute)
        {
            var noExistingRoutes = new Dictionary<string, int>();

            var result = _calculator.Invoke(noExistingRoutes, requestedRoute);

            Assert.That(result, Is.EqualTo("NO SUCH ROUTE"));
        }

        [TestCase("ABC")]
        [TestCase("AD")]
        [TestCase("ADC")]
        [TestCase("ADEBCD")]
        [TestCase("AED")]
        public void OnSingleInputRouteNotMatchingExistingRoute(string requestedRouteThatStartsWithA)
        {
            var singleRoute = new Dictionary<string, int>
            {
                {"DZ", 29}
            };

            var result = _calculator.Invoke(singleRoute, requestedRouteThatStartsWithA);

            Assert.That(result, Is.EqualTo("NO SUCH ROUTE"));
        }

        [TestCase("AD", 7)]
        [TestCase("BC", 11)]
        [TestCase("CD", 37)]
        public void OnSingleInputRouteMatchingExistingRoute(string requestedRoute, int distance)
        {
            var source = requestedRoute;
            var singleRoute = new Dictionary<string, int>
            {
                {source, distance}
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo(distance.ToString()));
        }

        [Test]
        public void OnSingleInputRouteMatchingThreeRouteDestinationDoesNotExist()
        {
            const string requestedRoute = "AD";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 3 },
                {"AC", 393 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo("NO SUCH ROUTE"));
        }

        [Test]
        public void OnSingleInputRouteMatchingThreeRouteDestinationChoosesDirectPathIgnoresIndirect()
        {
            const string requestedRoute = "AC";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 3 },
                {"BC", 77 },
                {"AC", 393 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo("393"));
        }

        [Test]
        public void On2LegRoute_2ndLegDoesntMatch_ReturnsNoSuchRoute()
        {
            const string requestedRoute = "AC";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 3 },
                {"BZ", 77 },
                {"BD", 393 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo("NO SUCH ROUTE"));
        }

        [Test]
        public void On2LegRoute_BothLegsMatch_ReturnsCorrectWeight()
        {
            const string requestedRoute = "ABC";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 3 },
                {"BZ", 77 },
                {"BC", 393 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            var one = 1;
            Assert.That(result, Is.EqualTo("396"));
        }

        [Test]
        public void ThoughtTest1()
        {
            const string requestedRoute = "ABC";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 5 },
                {"BC", 4 },
                {"CD", 8 },
                {"DC", 8 },
                {"DE", 6 },
                {"AD", 5 },
                {"CE", 2 },
                {"EB", 3 },
                {"AE", 7 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo("9"));
        }

        [Test]
        public void ThoughtTest2()
        {
            const string requestedRoute = "AD";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 5 },
                {"BC", 4 },
                {"CD", 8 },
                {"DC", 8 },
                {"DE", 6 },
                {"AD", 5 },
                {"CE", 2 },
                {"EB", 3 },
                {"AE", 7 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo("5"));
        }

        [Test]
        public void ThoughtTest3()
        {
            const string requestedRoute = "ADC";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 5 },
                {"BC", 4 },
                {"CD", 8 },
                {"DC", 8 },
                {"DE", 6 },
                {"AD", 5 },
                {"CE", 2 },
                {"EB", 3 },
                {"AE", 7 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo("13"));
        }

        [Test]
        public void ThoughtTest4()
        {
            const string requestedRoute = "AEBCD";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 5 },
                {"BC", 4 },
                {"CD", 8 },
                {"DC", 8 },
                {"DE", 6 },
                {"AD", 5 },
                {"CE", 2 },
                {"EB", 3 },
                {"AE", 7 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo("22"));
        }

        [Test]
        public void ThoughtTest5()
        {
            const string requestedRoute = "AED";
            var singleRoute = new Dictionary<string, int>
            {
                {"AB", 5 },
                {"BC", 4 },
                {"CD", 8 },
                {"DC", 8 },
                {"DE", 6 },
                {"AD", 5 },
                {"CE", 2 },
                {"EB", 3 },
                {"AE", 7 },
            };

            var result = _calculator.Invoke(singleRoute, requestedRoute);

            Assert.That(result, Is.EqualTo("NO SUCH ROUTE"));
        }
    }
}
