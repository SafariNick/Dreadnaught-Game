using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public Vector3 startPosition;
    void Start()
    {
        GameManager.Instance.StartLevel(startPosition);
    }
}