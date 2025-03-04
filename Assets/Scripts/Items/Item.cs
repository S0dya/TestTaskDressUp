using DG.Tweening;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemInfo itemInfo;
        [Space]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D boxCollider;

        private Tween _scaleTween;

        private Vector2 _initialScale;
        private Vector2 _targetScale;

        public bool CanPickUp { get; private set; } = true;

        private void Awake()
        {
            if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
            if (!boxCollider) boxCollider = GetComponent<BoxCollider2D>();

            _initialScale = transform.localScale;
            _targetScale = _initialScale + itemInfo.ScaleOffset;
        }

        private void Start()
        {
            Init(itemInfo);
        }

        public void Init(ItemInfo itemInfo)
        {
            spriteRenderer.sprite = itemInfo.Sprite;

            var spriteBounds = spriteRenderer.sprite.bounds;
            boxCollider.size = spriteBounds.size;
            boxCollider.offset = spriteBounds.center;
        }

        public void OnPickedUp()
        {
            CanPickUp = false;

            _scaleTween?.Kill();

            _scaleTween = transform.DOScale(_targetScale, itemInfo.ScaleInDuration);
        }
        public void OnDropped()
        {
            _scaleTween?.Kill();

            _scaleTween = transform.DOScale(_initialScale, itemInfo.ScaleOutDuration);
        }

        public void FinishedDrop()
        {
            CanPickUp = true;
        }

        public void SetSortingOrder(int order)
        {
            spriteRenderer.sortingOrder = order;
        }
        public int GetSortingOrder() => spriteRenderer.sortingOrder;

        public float GetOnSurfaceOffset()
        {
            float spriteHeight = spriteRenderer.bounds.size.y;

            return Mathf.Lerp(-spriteHeight / 2, spriteHeight / 2, itemInfo.OnSurfaceYPivot);
        }
    }
}
