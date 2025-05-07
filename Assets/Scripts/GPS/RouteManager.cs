using UnityEngine;

using UnproductiveProductions.StadsBingo.Global;
using UnproductiveProductions.StadsBingo.GPS.Utils;

namespace UnproductiveProductions.StadsBingo.GPS
{
    public class RouteManager : MonoBehaviour
    {
        public Route ActiveRoute;

        [Tooltip("The threshold to be considered with the destination in a radius.")]
        [SerializeField] private float radius = 10f;

        private int _currentIndex = 0;

        private void OnEnable()
        {
            EventSystem.Singleton.OnLocationUpdated += CheckDistanceToNextPOI;
        }

        private void OnDisable()
        {
            EventSystem.Singleton.OnLocationUpdated -= CheckDistanceToNextPOI;
        }

        private void CheckDistanceToNextPOI(double lat, double lon)
        {
            if (_currentIndex >= ActiveRoute.points.Length) return;

            POI poi = ActiveRoute.points[_currentIndex];
            double distance = GPSUtils.CalculateDistance(lat, lon, poi.Latitude, poi.Longitude);

            Debug.Log($"[{GetType().Name}] - Distance to target: {distance}");

            if (distance <= radius)
            {
                Debug.Log($"[{GetType().Name}] - Arrived at POI: {poi.Title}");
                _currentIndex++;

                // Send to UI
                EventSystem.Singleton.ArrivedAtLocation();
            }
            else if (UIManager.NotifiedUser && distance > radius) UIManager.Singleton.StopNotifying();
        }

        // TODO: Create a pathfinding system
        // TODO: Add possibly a map?
    }
}
