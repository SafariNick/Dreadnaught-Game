using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;


    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnPlayerControllerCreated += (playerController) => target = playerController.transform;
        //GameManager.Instance.OnPlayerControllerDestroyed += PlayerControllerCreated;
    }

    private void PlayerControllerCreated(PlayerController playerController)
    {
        target = playerController.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        transform.position = pos;
        pos.y = Mathf.Clamp(target.position.y, minYPos, maxYPos);
        transform.position = pos;

    }
}