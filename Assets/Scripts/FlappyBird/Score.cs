using UnityEngine;
using TMPro;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class Score : MonoBehaviour
    {
        public int CurrentScore;
        public int NeededScore;
        public bool Finished;
        public TMP_Text ScoreText;

        public void Update()
        {
            ScoreText.SetText(CurrentScore.ToString());
        }

        public void UpdateScore()
        {
            if(CurrentScore < NeededScore)
            {
                CurrentScore++;
            }
            else
            {
                Finished = true;
            }
        }
    }
}
