using System.Collections.Generic;
using MaxNumberOfTripsCalculator;
using NUnit.Framework;
using Rhino.Mocks;

namespace ShortestDistanceCalculator.Tests
{
    [TestFixture]

    public class WhenInvoking
    {
        private Calculator _calcer;
        private ICalculateMaxNumberOfTrips _maxNumberOfTripsCalculator;

        [SetUp]
        public void SetUp()
        {
            _maxNumberOfTripsCalculator = MockRepository.GenerateStrictMock<ICalculateMaxNumberOfTrips>();

            _calcer = new Calculator(_maxNumberOfTripsCalculator);
        }

        [TearDown]
        public void TearDown()
        {
            _maxNumberOfTripsCalculator.VerifyAllExpectations();
        }

        [Test]
        public void NoResultsReturnsNegOne()
        {
            const string requestedTrip = "AB";
            var allPossibleLegs = new Dictionary<string, int>{ {"CD", 7}};
            var noResults = new List<MaxTripsResult>();

            _maxNumberOfTripsCalculator.Expect(
                c => c.Invoke(Arg<Dictionary<string, int>>.Is.Anything, Arg<string>.Is.Anything, Arg<int>.Is.Equal(int.MaxValue))).Return(noResults);

            var result = _calcer.Invoke(allPossibleLegs, requestedTrip);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void OneResultReturnsOneDistance()
        {
            const string requestedTrip = "AB";
            const int distance = 7;
            var allPossibleLegs = new Dictionary<string, int>{ {requestedTrip, distance}};
            var oneResult = new List<MaxTripsResult>
            {
                new MaxTripsResult {Legs = new List<Trip> {new Trip {Leg = requestedTrip, Distance = distance}}}
            };

            _maxNumberOfTripsCalculator.Expect(
                c => c.Invoke(allPossibleLegs, requestedTrip, int.MaxValue)).Return(oneResult);

            var result = _calcer.Invoke(allPossibleLegs, requestedTrip);

            Assert.That(result, Is.EqualTo(distance));
        }

        [Test]
        public void TwoResultsReturnsShorterDistance()
        {
            const string requestedTrip = "AB";
            const int distance = 7;
            const int shorterDistance = 3;
            var allPossibleLegs = new Dictionary<string, int>{ {requestedTrip, distance}};
            var twoResults = new List<MaxTripsResult>
            {
                new MaxTripsResult {Legs = new List<Trip> {new Trip {Leg = requestedTrip, Distance = distance}}},
                new MaxTripsResult {Legs = new List<Trip> {new Trip {Leg = requestedTrip, Distance = shorterDistance}}},
            };

            _maxNumberOfTripsCalculator.Expect(
                c => c.Invoke(allPossibleLegs, requestedTrip, int.MaxValue)).Return(twoResults);

            var result = _calcer.Invoke(allPossibleLegs, requestedTrip);

            Assert.That(result, Is.EqualTo(shorterDistance));
        }
    }
}
