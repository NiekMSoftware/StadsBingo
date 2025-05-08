using UnityEngine;

namespace UnproductiveProductions.StadsBingo.IceHockey
{
    [RequireComponent(typeof(Rigidbody))]
    public class PuckCollision : MonoBehaviour
    {
        public float forceMultiplier = 1.5f;

        void OnCollisionEnter(Collision collision)
        {
            HockeyStick stick = collision.gameObject.GetComponent<HockeyStick>();
            if (stick != null)
            {
                Vector3 hitVelocity = stick.Velocity;
                if (hitVelocity.magnitude > 0.1f)
                {
                    Rigidbody rb = GetComponent<Rigidbody>();
                    Vector3 direction = (transform.position - stick.transform.position).normalized;
                    Vector3 force = direction * hitVelocity.magnitude * forceMultiplier;

                    rb.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }
}
