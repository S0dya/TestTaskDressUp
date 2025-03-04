using UnityEngine;

namespace Movers
{
    public class CameraMover : IMover
    {
        private Transform _currentTransform;
        private Vector2 _lastScreenPosition;
        private Camera _camera;

        private float _min;
        private float _max;

        private float _speedMultiplier = 0.15f;

        public CameraMover(Camera camera, float min, float max)
        {
            _camera = camera;

            _min = min;
            _max = max;
        }

        public void StartMoving(Transform transform, Vector2 screenPosition)
        {
            _currentTransform = transform;
            _lastScreenPosition = screenPosition;
        }

        // move camera based on input delta and camera reversed movement
        public void Move(Vector2 screenPosition)
        {
            if (!_currentTransform) return;

            var deltaScreen = screenPosition - _lastScreenPosition;
            var deltaWorld = deltaScreen.x * _camera.orthographicSize * _speedMultiplier;

            var newX = _currentTransform.position.x - deltaWorld;
            newX = Mathf.Clamp(newX, _min, _max);

            _currentTransform.position = new Vector3(newX, _currentTransform.position.y, _currentTransform.position.z);
            _lastScreenPosition = screenPosition;
        }

        public void StopMoving(Vector2 position)
        {
            _currentTransform = null;
        }
    }
}