using UnityEngine;

namespace UnproductiveProductions.StadsBingo.Pong
{
    public class PongBall : MonoBehaviour
    {
        public float initialSpeed = 8f;
        public float speedIncrement = 0.5f;
        public float maxSpeed = 20f;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            LaunchBall();
        }

        void LaunchBall()
        {
            transform.position = Vector3.zero;
            rb.linearVelocity = Vector3.zero;

            float xDir = Random.Range(0, 2) == 0 ? -1 : 1;
            float yDir = Random.Range(-0.5f, 0.5f);
            Vector3 launchDirection = new Vector3(xDir, yDir, 0).normalized;

            rb.linearVelocity = launchDirection * initialSpeed;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Paddle"))
            {
                float yOffset = transform.position.y - collision.transform.position.y;
                Vector3 bounceDirection = new Vector3(
                    rb.linearVelocity.x > 0 ? 1 : -1,
                    yOffset,
                    0f
                ).normalized;

                float newSpeed = Mathf.Min(rb.linearVelocity.magnitude + speedIncrement, maxSpeed);
                rb.linearVelocity = bounceDirection * newSpeed;
            }

            if (collision.gameObject.CompareTag("Wall"))
            {

            }

            if (collision.gameObject.CompareTag("Goal"))
            {
                LaunchBall();
            }
        }
    }
}