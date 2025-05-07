using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace UnproductiveProductions.StadsBingo.Pong
{
    public class PongScore : MonoBehaviour
    {
        public static PongScore Instance;

        public int player1Score = 0;
        public int player2Score = 0;

        public TextMeshProUGUI player1Text;
        public TextMeshProUGUI player2Text;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
           Destroy(gameObject);
        }

        public void AddScore(bool isPlayer1)
        {
            if (isPlayer1)
                player1Score++;
            else
                player2Score++;

            UpdateScoreUI();
        }

        void UpdateScoreUI()
        {
            player1Text.text = player1Score.ToString();
            player2Text.text = player2Score.ToString();
        }

        public void ResetScores()
        {
            player1Score = 0;
            player2Score = 0;
            UpdateScoreUI();
        }
    }
}