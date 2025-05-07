using UnityEngine;
using TMPro;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class Player : MonoBehaviour
    {
        public float YForce;
        public bool Scored;
        public bool IsAlive;
        public bool GameActive;
        public GameObject ScoreUI;
        public GameObject ResetUI;

        public void Start()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        public void Update()
        {
            if(Input.GetMouseButtonDown(0) && IsAlive)
            {
                GameActive = true;
                GetComponent<Rigidbody>().isKinematic = false;
                Jump();
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if(collision.collider != null)
            {
                IsAlive = false;
            }
            ScoreUI.SetActive(false);
            ResetUI.SetActive(true);
            GameActive = false;
            //stop obstakels
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "ScoreTrigger")
            {
                Scored = true;
            }
        }

        public void Jump()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * YForce);
        }
    }
}
