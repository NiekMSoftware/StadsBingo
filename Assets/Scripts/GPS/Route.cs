using UnityEngine;

namespace UnproductiveProductions.StadsBingo.GPS
{
    [CreateAssetMenu(fileName = "Route", menuName = "Scriptable Objects/Route")]
    public class Route : ScriptableObject
    {
        public string RouteName;
        public POI[] points;
    }
}
