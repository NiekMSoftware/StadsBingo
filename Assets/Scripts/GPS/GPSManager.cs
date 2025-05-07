using System.Collections;

using UnproductiveProductions.StadsBingo.Global;

using UnityEngine;
using UnityEngine.Android;

namespace UnproductiveProductions.StadsBingo.GPS
{
    public class GPSManager : MonoBehaviour
    {
        [Tooltip("The update interval the coordinates gets updated.")]
        [SerializeField, Min(1)] private float updateInterval;

        // Location properties
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        #region Singleton

        public static GPSManager Singleton { get; private set; }

        private void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
                Debug.LogWarning($"[{GetType().Name}] - Another Singleton instance found, destroying other Singleton...");
                return;
            }

            Singleton = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log($"[{GetType().Name}] - Created a Singleton successfully.");
        }

        #endregion

        private IEnumerator Start()
        {
            // request access to location
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                Debug.Log($"[{GetType().Name}] - Requesting location access...");
            }

            // check if location is accessible
            if (!Input.location.isEnabledByUser)
            {
                Debug.LogWarning($"[{GetType().Name}] - Location is not accessible from device.");
                yield break;
            }

            // start retrieving location
            Input.location.Start();

            // retrieve location status
            while (Input.location.status == LocationServiceStatus.Initializing)
                yield return new WaitForSeconds(1);

            // show the user a warning
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.LogWarning($"[{GetType().Name}] - Failed to initialize Location Services.");
                yield break;
            }

            while (true)
            {
                Latitude = Input.location.lastData.latitude;
                Longitude = Input.location.lastData.longitude;

                // invoke location updates
                EventSystem.Singleton.UpdateCoordinates(Longitude, Latitude);
                yield return new WaitForSeconds(updateInterval);
            }
        }
    }
}
