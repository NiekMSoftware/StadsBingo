using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnproductiveProductions.StadsBingo.TicTacToe
{
    public class TicTactToeScript : MonoBehaviour
    {
        public bool IsPlayerOTurn = false;
        public int RoundCount = 0;
        public int TotalRounds = 3;
        public bool GameOver = false;

        public TextMeshProUGUI[] BoardButtons = new TextMeshProUGUI[9];
        public TextMeshProUGUI ScoreTeller;
        public TextMeshProUGUI ScorePlayerX;
        public TextMeshProUGUI ScorePlayerO;

        private readonly int[][] winCombinations = new int[][]
        {
            new[] {0, 1, 2},
            new[] {3, 4, 5},
            new[] {6, 7, 8},
            new[] {0, 3, 6},
            new[] {1, 4, 7},
            new[] {2, 5, 8},
            new[] {0, 4, 8},
            new[] {2, 4, 6}
        };

        private readonly Color[] winColors = new[]
        {
            Color.red, Color.blue, Color.green, Color.gray,
            Color.yellow, Color.cyan, Color.magenta, Color.white
        };

        public void PlayTurn(int index)
        {
            if (GameOver || !string.IsNullOrEmpty(BoardButtons[index].text)) return;

            BoardButtons[index].text = IsPlayerOTurn ? "O" : "X";
            IsPlayerOTurn = !IsPlayerOTurn;
            CheckScore();
        }

        private void CheckScore()
        {
            for (int i = 0; i < winCombinations.Length; i++)
            {
                var combo = winCombinations[i];
                string a = BoardButtons[combo[0]].text;
                string b = BoardButtons[combo[1]].text;
                string c = BoardButtons[combo[2]].text;

                if (!string.IsNullOrEmpty(a) && a == b && b == c)
                {
                    HandleWin(combo, a, winColors[i]);
                    return;
                }
            }
        }
        private void ClearScore()
        {
            ScoreTeller.text = "";
        }

        private void HandleWin(int[] combo, string player, Color winColor)
        {
            foreach (int index in combo)
                BoardButtons[index].color = winColor;

            ScoreTeller.text = $"The winner is Player {player}";
            Invoke(nameof(ClearScore), 2f);

            UpdateScore(player);

            RoundCount++;
            if (RoundCount >= TotalRounds)
            {
                DeclareFinalWinner();
                GameOver = true;
            }
            else
            {
                Invoke(nameof(ResetBoard), 1.0f);
            }
        }

        private void UpdateScore(string player)
        {
            var scoreText = player == "X" ? ScorePlayerX : ScorePlayerO;
            int score = int.Parse(scoreText.text);
            scoreText.text = (score + 1).ToString();
        }

        private void DeclareFinalWinner()
        {
            int xScore = int.Parse(ScorePlayerX.text);
            int oScore = int.Parse(ScorePlayerO.text);

            if (xScore > oScore)
                ScoreTeller.text = "Player X is the winner!";
            else if (oScore > xScore)
                ScoreTeller.text = "Player O is the winner!";
            else
                ScoreTeller.text = "The game is a draw!";

            Invoke(nameof(ClearScore), 2f);

            Application.Quit();
        }

        private void ResetBoard()
        {
            foreach (var button in BoardButtons)
            {
                button.text = "";
                button.color = Color.black;
            }
            GameOver = false;
        }

        public void ResetGame()
        {
            ResetBoard();
            ScoreTeller.text = "Game reset";
            RoundCount = 0;
            IsPlayerOTurn = false;
            GameOver = false;

            ScorePlayerX.text = "0";
            ScorePlayerO.text = "0";
            Invoke(nameof(ClearScore), 2f);
        }
    }
}