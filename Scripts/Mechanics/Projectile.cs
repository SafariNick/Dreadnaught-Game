using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float lifetime = 1.0f;

    private void Start() => Destroy(gameObject, lifetime);
    public void SetVelocity(Vector2 velocity) => GetComponent<Rigidbody2D>().linearVelocity = velocity;

    // change bullet speed 
    //[SerializeField] private float bulletSpeed = 10f; // Speed of the bullets
    //[SerializeField] private float missleSpeed = 15f; // Speed of the missiles
    //[SerializeField] private float bigBulletSpeed = 12f; // Speed of the big bullets
    //[SerializeField] private float smallBulletSpeed = 8f; // Speed of the small bullets
    //[SerializeField] private float bulletLifetime = 2f; // Lifetime of the bullets in seconds


}
