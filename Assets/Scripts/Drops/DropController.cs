using DG.Tweening;
using Items;
using UnityEngine;

namespace Drops
{
    public class DropController : MonoBehaviour
    {
        [SerializeField] private float itemDropDuration = 0.3f;
        [SerializeField] private float itemShakeDuration = 0.2f;
        [SerializeField] private float itemShakeOffset = 0.1f;
        [SerializeField] private float itemDistanceToSurface = 0.1f;
        [Space]
        [SerializeField] private LayerMask surfaceLayer;

        public void DropItem(Item item)
        {
            item.OnDropped();

            var hit = Physics2D.Raycast(item.transform.position, Vector2.down, Mathf.Infinity, surfaceLayer);

            if (hit.collider)
            {
                var hitPosition = hit.point;
                var targetPosition = new Vector2(hitPosition.x, hitPosition.y - item.GetOnSurfaceOffset());

                bool shakes = Vector2.Distance(item.transform.position, hitPosition) > itemDistanceToSurface;
                
                item.transform.DOMove(targetPosition, itemDropDuration).OnComplete(() =>
                {
                    item.FinishedDrop();
                    
                    if (shakes) 
                        item.transform.DOShakePosition(itemShakeDuration, new Vector3(0, itemShakeOffset, 0));
                });

                item.SetSortingOrder(Mathf.RoundToInt(-targetPosition.y * 10));
            }
        }
    }
}
