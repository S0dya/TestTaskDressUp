using Drops;
using Items;
using Movers;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Surface
{
    public class SurfaceController : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private SpriteRenderer sceneBackground;

        [Inject] private DropController _dropController;

        private ItemMover _itemMover = new();
        private CameraMover _cameraMover;
        private IMover _currentIMover;

        private Action _dropAction;

        private void Awake()
        {
            InitCamera();
        }
        private void InitCamera()
        {
            if (!cam) cam = Camera.main;

            float halfBgWidth = sceneBackground.bounds.extents.x;
            float minX = sceneBackground.transform.position.x - halfBgWidth + cam.orthographicSize * cam.aspect;
            float maxX = sceneBackground.transform.position.x + halfBgWidth - cam.orthographicSize * cam.aspect;

            _cameraMover = new(cam, minX, maxX);
        }

        public void HandlePointerDown(Vector2 position)
        {
            SetMover(position, out Transform targetTransform);

            _currentIMover.StartMoving(targetTransform, position);
        }

        public void HandlePointerDrag(Vector2 position)
        {
            _currentIMover.Move(position);
        }
        
        public void HandlePointerUp(Vector2 position)
        {
            _currentIMover.StopMoving(position);

            _dropAction?.Invoke();
            
            _currentIMover = null;
            _dropAction = null;
        }

        private void SetMover(Vector2 position, out Transform targetTransform)
        {
            var hits = Physics2D.OverlapPointAll(position);

            var itemHit = hits
                .Select(x => x.GetComponent<Item>()) 
                .Where(item => item != null && item.CanPickUp)
                .OrderByDescending(item => item.GetSortingOrder()).FirstOrDefault();

            if (itemHit)
            {
                _currentIMover = _itemMover;
                targetTransform = itemHit.transform;

                itemHit.OnPickedUp();
                _dropAction = () => { _dropController.DropItem(itemHit); };
            }
            else
            {
                _currentIMover = _cameraMover;
                targetTransform = cam.transform;
            }
        }
    }
}
