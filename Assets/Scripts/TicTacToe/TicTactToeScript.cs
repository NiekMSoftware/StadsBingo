using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnproductiveProductions.StadsBingo.TicTacToe
{
    public class TicTactToeScript : MonoBehaviour
    {
        Boolean checker;
        int plusone;

        int roundCount = 0;
        int totalRounds = 3;
        bool gameOver = false;

        public TextMeshProUGUI BtnText1 = null;
        public TextMeshProUGUI BtnText2 = null;
        public TextMeshProUGUI BtnText3 = null;
        public TextMeshProUGUI BtnText4 = null;
        public TextMeshProUGUI BtnText5 = null;
        public TextMeshProUGUI BtnText6 = null;
        public TextMeshProUGUI BtnText7 = null;
        public TextMeshProUGUI BtnText8 = null;
        public TextMeshProUGUI BtnText9 = null;

        public TextMeshProUGUI ScoreTeller = null;
        public TextMeshProUGUI ScorePlayerX;
        public TextMeshProUGUI ScorePlayerO;

        public void Score()
        {
            bool win = false;

            void HandleWin(TextMeshProUGUI b1, TextMeshProUGUI b2, TextMeshProUGUI b3, Color color, string player)
            {
                b1.color = color;
                b2.color = color;
                b3.color = color;
                ScoreTeller.text = $"The winner is Player {player}";

                if (player == "X")
                {
                    plusone = int.Parse(ScorePlayerX.text);
                    ScorePlayerX.text = Convert.ToString(plusone + 1);
                }
                else
                {
                    plusone = int.Parse(ScorePlayerO.text);
                    ScorePlayerO.text = Convert.ToString(plusone + 1);
                }

                roundCount++;
                win = true;

                if (roundCount >= totalRounds)
                {
                    DeclareFinalWinner();
                    gameOver = true;
                }
                else
                {
                    Invoke("ResetBoard", 1.0f);
                }
            }

            //Player X
            if (BtnText1.text == "X" && BtnText2.text == "X" && BtnText3.text == "X") HandleWin(BtnText1, BtnText2, BtnText3, Color.red, "X");
            else if (BtnText1.text == "X" && BtnText4.text == "X" && BtnText7.text == "X") HandleWin(BtnText1, BtnText4, BtnText7, Color.blue, "X");
            else if (BtnText1.text == "X" && BtnText5.text == "X" && BtnText9.text == "X") HandleWin(BtnText1, BtnText5, BtnText9, Color.green, "X");
            else if (BtnText3.text == "X" && BtnText5.text == "X" && BtnText7.text == "X") HandleWin(BtnText3, BtnText5, BtnText7, Color.gray, "X");
            else if (BtnText2.text == "X" && BtnText5.text == "X" && BtnText8.text == "X") HandleWin(BtnText2, BtnText5, BtnText8, Color.yellow, "X");
            else if (BtnText3.text == "X" && BtnText6.text == "X" && BtnText9.text == "X") HandleWin(BtnText3, BtnText6, BtnText9, Color.blue, "X");
            else if (BtnText4.text == "X" && BtnText5.text == "X" && BtnText6.text == "X") HandleWin(BtnText4, BtnText5, BtnText6, Color.cyan, "X");
            else if (BtnText7.text == "X" && BtnText8.text == "X" && BtnText9.text == "X") HandleWin(BtnText7, BtnText8, BtnText9, Color.green, "X");

            //Player O
            else if (BtnText1.text == "O" && BtnText2.text == "O" && BtnText3.text == "O") HandleWin(BtnText1, BtnText2, BtnText3, Color.red, "O");
            else if (BtnText1.text == "O" && BtnText4.text == "O" && BtnText7.text == "O") HandleWin(BtnText1, BtnText4, BtnText7, Color.blue, "O");
            else if (BtnText1.text == "O" && BtnText5.text == "O" && BtnText9.text == "O") HandleWin(BtnText1, BtnText5, BtnText9, Color.green, "O");
            else if (BtnText3.text == "O" && BtnText5.text == "O" && BtnText7.text == "O") HandleWin(BtnText3, BtnText5, BtnText7, Color.gray, "O");
            else if (BtnText2.text == "O" && BtnText5.text == "O" && BtnText8.text == "O") HandleWin(BtnText2, BtnText5, BtnText8, Color.yellow, "O");
            else if (BtnText3.text == "O" && BtnText6.text == "O" && BtnText9.text == "O") HandleWin(BtnText3, BtnText6, BtnText9, Color.blue, "O");
            else if (BtnText4.text == "O" && BtnText5.text == "O" && BtnText6.text == "O") HandleWin(BtnText4, BtnText5, BtnText6, Color.cyan, "O");
            else if (BtnText7.text == "O" && BtnText8.text == "O" && BtnText9.text == "O") HandleWin(BtnText7, BtnText8, BtnText9, Color.green, "O");
        }

        private void DeclareFinalWinner()
        {
            int xScore = int.Parse(ScorePlayerX.text);
            int oScore = int.Parse(ScorePlayerO.text);

            if (xScore > oScore)
                ScoreTeller.text = "Game Over! Player X wins overall!";
            else if (oScore > xScore)
                ScoreTeller.text = "Game Over! Player O wins overall!";
            else
                ScoreTeller.text = "Game Over! It's a Draw!";
            Application.Quit();
        }

        private void PlayTurn(TextMeshProUGUI button)
        {
            if (gameOver || !string.IsNullOrEmpty(button.text)) return;

            button.text = checker ? "O" : "X";
            checker = !checker;
            Score();
        }

        public void BtnText1_Click() => PlayTurn(BtnText1);
        public void BtnText2_Click() => PlayTurn(BtnText2);
        public void BtnText3_Click() => PlayTurn(BtnText3);
        public void BtnText4_Click() => PlayTurn(BtnText4);
        public void BtnText5_Click() => PlayTurn(BtnText5);
        public void BtnText6_Click() => PlayTurn(BtnText6);
        public void BtnText7_Click() => PlayTurn(BtnText7);
        public void BtnText8_Click() => PlayTurn(BtnText8);
        public void BtnText9_Click() => PlayTurn(BtnText9);

        private void ResetBoard()
        {
            BtnText1.text = BtnText2.text = BtnText3.text = "";
            BtnText4.text = BtnText5.text = BtnText6.text = "";
            BtnText7.text = BtnText8.text = BtnText9.text = "";

            BtnText1.color = BtnText2.color = BtnText3.color = Color.black;
            BtnText4.color = BtnText5.color = BtnText6.color = Color.black;
            BtnText7.color = BtnText8.color = BtnText9.color = Color.black;
        }

        public void ResetGame()
        {
            ResetBoard();
            ScoreTeller.text = "Game Reset";
            checker = false;
        }
    }
}