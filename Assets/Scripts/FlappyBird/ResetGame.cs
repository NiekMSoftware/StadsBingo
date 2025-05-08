using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class ResetGame : MonoBehaviour
    {
        [SerializeField] private Button resetButton;
        private string _sceneName;

        // Gets the name of the current scene and makes the ResetButton reset the mini-game when clicked.
        private void Start()
        {
            _sceneName = SceneManager.GetActiveScene().name;
            resetButton.onClick.AddListener(ResetMiniGame);
        }

        private void ResetMiniGame()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
