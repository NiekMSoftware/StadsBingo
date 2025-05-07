using UnityEngine;
using TMPro;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class Player : MonoBehaviour
    {
        public float YForce;
        public bool Scored;
        public bool CanJump;
        public GameObject ScoreUI;
        public GameObject ResetUI;

        public void Start()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        public void Update()
        {
            if(Input.GetMouseButtonDown(0) && CanJump)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                Jump();
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if(collision.collider != null)
            {
                CanJump = false;
            }
            ScoreUI.SetActive(false);
            ResetUI.SetActive(true);
            //stop obstakels
        }

        public void Jump()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * YForce);
        }
    }
}
