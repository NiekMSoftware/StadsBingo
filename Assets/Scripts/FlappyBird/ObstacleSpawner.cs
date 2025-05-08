using UnityEngine;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private float fixedX;
        private float _fixedZ = 0f;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;

        private float _currentTime;
        [SerializeField] private float cooldown;
        private bool _canSpawn = true;
        [SerializeField] private GameObject obstacle;
        [SerializeField] private Player player;

        // Checks if the obstacles can spawn and if the player is alive playing the game.
        // Activates the timer and spawns an obstacle when it's done, then resets the timer again.
        private void Update()
        {
            if (_canSpawn && player.IsAlive && player.GameActive)
            {
                _currentTime += Time.deltaTime;
                if(_currentTime >= cooldown)
                {
                    SpawnObstacle();
                    ResetTimer();
                }
            }
        }

        // Picks a random value to spawn the obstacle and instantiates the obstacle there.
        private void SpawnObstacle()
        {
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(fixedX, randomY, _fixedZ);
            Instantiate(obstacle, spawnPosition, Quaternion.identity);
        }

        private void ResetTimer()
        {
            _currentTime = 0;
        }
    }
}
