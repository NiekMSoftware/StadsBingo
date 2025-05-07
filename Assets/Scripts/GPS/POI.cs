using UnityEngine;

namespace UnproductiveProductions.StadsBingo.GPS
{
    [CreateAssetMenu(fileName = "POI", menuName = "Scriptable Objects/POI")]
    public class POI : ScriptableObject
    {
        public string Title;
        public string Description;

        [Space]
        public double Latitude;
        public double Longitude;
    }
}
