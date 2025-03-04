using Surface;
using UnityEngine;
using Zenject;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        [Inject] private SurfaceController _surfaceController;

        private void Awake()
        {
            if (!cam) cam = Camera.main;
        }

        private void Update()
        {
#if UNITY_EDITOR
            HandleMouseInput();
#elif UNITY_ANDROID
            HandleTouchInput();
#endif
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
                _surfaceController.HandlePointerDown(GetWorldPosition(Input.mousePosition));

            if (Input.GetMouseButton(0))
                _surfaceController.HandlePointerDrag(GetWorldPosition(Input.mousePosition));

            if (Input.GetMouseButtonUp(0))
                _surfaceController.HandlePointerUp(GetWorldPosition(Input.mousePosition));
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector2 touchPosition = touch.position;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _surfaceController.HandlePointerDown(GetWorldPosition(touchPosition));
                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        _surfaceController.HandlePointerDrag(GetWorldPosition(touchPosition));
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _surfaceController.HandlePointerUp(GetWorldPosition(touchPosition));
                        break;
                }
            }
        }

        private Vector2 GetWorldPosition(Vector2 screenPosition)
        {
            return cam.ScreenToWorldPoint(new (screenPosition.x, screenPosition.y, cam.nearClipPlane));
        }

    }
}
