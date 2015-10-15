using System.Collections.Generic;
using System.Linq;
using MaxNumberOfTripsCalculator;

namespace ShortestDistanceCalculator
{
    public class Calculator
    {
        private readonly ICalculateMaxNumberOfTrips _maxNumberOfTripsCalculator;

        public Calculator(ICalculateMaxNumberOfTrips maxNumberOfTripsCalculator)
        {
            _maxNumberOfTripsCalculator = maxNumberOfTripsCalculator;
        }

        public int Invoke(Dictionary<string, int> allPossibleLegs, string requestedTrip)
        {
            var results = _maxNumberOfTripsCalculator.Invoke(allPossibleLegs, requestedTrip, int.MaxValue);

            if (!results.Any())
                return -1;

            return results.Select(result => result.Legs.Select(l => l.Distance).Sum()).Concat(new[] {int.MaxValue}).Min();
        }
    }
}