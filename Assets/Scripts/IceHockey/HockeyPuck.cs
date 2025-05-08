using UnityEngine;

namespace UnproductiveProductions.StadsBingo.IceHockey
{
    public class HockeyPuck : MonoBehaviour
    {
        public Vector2 MinBounds = new Vector2(-8f, -4f);   
        public Vector2 MaxBounds = new Vector2(8f, 4f);

        public LayerMask LeftGoalLayer;
        public LayerMask RightGoalLayer;

        //private void Start()
        //{
        //    Rigidbody rb = GetComponent<Rigidbody>();

        //    int direction = Random.value < 0.5f ? -1 : 1;

        //    Vector3 force = new Vector3(direction * 1f, 0f, 0f) * 5f; // Change 5f for more/less force

        //    rb.AddForce(force, ForceMode.Impulse);
        //}

        private void Update()
        {
            Vector3 pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, MinBounds.x, MaxBounds.x);
            pos.z = Mathf.Clamp(pos.z, MinBounds.y, MaxBounds.y);

            transform.position = pos;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & LeftGoalLayer) != 0)
            {
                IceHockeyGameManager.Instance.AddPointRight();
            }

            if (((1 << other.gameObject.layer) & RightGoalLayer) != 0)
            {
                IceHockeyGameManager.Instance.AddPointLeft();
            }
        }
    }
}
