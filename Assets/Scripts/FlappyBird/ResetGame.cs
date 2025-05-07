using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class ResetGame : MonoBehaviour
    {
        public Button ResetButton;
        private string sceneName;

        public void Start()
        {
            sceneName = SceneManager.GetActiveScene().name;
            ResetButton.onClick.AddListener(ResetMiniGame);
            
        }

        public void ResetMiniGame()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
