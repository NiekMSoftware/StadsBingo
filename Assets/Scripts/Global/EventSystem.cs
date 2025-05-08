using System;
using UnityEngine;
using UnproductiveProductions.StadsBingo.GPS;

namespace UnproductiveProductions.StadsBingo.Global
{
    public class EventSystem : MonoBehaviour
    {
        #region Events

        // location events
        public event Action<double, double> OnLocationUpdated;
        public event Action OnLocationArrived;

        // map events
        public event Action<POI> OnPOISelected;
        public event Action<Route> OnRouteChanged;
        public event Action OnMapReady;

        #endregion

        #region Singleton

        public static EventSystem Singleton { get; private set; }

        private void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            DontDestroyOnLoad(this);
            Debug.Log($"[{GetType().Name}] - Created a Singleton instance successfully.");
        }

        #endregion

        #region Event Invokers

        public void UpdateCoordinates(double lat, double lon)
        {
            OnLocationUpdated?.Invoke(lat, lon);
            Debug.Log($"[{GetType().Name}] - Updated coordinates: {lat}, {lon}");
        }

        public void ArrivedAtLocation()
        {
            OnLocationArrived?.Invoke();
            Debug.Log($"[{GetType().Name}] - Arrived at location.");
        }

        public void SelectPOI(POI poi)
        {
            OnPOISelected?.Invoke(poi);
            Debug.Log($"[{GetType().Name}] - Selected POI: {poi.Title}");
        }

        public void ChangeRoute(Route route)
        {
            OnRouteChanged?.Invoke(route);
            Debug.Log($"[{GetType().Name}] - Changed route to: {route.RouteName}");
        }

        public void MapReady()
        {
            OnMapReady?.Invoke();
            Debug.Log($"[{GetType().Name}] - Map is ready.");
        }

        #endregion
    }
}
