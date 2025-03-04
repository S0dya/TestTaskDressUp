using UnityEngine;

namespace Movers
{
    public class ItemMover : IMover
    {
        private Transform _currentTransform;

        public void StartMoving(Transform transform, Vector2 position)
        {
            _currentTransform = transform;

            _currentTransform.position = position;
        }

        public void Move(Vector2 position)
        {
            if (!_currentTransform) return;

            _currentTransform.position = position;
        }

        public void StopMoving(Vector2 position)
        {
            if (!_currentTransform) return;

            _currentTransform.position = position;

            _currentTransform = null;
        }
    }
}
