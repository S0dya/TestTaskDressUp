using UnityEngine;

public interface IMover
{
    public void StartMoving(Transform transform, Vector2 position);
    public void Move(Vector2 position);
    public void StopMoving(Vector2 position);
}
