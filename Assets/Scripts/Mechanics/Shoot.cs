using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer sr;
    private Vector2 initShotVelocity = Vector2.zero; //bullet speed


    [SerializeField] private Vector2 missleVel = Vector2.zero; //bullet speed
    [SerializeField] private Vector2 bigBulletVel = Vector2.zero; //bullet speed
    [SerializeField] private Vector2 smallBulletVel = Vector2.zero; //bullet speed


    [SerializeField] private Projectile misslePrefab = null;
    [SerializeField] private Projectile bigBulletPrefab = null;
    [SerializeField] private Projectile smallBulletPrefab = null;
    //[SerializeField] private Transform rightSpawn;
    //[SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform leftMissleSpawn;
    [SerializeField] private Transform leftBigBulletSpawn;
    [SerializeField] private Transform leftSmallBulletSpawn;
    //[SerializeField] private Transform currentSpawn;
    [SerializeField] private Transform rightMissleSpawn;
    [SerializeField] private Transform rightBigBulletSpawn;
    [SerializeField] private Transform rightSmallBulletSpawn;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity = new Vector2(10f, 0f);
            //Debug.LogWarning("Initial shot velocity not set, using default value: " + initShotVelocity);
        }
        
        

        //if (currentSpawn == null)
        //{
        //    Debug.LogError("Spawn points not set. Please assign leftSpawn and rightSpawn in the inspector.");
        //}

        if (misslePrefab == null || bigBulletPrefab == null || smallBulletPrefab == null)
        {
            Debug.LogError("Projectile prefabs not assigned. Please assign misslePrefab, bigBulletPrefab, and smallBulletPrefab in the inspector.");
        }
    }

    public void FireMissle()
    { 
        if (sr.flipX)
            Fire(misslePrefab, leftMissleSpawn, new Vector2(-missleVel.x, missleVel.y));
        else
            Fire(misslePrefab, rightMissleSpawn, missleVel);

    }


    public void FireBigBullet()
    {
        if (sr.flipX)
            Fire(bigBulletPrefab, leftBigBulletSpawn, new Vector2(-bigBulletVel.x, bigBulletVel.y));
        else
            Fire(bigBulletPrefab, rightBigBulletSpawn, bigBulletVel);
    }

    public void FireSmallBullet()
    {
      
        if (sr.flipX)
            Fire(smallBulletPrefab, leftSmallBulletSpawn, new Vector2(-smallBulletVel.x, smallBulletVel.y));
        else
            Fire(smallBulletPrefab, rightSmallBulletSpawn, smallBulletVel);
    }
    private void Fire(Projectile projectileToFire, Transform Spawn, Vector2 shotVel)
    {
        Projectile curProjectile;

        curProjectile = Instantiate(projectileToFire, Spawn.position, Quaternion.identity);
        curProjectile.SetVelocity(shotVel);

        if (curProjectile.projectileType == ProjectileType.Missle)
            curProjectile.GetComponent<SpriteRenderer>().flipX = sr.flipX;
        
    }
}