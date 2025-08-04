using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;
    [SerializeField] private Projectile projectilePrefab = null;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity = new Vector2(10f, 0f);
            Debug.LogWarning("Initial shot velocity not set, using default value: " + initShotVelocity);
        }
        if (leftSpawn == null || rightSpawn == null || projectilePrefab == null)
        {
            Debug.LogError("Spawn points not set. Please assign leftSpawn and rightSpawn in the inspector.");
        }
    }

    public void Fire()
    {
        Projectile curProjectile;
        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, rightSpawn.position, Quaternion.identity);
            curProjectile.SetVelocity(initShotVelocity);
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, leftSpawn.position, Quaternion.identity);
            curProjectile.SetVelocity(new Vector2(-initShotVelocity.x, initShotVelocity.y));
        }
    }
}