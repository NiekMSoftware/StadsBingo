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
