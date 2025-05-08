using UnityEngine;
using TMPro;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class Score : MonoBehaviour
    {
        private int _currentScore = 0;
        [SerializeField] private int neededScore;
        private bool _finished = false;
        [SerializeField] private TMP_Text scoreText;

        // Sets the CurrentScore to the ScoreText in the UI.
        private void Update()
        {
            scoreText.SetText(_currentScore.ToString());
        }

        // Updates the score if it hasn't reached the NeededScore, otherwise it sets Finished to true.
        public void UpdateScore()
        {
            if(_currentScore < neededScore)
            {
                _currentScore++;
            }
            else if(_currentScore == neededScore) 
            {
                _finished = true;
                Debug.Log($"finished = {_finished}");
            }
        }
    }
}
