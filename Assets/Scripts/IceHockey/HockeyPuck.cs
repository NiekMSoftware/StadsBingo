using UnityEngine;

namespace UnproductiveProductions.StadsBingo.IceHockey
{
    public class HockeyPuck : MonoBehaviour
    {
        public Vector2 MinBounds = new Vector2(-8f, -4f);   
        public Vector2 MaxBounds = new Vector2(8f, 4f);

        public LayerMask LeftGoalLayer;
        public LayerMask RightGoalLayer;

        private void Update()
        {
            Vector3 pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, MinBounds.x, MaxBounds.x);
            pos.z = Mathf.Clamp(pos.z, MinBounds.y, MaxBounds.y);

            transform.position = pos;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & LeftGoalLayer) != 0)
            {
                if (IceHockeyGameManager.Instance.RightScore >= 2)
                {
                    //end
                    print("Right won");
                }
                IceHockeyGameManager.Instance.AddPointRight();
            }

            if (((1 << other.gameObject.layer) & RightGoalLayer) != 0)
            {
                if (IceHockeyGameManager.Instance.LeftScore >= 2)
                {
                    //end
                    print("Left won");
                }
                IceHockeyGameManager.Instance.AddPointLeft();
            }
        }
    }
}
