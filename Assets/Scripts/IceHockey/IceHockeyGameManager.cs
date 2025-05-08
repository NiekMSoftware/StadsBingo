using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnproductiveProductions.StadsBingo
{
    public class IceHockeyGameManager : MonoBehaviour
    {
        public static IceHockeyGameManager Instance;

        [Header("Score")]
        public int LeftScore = 0;
        public int RightScore = 0;
        public Text LeftScoreText;
        public Text RightScoreText;

        [Header("Gameplay Objects")]
        [SerializeField] private Transform puck;
        [SerializeField] private Transform puckStartPosition;
        [SerializeField] private Transform leftStick;
        [SerializeField] private Transform rightStick;
        [SerializeField] private Transform leftStartPos;
        [SerializeField] private Transform rightStartPos;

        [Header("Countdowns")]
        public Text CountdownTextLeft;
        public Text CountdownTextRight;
        public float CountdownDurationLeft = 3f;
        public float CountdownDurationRight = 3f;


        private Rigidbody puckRb;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        void Start()
        {
            puckRb = puck.GetComponent<Rigidbody>();
            UpdateScoreUI();
            StartCoroutine(CountdownLeft());
            StartCoroutine(CountdownRight());
        }

        public void AddPointLeft()
        {
            LeftScore++;
            UpdateScoreUI();
            ResetRound();
            StartCoroutine(CountdownLeft());
            StartCoroutine(CountdownRight());
        }

        public void AddPointRight()
        {
            RightScore++;
            UpdateScoreUI();
            ResetRound();
            StartCoroutine(CountdownLeft());
            StartCoroutine(CountdownRight());
        }


        private void UpdateScoreUI()
        {
            if (LeftScoreText != null) LeftScoreText.text = LeftScore.ToString();
            if (RightScoreText != null) RightScoreText.text = RightScore.ToString();
        }

        private IEnumerator CountdownLeft()
        {
            float remaining = CountdownDurationLeft;
            while (remaining > 0)
            {
                if (CountdownTextLeft != null)
                    CountdownTextLeft.text = Mathf.Ceil(remaining).ToString();
                yield return new WaitForSeconds(1f);
                remaining -= 1f;
            }
            if (CountdownTextLeft != null)
            { 
                CountdownTextLeft.text = "";
            }

            if (puckRb != null)
            {
                puckRb.isKinematic = false;
                puckRb.linearVelocity = Vector3.zero;

                int direction = Random.value < 0.5f ? -1 : 1;

                Vector3 force = new Vector3(direction * 1f, 0f, 0f) * 5f; // Change 5f for more/less force

                puckRb.AddForce(force, ForceMode.Impulse);
            }
        }

        private IEnumerator CountdownRight()
        {
            float remaining = CountdownDurationRight;
            while (remaining > 0)
            {
                if (CountdownTextRight != null)
                    CountdownTextRight.text = Mathf.Ceil(remaining).ToString();
                yield return new WaitForSeconds(1f);
                remaining -= 1f;
            }
            if (CountdownTextRight != null)
            {
                CountdownTextRight.text = "";
            }

            if (puckRb != null)
            {
                puckRb.isKinematic = false;
                puckRb.linearVelocity = Vector3.zero;
            }
        }


        public void ResetRound()
        {
            // Reset puck position and stop movement
            if (puck != null && puckStartPosition != null)
            {
                puck.position = puckStartPosition.position;
                puck.rotation = puckStartPosition.rotation;
                puckRb.linearVelocity = Vector3.zero;
                puckRb.angularVelocity = Vector3.zero;
                puckRb.isKinematic = true; // Temporarily freeze during countdown
            }

            // Reset players
            if (leftStick != null && leftStartPos != null)
            {
                leftStick.position = leftStartPos.position;
                leftStick.rotation = leftStartPos.rotation;
            }

            if (rightStick != null && rightStartPos != null)
            {
                rightStick.position = rightStartPos.position;
                rightStick.rotation = rightStartPos.rotation;
            }
        }
    }
}
