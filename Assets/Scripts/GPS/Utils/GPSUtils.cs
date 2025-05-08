using UnityEngine;

namespace UnproductiveProductions.StadsBingo.GPS.Utils
{
    public class GPSUtils
    {
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // radius in meters (earth)
            var R = 6371e3;

            // formula used can be found here: https://en.wikipedia.org/wiki/Haversine_formula
            var lonRad1 = lon1 * Mathf.Deg2Rad;
            var lonRad2 = lon2 * Mathf.Deg2Rad;
            var latRad1 = lat1 * Mathf.Deg2Rad;
            var latRad2 = lat2 * Mathf.Deg2Rad;

            var deltaLat = latRad2 - latRad1;
            var deltaLon = lonRad2 - lonRad1;

            var a = Mathf.Sin((float)deltaLat / 2) * Mathf.Sin((float)deltaLat / 2) +
                    Mathf.Cos((float)latRad1) * Mathf.Cos((float)latRad2) *
                    Mathf.Sin((float)deltaLon / 2) * Mathf.Sin((float)deltaLon / 2);

            var c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1 - a)));

            return R * c;  // distance in meters
        }        
    }
}
