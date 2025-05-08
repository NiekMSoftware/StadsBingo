using UnityEngine;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public float FixedX;
        public float FixedZ;
        public float MinY;
        public float MaxY;

        public float CurrentTime;
        public float Cooldown;
        public bool _canSpawn;
        public GameObject Obstacle;
        public Player Player;

        // Checks if the obstacles can spawn and if the player is alive playing the game.
        // Activates the timer and spawns an obstacle when it's done, then resets the timer again.
        public void Update()
        {
            if (_canSpawn && Player.IsAlive && Player.GameActive)
            {
                CurrentTime += Time.deltaTime;
                if(CurrentTime >= Cooldown)
                {
                    SpawnObstacle();
                    ResetTimer();
                }
            }
        }

        // Picks a random value to spawn the obstacle and instantiates the obstacle there.
        private void SpawnObstacle()
        {
            float randomY = Random.Range(MinY, MaxY);
            Vector3 spawnPosition = new Vector3(FixedX, randomY, FixedZ);
            Instantiate(Obstacle, spawnPosition, Quaternion.identity);
        }

        private void ResetTimer()
        {
            CurrentTime = 0;
        }
    }
}
