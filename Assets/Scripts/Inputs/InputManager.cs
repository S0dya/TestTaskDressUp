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
            if (Input.GetMouseButtonDown(0))
                _surfaceController.HandlePointerDown(cam.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetMouseButton(0))
                _surfaceController.HandlePointerDrag(cam.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetMouseButtonUp(0))
                _surfaceController.HandlePointerUp(cam.ScreenToWorldPoint(Input.mousePosition));

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = cam.ScreenToWorldPoint(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _surfaceController.HandlePointerDown(touchPosition);
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        _surfaceController.HandlePointerDrag(touchPosition);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _surfaceController.HandlePointerUp(touchPosition);
                        break;
                }
            }
        }
    }
}
