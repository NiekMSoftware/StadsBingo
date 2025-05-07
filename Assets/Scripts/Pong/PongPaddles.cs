using UnityEngine;
using UnityEngine.InputSystem;

namespace UnproductiveProductions.StadsBingo.Pong
{
    public class PongPaddles : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public float boundary = 4.5f;
        public bool isLeftPlayer = true;

        private Camera mainCamera;
        private Vector2 touchStartPos;
        private Vector3 paddleStartPos;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.isPressed == false)
                return;

            var touch = Touchscreen.current.primaryTouch;

            if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                touchStartPos = touch.position.ReadValue();
                paddleStartPos = transform.position;
            }
            else if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved ||
                     touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Stationary)
            {
                Vector2 currentTouchPos = touch.position.ReadValue();
                if ((isLeftPlayer && currentTouchPos.x < Screen.width / 2) ||
                    (!isLeftPlayer && currentTouchPos.x >= Screen.width / 2))
                {
                    float deltaY = (currentTouchPos.y - touchStartPos.y) / Screen.height;
                    Vector3 newPos = paddleStartPos + Vector3.up * deltaY * moveSpeed;

                    newPos.y = Mathf.Clamp(newPos.y, -boundary, boundary);
                    transform.position = newPos;
                }
            }
        }
    }
}