using System;
using UnityEngine;

namespace UnproductiveProductions.StadsBingo.Global
{
    public class EventSystem : MonoBehaviour
    {
        public event Action<double, double> OnLocationUpdated;

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

        public void UpdateCoordinates(double log, double lat)
        {
            OnLocationUpdated?.Invoke(log, lat);
        }
    }
}
