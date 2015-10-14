using System.Collections.Generic;
using NUnit.Framework;

namespace MaxNumberOfTripsCalculator.Tests
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

        [Test]
        public void CanCreate()
        {

        }

        [TestCase("AC")]
        [TestCase("AC")]
        [TestCase("CC")]
        [TestCase("CD")]
        public void ReturnsEmptyOutputForEmptyAvailableRoutes(string desiredStartEnd)
        {
            var availableRoutes = new Dictionary<string, int>();

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, 5);

            AssertEmptyResults(result);
        }

        private static void AssertEmptyResults(List<MaxTripsResult> result)
        {
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [TestCase("")]
        [TestCase(null)]
        public void ReturnsEmptyOutputForEmptyDesiredStart(string desiredStartEnd)
        {
            var availableRoutes = new Dictionary<string, int>
            {
                { "AB", 3 }
            };

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, 5);

            AssertEmptyResults(result);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]

        public void ReturnsEmptyOutputForLessThan1MaxStops(int maxStops)
        {
            const string route = "AB";

            var availableRoutes = new Dictionary<string, int>
            {
                { route, 3 }
            };

            var desiredStartEnd = route;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            AssertEmptyResults(result);
        }

        [Test]

        public void OnlyOneRouteAvailDoesntMatchRequest()
        {
            const string route = "AB";

            var availableRoutes = new Dictionary<string, int>
            {
                { route, 3 }
            };

            var desiredStartEnd = "BC";
            const int maxStops = 1;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            AssertEmptyResults(result);
        }

        [Test]

        public void OnlyOneRouteAvailMatchesRequest()
        {
            const string route = "AB";

            const int length = 3;
            var availableRoutes = new Dictionary<string, int>
            {
                { route, length }
            };

            var desiredStartEnd = route;
            const int maxStops = 1;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Legs.Count, Is.EqualTo(1));
            Assert.That(result[0].Legs[0].Distance, Is.EqualTo(length));
        }

        [Test]

        public void TwoLegsOneRouteMatches()
        {
            const string route = "AC";

            const int length = 3;
            const int length2 = length + 5;
            const string leg1 = "AB";
            const string leg2 = "BC";

            var availableRoutes = new Dictionary<string, int>
            {
                { leg1, length },
                { leg2, length2 }
            };

            var desiredStartEnd = route;
            const int maxStops = 1;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Legs.Count, Is.EqualTo(2));
            Assert.That(result[0].Legs[0].Distance, Is.EqualTo(length));
            Assert.That(result[0].Legs[0].Leg, Is.EqualTo(leg1));
            Assert.That(result[0].Legs[1].Distance, Is.EqualTo(length2));
            Assert.That(result[0].Legs[1].Leg, Is.EqualTo(leg2));
        }

        [Test]

        public void ThreeLegs_ExceedsMaxStops()
        {
            const string route = "AD";

            const int length = 3;
            const int length2 = length + 5;
            const int length3 = length + 522;
            const string leg1 = "AB";
            const string leg2 = "BC";
            const string leg3 = "CD";

            var availableRoutes = new Dictionary<string, int>
            {
                { leg1, length },
                { leg2, length2 },
                { leg3, length3 },
            };

            var desiredStartEnd = route;
            const int maxStops = 1;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]

        public void TwoPossibleRoutesBothMatch()
        {
            const string route = "AC";

            const int length = 3;
            const int length2 = length + 5;
            const int length3 = length + 77;
            const int length4 = length + 235;
            const string leg1 = "AB";
            const string leg2 = "BC";
            const string leg3 = "AD";
            const string leg4 = "DC";

            var availableRoutes = new Dictionary<string, int>
            {
                { leg1, length },
                { leg2, length2 },
                { leg3, length3 },
                { leg4, length4 },

            };

            var desiredStartEnd = route;
            const int maxStops = 1;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].Legs.Count, Is.EqualTo(2));
            Assert.That(result[0].Legs[0].Distance, Is.EqualTo(length));
            Assert.That(result[0].Legs[0].Leg, Is.EqualTo(leg1));
            Assert.That(result[0].Legs[1].Distance, Is.EqualTo(length2));
            Assert.That(result[0].Legs[1].Leg, Is.EqualTo(leg2));

            Assert.That(result[1].Legs[0].Distance, Is.EqualTo(length3));
            Assert.That(result[1].Legs[0].Leg, Is.EqualTo(leg3));
            Assert.That(result[1].Legs[1].Distance, Is.EqualTo(length4));
            Assert.That(result[1].Legs[1].Leg, Is.EqualTo(leg4));
        }

        [Test]

        public void ThreePossibleRoutesTwoMatch()
        {
            const string route = "AC";

            const int length1 = 3;
            const int length2 = length1 + 5;
            const int length3 = length1 + 77;
            const int length4 = length1 + 235;
            const int length5 = length1 + 323;
            const int length6 = length1 + 5890;
            const string leg1 = "AB";
            const string leg2 = "BC";
            const string leg3 = "AD";
            const string leg4 = "DC";
            const string leg5 = "AE";
            const string leg6 = "EF";

            var availableRoutes = new Dictionary<string, int>
            {
                { leg1, length1 },
                { leg2, length2 },
                { leg3, length3 },
                { leg4, length4 },
                { leg5, length5 },
                { leg6, length6 },
            };

            var desiredStartEnd = route;
            const int maxStops = 1;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].Legs.Count, Is.EqualTo(2));
            Assert.That(result[0].Legs[0].Distance, Is.EqualTo(length1));
            Assert.That(result[0].Legs[0].Leg, Is.EqualTo(leg1));
            Assert.That(result[0].Legs[1].Distance, Is.EqualTo(length2));
            Assert.That(result[0].Legs[1].Leg, Is.EqualTo(leg2));

            Assert.That(result[1].Legs[0].Distance, Is.EqualTo(length3));
            Assert.That(result[1].Legs[0].Leg, Is.EqualTo(leg3));
            Assert.That(result[1].Legs[1].Distance, Is.EqualTo(length4));
            Assert.That(result[1].Legs[1].Leg, Is.EqualTo(leg4));
        }

        [Test]

        public void ThreePossibleRoutesTwoMatch_JumbledAvailableRoutes()
        {
            const string route = "AC";

            const int length1 = 3;
            const int length2 = length1 + 5;
            const int length3 = length1 + 77;
            const int length4 = length1 + 235;
            const int length5 = length1 + 323;
            const int length6 = length1 + 5890;
            const string leg1 = "AB";
            const string leg2 = "BC";
            const string leg3 = "AD";
            const string leg4 = "DC";
            const string leg5 = "AE";
            const string leg6 = "EF";

            var availableRoutes = new Dictionary<string, int>
            {
                { leg6, length6 },
                { leg1, length1 },
                { leg3, length3 },
                { leg2, length2 },
                { leg5, length5 },
                { leg4, length4 },
            };

            var desiredStartEnd = route;
            const int maxStops = 1;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].Legs.Count, Is.EqualTo(2));
            Assert.That(result[0].Legs[0].Distance, Is.EqualTo(length1));
            Assert.That(result[0].Legs[0].Leg, Is.EqualTo(leg1));
            Assert.That(result[0].Legs[1].Distance, Is.EqualTo(length2));
            Assert.That(result[0].Legs[1].Leg, Is.EqualTo(leg2));

            Assert.That(result[1].Legs[0].Distance, Is.EqualTo(length3));
            Assert.That(result[1].Legs[0].Leg, Is.EqualTo(leg3));
            Assert.That(result[1].Legs[1].Distance, Is.EqualTo(length4));
            Assert.That(result[1].Legs[1].Leg, Is.EqualTo(leg4));
        }

        [Test]

        public void Thought6()
        {
            const string route = "CC";

            var availableRoutes = new Dictionary<string, int>
            {
                { "AB", 5 },
                { "BC", 4 },
                { "CD", 8 },
                { "DC", 8 },
                { "DE", 6 },
                { "AD", 5 },
                { "CE", 2 },
                { "EB", 3 },
                { "AE", 7 },
            };

            var desiredStartEnd = route;
            const int maxStops = 3;

            var result = _calculator.Invoke(availableRoutes, desiredStartEnd, maxStops);

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].Legs.Count, Is.EqualTo(2));
            Assert.That(result[0].Legs[0].Leg, Is.EqualTo("CD"));
            Assert.That(result[0].Legs[1].Leg, Is.EqualTo("DC"));

            Assert.That(result[1].Legs.Count, Is.EqualTo(3));
            Assert.That(result[1].Legs[0].Leg, Is.EqualTo("CE"));
            Assert.That(result[1].Legs[1].Leg, Is.EqualTo("EB"));
            Assert.That(result[1].Legs[2].Leg, Is.EqualTo("BC"));
        }
    }
}
