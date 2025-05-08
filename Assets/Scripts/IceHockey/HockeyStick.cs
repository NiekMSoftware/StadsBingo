using UnityEngine;

namespace UnproductiveProductions.StadsBingo.IceHockey
{
    public class HockeyStick : MonoBehaviour
    {
        public Camera MainCamera;
        public Vector2 MinBounds = new Vector2(-5f, -5f);
        public Vector2 MaxBounds = new Vector2(5f, 5f);
        public bool RotateTowardTouch = true;

        [Tooltip("Side of the screen this stick responds to (Left = true, Right = false)")]
        public bool IsLeftStick = true;

        private int fingerId = -1;

        void Start()
        {
            if (MainCamera == null)
                MainCamera = Camera.main;
        }

        void Update()
        {
            HandleTouch();

#if UNITY_EDITOR
            HandleMouse();
#endif
        }

        void HandleTouch()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                float screenMid = Screen.width / 2f;
                bool isInMySide = (IsLeftStick && t.position.x < screenMid) ||
                                  (!IsLeftStick && t.position.x > screenMid);

                if (fingerId == -1 && t.phase == TouchPhase.Began && isInMySide)
                {
                    fingerId = t.fingerId;
                }

                if (t.fingerId == fingerId && (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary))
                {
                    MoveStickToScreenPosition(t.position);
                }

                if (t.fingerId == fingerId && (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled))
                {
                    fingerId = -1;
                }
            }
        }


        void HandleMouse()
        {
            if (!Application.isMobilePlatform && Input.GetMouseButton(0))
            {
                float screenMid = Screen.width / 2f;
                bool isInMySide = (IsLeftStick && Input.mousePosition.x < screenMid) ||
                                  (!IsLeftStick && Input.mousePosition.x > screenMid);

                if (isInMySide)
                {
                    MoveStickToScreenPosition(Input.mousePosition);
                }
            }
        }


        void MoveStickToScreenPosition(Vector2 screenPos)
        {
            Vector3 inputPosition = screenPos;
            inputPosition.z = Mathf.Abs(MainCamera.transform.position.y);

            Vector3 worldPos = MainCamera.ScreenToWorldPoint(inputPosition);
            Vector3 targetPos = new Vector3(
                Mathf.Clamp(worldPos.x, MinBounds.x, MaxBounds.x),
                transform.position.y,
                Mathf.Clamp(worldPos.z, MinBounds.y, MaxBounds.y)
            );

            transform.position = targetPos;

            if (RotateTowardTouch)
            {
                Vector3 dir = targetPos - transform.position;
                dir.y = 0;
                if (dir.sqrMagnitude > 0.001f)
                {
                    float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, -angle, 0);
                }
            }
        }
    }
}
