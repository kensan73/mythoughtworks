using System.Collections.Generic;
using System.Linq;

namespace MaxNumberOfTripsCalculator
{
    public class Calculator
    {
        public List<MaxTripsResult> Invoke(Dictionary<string, int> availableRoutes, string requestedStartEnd, int maxStops)
        {
            var maxTripsResults = new List<MaxTripsResult>();

            if (string.IsNullOrEmpty(requestedStartEnd))
                return maxTripsResults;

            if(maxStops < 1)
                return maxTripsResults;

            if (requestedStartEnd.Length == 2 && availableRoutes.ContainsKey(requestedStartEnd))
            {
                var legs = from routes in availableRoutes
                           select new Trip { Leg = routes.Key, Distance = routes.Value };
                maxTripsResults.Add(new MaxTripsResult
                {
                    Legs = legs.ToList()
                });

                return maxTripsResults;
            }

            return DoDepthFirstSearching(availableRoutes, requestedStartEnd, maxStops);
        }

        private static List<MaxTripsResult> DoDepthFirstSearching(Dictionary<string, int> availableRoutes, string requestedRoute, int maxStops)
        {
            var results = new List<MaxTripsResult>();
            
            
            var possibleLegs = availableRoutes.Where(r => r.Key[0] == requestedRoute[0]);


            foreach (var legs in possibleLegs)
            {
                var tempResult = new MaxTripsResult {Legs = new List<Trip>()};
                var runningStops = 0;
                if (DoDepthFirstSearchingAux(availableRoutes, ref tempResult, legs.Key, requestedRoute, ref runningStops, maxStops))
                {
                    results.Add(tempResult);
                };
            }

            return results;
        }

        private static bool DoDepthFirstSearchingAux(Dictionary<string, int> existingRoutes, ref MaxTripsResult runningResult, string fromToKey, string requestedRoute, ref int runningStops, int maxStops)
        {
            // Concept: the minute you have entered this function, you have gone from...to.  (See fromToKey.)  So update recordkeeping now.
            UpdateRunningStops(ref runningStops);
            if (!CheckIfMaxStopsExceeded(runningStops, maxStops)) return false;

            runningResult.Legs.Add(new Trip{Leg = fromToKey, Distance = existingRoutes[fromToKey]});

            var requestedDest = requestedRoute[1];
            var currentDest = fromToKey[1];

            if (YouHaveReachedYourDestination(requestedDest, currentDest))
                return true;

            var nextLocation = currentDest;
            var possibleRoutes = existingRoutes.Where(r => r.Key[0] == nextLocation);

            if (!possibleRoutes.Any())
                return false;

            foreach (var route in possibleRoutes)
            {
                var savedRunningResult = runningResult;
                if (DoDepthFirstSearchingAux(existingRoutes, ref savedRunningResult, route.Key, requestedRoute, ref runningStops, maxStops))
                {
                    return true;
                }
                runningResult = savedRunningResult;
            }

            return false;
        }

        private static void UpdateRunningStops(ref int runningStops)
        {
            runningStops++;
        }

        private static bool CheckIfMaxStopsExceeded(int runningStops, int maxStops)
        {
            if (runningStops > maxStops)
                return false;
            return true;
        }

        private static bool YouHaveReachedYourDestination(char requestedDest, char currentDest)
        {
            return requestedDest == currentDest;
        }
    }
}