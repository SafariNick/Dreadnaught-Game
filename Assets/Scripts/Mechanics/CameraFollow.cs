using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    [SerializeField] private Transform target;

    void Update()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        transform.position = pos;
    }
//            if (target == null)
//        {
//            Debug.LogWarning("Target not set for CameraFollow.");
//            return;
//        }
//Vector3 newPosition = transform.position;
//newPosition.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
//newPosition.y = target.position.y; // Follow the target's Y position
//transform.position = newPosition;
}