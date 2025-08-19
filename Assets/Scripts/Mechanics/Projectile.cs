using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public ProjectileType projectileType;

    [SerializeField, Range(1, 20)] private float lifetime = 1.0f;
    [SerializeField] private float bulletVFXLifetime = 0.5f; // Lifetime of the bullet VFX in seconds
    //private void Start() => Destroy(gameObject, lifetime);
    public void SetVelocity(Vector2 velocity) => GetComponent<Rigidbody2D>().linearVelocity = velocity;
    

   [SerializeField] private GameObject bulletPrefab; // Prefab for the bullet
    private void OnEnable()
    {
        CancelInvoke(nameof(Expire));
        Invoke(nameof(Expire),lifetime);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(Expire));
    }
    private void Expire()
    {
        if (bulletPrefab != null) 
        {
            var bulletVFX = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            if (bulletVFXLifetime > 0f) Destroy(bulletVFX, bulletVFXLifetime);
            
        }
        Destroy(gameObject);
    }

    // change bullet speed 
    //[SerializeField] private float bulletSpeed = 10f; // Speed of the bullets
    //[SerializeField] private float missleSpeed = 15f; // Speed of the missiles
    //[SerializeField] private float bigBulletSpeed = 12f; // Speed of the big bullets
    //[SerializeField] private float smallBulletSpeed = 8f; // Speed of the small bullets
    //[SerializeField] private float bulletLifetime = 2f; // Lifetime of the bullets in seconds


}

public enum ProjectileType
{
    Missle,
    BigBullet,
    SmallBullet
}