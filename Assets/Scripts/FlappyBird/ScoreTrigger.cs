using UnityEngine;

namespace UnproductiveProductions.StadsBingo.FlappyBird
{
    public class ScoreTrigger : MonoBehaviour
    {
        private bool _hasBeenTriggered;

        // Checks if the space between the obstacles has hit the player trigger and updates the score once.
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player" && !_hasBeenTriggered)
            {
                Player Player = other.GetComponent<Player>();
                if(Player != null && Player.IsAlive)
                {
                    Player.Scored.UpdateScore();
                    _hasBeenTriggered = true;
                }
            }
        }
    }
}
