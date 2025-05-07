using TMPro;
using UnityEngine;

namespace UnproductiveProductions.StadsBingo.Global
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton

        public static UIManager Singleton { get; private set; }

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

        [Header("Coordinates")]
        [SerializeField] private TMP_Text coordinates;

        [Header("Notify Usr")]
        [SerializeField] private GameObject arrivedNotification;

        public static bool NotifiedUser { get; private set; } = false;

        private void OnEnable()
        {
            EventSystem.Singleton.OnLocationUpdated += UpdateCoordinateTexts;
            EventSystem.Singleton.OnLocationArrived += NotifyUser;
        }

        private void OnDisable()
        {
            EventSystem.Singleton.OnLocationUpdated -= UpdateCoordinateTexts;
            EventSystem.Singleton.OnLocationArrived -= NotifyUser;
        }

        public void UpdateCoordinateTexts(double longtitude, double latitude)
        {
            if (coordinates)
                coordinates.text = $"{longtitude}, {latitude}";
        }

        public void NotifyUser()
        {
            if (!arrivedNotification.activeSelf && !NotifiedUser)
            { 
                arrivedNotification.SetActive(true);
                NotifiedUser = true;
            }
        }

        public void StopNotifying()
        {
            arrivedNotification.SetActive(false);
            NotifiedUser = false;
        }
    }
}
