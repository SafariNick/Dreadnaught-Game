using UnityEngine;

//public class Life : Pickup
//{
//    [SerializeField] private float xVel = -4f;
//    [SerializeField] private float yVel = 4f;

//    Rigidbody rb;
//    public override void OnPickup(GameObject player) => player.GetComponent<PlayerController>().lives++;
//    void OnStart()
//    {
//        rb = GetComponent<Rigidbody>();
//        rb.linearVelocity = new Vector2(-4, 4);

//    }
//    void Update()
//    {
//        rb.linearVelocity = new Vector2(xVel, rb.linearVelocity.y);
//    }

//    public override void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            OnPickup(collision.gameObject);
//        }

//    }
//}
