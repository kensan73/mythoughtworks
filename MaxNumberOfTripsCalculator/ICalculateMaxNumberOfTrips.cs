using System.Collections.Generic;

namespace MaxNumberOfTripsCalculator
{
    public interface ICalculateMaxNumberOfTrips
    {
        List<MaxTripsResult> Invoke(Dictionary<string, int> availableRoutes, string requestedStartEnd, int maxStops);
    }
}