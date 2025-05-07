using UnityEngine;
using UnityEngine.SceneManagement;
using UnproductiveProductions.StadsBingo.Pong;

namespace UnproductiveProductions.StadsBingo.Pong
{
    public class GoalZone : MonoBehaviour
    {
        public bool isLeftGoal = true; 

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                PongScore.Instance.AddScore(!isLeftGoal);

                other.GetComponent<PongBall>().SendMessage("LaunchBall");
            }
        }
    }
}