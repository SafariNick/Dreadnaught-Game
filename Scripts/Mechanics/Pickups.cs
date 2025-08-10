using UnityEngine;

public class Pickups : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum PickupType
    {
        Life,
        Score,
        Ammo,
        PowerUp
    }
    public PickupType pickupType = PickupType.Life;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           PlayerController pc = collision.GetComponent<PlayerController>();
            pc.SetLives(pc.GetLives() + 1);
            Debug.Log("Picked up!");
            Destroy(gameObject);
        }
    }
}
