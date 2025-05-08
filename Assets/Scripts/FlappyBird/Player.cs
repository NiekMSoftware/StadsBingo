using UnityEngine;
using TMPro;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float yForce;
        public Score Scored;
        public bool IsAlive;
        public bool GameActive;
        [SerializeField] private GameObject scoreUI;
        [SerializeField] private GameObject resetUI;
        private Rigidbody _rb;

        // Makes sure the Player doesn't fall down when the mini-game loads in.
        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        // When the player presses the screen and hasn't died, the game will start and the player can jump.
        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && IsAlive)
            {
                GameActive = true;
                GetComponent<Rigidbody>().isKinematic = false;
                Jump();
            }
        }

        // When the player hits an obstacle the player can't jump and the ResetUI is activated.
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider != null)
            {
                IsAlive = false;
            }
            scoreUI.SetActive(false);
            resetUI.SetActive(true);
            GameActive = false;
        }

        private void Jump()
        {
            _rb.linearVelocity = Vector3.up * yForce;
        }
    }
}
