using UnityEngine;

[CreateAssetMenu(menuName = "Infos/ItemInfo", fileName = "Item Info")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [Space]
    [SerializeField] private Vector2 scaleOffset = new(0.1f, 0.1f);
    [SerializeField] private float scaleInDuration = 0.1f;
    [SerializeField] private float scaleOutDuration = 0.1f;
    [Space]
    [SerializeField] private float onSurfaceYPivot = 0.5f;

    public Sprite Sprite => sprite;
    public Vector2 ScaleOffset => scaleOffset;
    public float ScaleInDuration => scaleInDuration;
    public float ScaleOutDuration => scaleOutDuration;
    public float OnSurfaceYPivot => onSurfaceYPivot;
}
