using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    [SerializeField] private Transform target;

    void Start()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        transform.position = pos;
    }
}