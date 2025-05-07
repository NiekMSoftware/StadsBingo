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
        [SerializeField] private TMP_Text longtitudeText;
        [SerializeField] private TMP_Text latitudeText;

        private void OnEnable()
        {
            EventSystem.Singleton.OnLocationUpdated += UpdateCoordinateTexts;
        }

        private void OnDisable()
        {
            EventSystem.Singleton.OnLocationUpdated -= UpdateCoordinateTexts;
        }

        public void UpdateCoordinateTexts(double longtitude, double latitude)
        {
            if (longtitudeText)
                longtitudeText.text = $"Longtitude: {longtitude}";
            if (latitudeText)
                latitudeText.text = $"Latitude: {latitude}";
        }
    }
}
