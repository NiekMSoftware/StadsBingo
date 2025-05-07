using UnityEngine;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float timeToLive;
        [SerializeField] private float currentTime;
        public GameObject Player;
        public Player PlayerScript;
        public float Speed;
        private float FixedY;
        public bool HasBeenDodged;

        public void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerScript = Player.GetComponent<Player>();
            FixedY = transform.position.y;
        }

        public void Update()
        {
            if (PlayerScript.IsAlive && PlayerScript.GameActive)
            {
                MoveObstacle();
                currentTime += Time.deltaTime;
                DestroyObstacle();
            }
        }

        public void StopMovement()
        {
            if(PlayerScript.IsAlive)
            {
                Speed = 0f;
            }
        }

        private void MoveObstacle()
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, FixedY, transform.position.z);
        }

        private void DestroyObstacle()
        {
            if(currentTime >= timeToLive)
            {
                Destroy(gameObject);
            }
        }
    }
}
