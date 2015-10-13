using System.Collections.Generic;
using System.Linq;

namespace Thoughtworks_Train
{
    public class Calculator
    {
        public string Invoke(Dictionary<string, int> existingRoutes, string requestedRoute)
        {
            if(!existingRoutes.Any() || requestedRoute.Length < 2)
                return "NO SUCH ROUTE";

            if(existingRoutes.ContainsKey(requestedRoute) && requestedRoute.Length == 2)
                return existingRoutes[requestedRoute].ToString();

            return DoDepthFirstSearching(existingRoutes, requestedRoute);
        }

        private static string DoDepthFirstSearching(Dictionary<string, int> existingRoutes, string requestedRoute)
        {
            var possibleLegs = existingRoutes.Where(r => r.Key == requestedRoute.Substring(0, 2));
            
            foreach (var legs in possibleLegs)
            {
                var weight = 0;
                if (DoDepthFirstSearchingAux(existingRoutes, ref weight, legs.Key, requestedRoute))
                {
                    return weight.ToString();
                };
            }

            return "NO SUCH ROUTE";
        }

        private static bool DoDepthFirstSearchingAux(Dictionary<string, int> existingRoutes, ref int runningWeight, string fromToKey, string requestedRoute)
        {
            // Concept: the minute you have entered this function, you have gone from...to.  (See fromToKey.)  So update recordkeeping now.
            runningWeight += existingRoutes[fromToKey];

            if (YouHaveReachedYourDestination(requestedRoute))
                return true;

            var nextLocation = requestedRoute.Substring(1);
            var possibleRoutes = existingRoutes.Where(r => r.Key == nextLocation.Substring(0, 2));

            if (!possibleRoutes.Any())
                return false;

            foreach (var route in possibleRoutes)
            {
                var savedWeight = runningWeight;
                if (DoDepthFirstSearchingAux(existingRoutes, ref runningWeight, route.Key, nextLocation))
                {
                    return true;
                }
                runningWeight = savedWeight;
            }

            return false;
        }

        private static bool YouHaveReachedYourDestination(string requestedRoute)
        {
            return requestedRoute.Length == 2;
        }
    }
}
