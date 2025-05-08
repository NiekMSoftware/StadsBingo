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

        // The obstacle sets the player and makes sure it won't change the Y-axis when moving.
        public void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerScript = Player.GetComponent<Player>();
            FixedY = transform.position.y;
        }

        // Update will move the obstacle if the game is playing and the player hasn't died yet.
        // The timer will begin and DestroyObstacle will be called.
        public void Update()
        {
            if (PlayerScript.IsAlive && PlayerScript.GameActive)
            {
                MoveObstacle();
                currentTime += Time.deltaTime;
                DestroyObstacle();
            }
        }

        // Moves the obstacle towards the player without changeing the Y-axis.
        private void MoveObstacle()
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, FixedY, transform.position.z);
        }

        // Destroys the obstacle after a certain time.
        private void DestroyObstacle()
        {
            if(currentTime >= timeToLive)
            {
                Destroy(gameObject);
            }
        }
    }
}
