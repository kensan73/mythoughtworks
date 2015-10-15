using System.Collections.Generic;
using System.Linq;
using MaxNumberOfTripsCalculator;

namespace ExactStopsCalculator
{
    public class Calculator
    {
        private readonly ICalculateMaxNumberOfTrips _maxTripsCalculator;

        public Calculator(ICalculateMaxNumberOfTrips maxTripsCalculator)
        {
            _maxTripsCalculator = maxTripsCalculator;
        }

        public List<MaxTripsResult> InvokeDetails(Dictionary<string, int> allLegs, string requestedTrip, int exactStops)
        {
            return _maxTripsCalculator.Invoke(allLegs, requestedTrip, exactStops);


        }
        public int Invoke(Dictionary<string, int> allLegs, string requestedTrip, int exactStops)
        {
            var results = InvokeDetails(allLegs, requestedTrip, exactStops);

            return results.Count(result => ResultMatchesRequestedStops(exactStops, result));
        }

        private static bool ResultMatchesRequestedStops(int exactStops, MaxTripsResult result)
        {
            return result.Legs.Count - 1 == exactStops;
        }
    }
}
