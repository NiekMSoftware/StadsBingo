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

        // Sets the CurrentScore to the ScoreText in the UI.
        public void Update()
        {
            ScoreText.SetText(CurrentScore.ToString());
        }

        // Updates the score if it hasn't reached the NeededScore, otherwise it sets Finished to true.
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
