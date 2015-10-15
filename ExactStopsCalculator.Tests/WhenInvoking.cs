using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxNumberOfTripsCalculator;
using NUnit.Framework;
using Rhino.Mocks;
using Calculator = ExactStopsCalculator.Calculator;

public partial class ExactStopsCalculatorTests
{
    [TestFixture]

    public class WhenInvoking
    {
        private Calculator _calcer;
        private ICalculateMaxNumberOfTrips _maxTripsCalculator;
        private Dictionary<string, int> _possibleLegs;

        [SetUp]
        public void SetUp()
        {
            _maxTripsCalculator = MockRepository.GenerateStrictMock<ICalculateMaxNumberOfTrips>();
            _possibleLegs = new Dictionary<string, int>
                {
                    {"AB", 8},
                    {"BC", 9},
                    {"CE", 9},
                    {"AD", 13}
                };

            _calcer = new Calculator(_maxTripsCalculator);
        }

        [TearDown]
        public void TearDown()
        {
            _maxTripsCalculator.VerifyAllExpectations();
        }

        [Test]
        public void OneTripReturnedExceedsMaxStops()
        {
            var requestedTrip = "AD";
            var exactStops = 1;
            var twoStopTrip = new List<Trip> { new Trip(), new Trip(), new Trip() };

            _maxTripsCalculator.Expect(
                mtc =>
                    mtc.Invoke(Arg<Dictionary<string, int>>.Is.Anything, Arg<string>.Is.Anything,
                        Arg<int>.Is.Anything)).Return(new List<MaxTripsResult>
                            {
                                new MaxTripsResult{Legs = twoStopTrip},
                            });

            var result = _calcer.Invoke(_possibleLegs, requestedTrip, exactStops);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void OneTripReturnedEqualsMaxStops()
        {
            var requestedTrip = "AD";
            var exactStops = 2;
            var twoStopTrip = new List<Trip> { new Trip(), new Trip(), new Trip() };
            var maxTripsResult = new MaxTripsResult { Legs = twoStopTrip };
            var oneTripReturned = new List<MaxTripsResult>
                {
                    maxTripsResult,
                };

            _maxTripsCalculator.Expect(
                mtc =>
                    mtc.Invoke(Arg<Dictionary<string, int>>.Is.Anything, Arg<string>.Is.Anything,
                        Arg<int>.Is.Anything)).Return(oneTripReturned);

            var result = _calcer.Invoke(_possibleLegs, requestedTrip, exactStops);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void OneTripReturnedLessThanMaxStops()
        {
            var requestedTrip = "AD";
            var exactStops = 2;
            var threeStopTrip = new List<Trip> { new Trip(), new Trip(), new Trip(), new Trip() };
            var maxTripsResult = new MaxTripsResult { Legs = threeStopTrip };
            var oneTripReturned = new List<MaxTripsResult>
                {
                    maxTripsResult,
                };

            _maxTripsCalculator.Expect(
                mtc =>
                    mtc.Invoke(Arg<Dictionary<string, int>>.Is.Anything, Arg<string>.Is.Anything,
                        Arg<int>.Is.Anything)).Return(oneTripReturned);

            var numTrips = _calcer.Invoke(_possibleLegs, requestedTrip, exactStops);

            Assert.That(numTrips, Is.EqualTo(0));
        }
    }
}
