using UnityEngine;
using TMPro;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class Player : MonoBehaviour
    {
        public float YForce;
        public Score Scored;
        public bool IsAlive;
        public bool GameActive;
        public GameObject ScoreUI;
        public GameObject ResetUI;

        // Makes sure the Player doesn't fall down when the mini-game loads in.
        public void Start()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        // When the player presses the screen and hasn't died, the game will start and the player can jump.
        public void Update()
        {
            if(Input.GetMouseButtonDown(0) && IsAlive)
            {
                GameActive = true;
                GetComponent<Rigidbody>().isKinematic = false;
                Jump();
            }
        }

        // When the player hits an obstacle the player can't jump and the ResetUI is activated.
        public void OnCollisionEnter(Collision collision)
        {
            if(collision.collider != null)
            {
                IsAlive = false;
            }
            ScoreUI.SetActive(false);
            ResetUI.SetActive(true);
            GameActive = false;
        }

        public void Jump()
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.up * YForce;
        }
    }
}
