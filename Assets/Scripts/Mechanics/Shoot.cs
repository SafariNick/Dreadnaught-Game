using UnityEngine;
using UnityEngine.Audio;

public class Shoot : MonoBehaviour
{
    public AudioClip shootSound;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();

        if (shootSound != null)
        {
            TryGetComponent(out audioSource);

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();

                audioSource.outputAudioMixerGroup = GameManager.Instance.sfxMixerGroup;
                Debug.LogWarning("AudioSource component missing. Added one dynamically.");
            }
        }
        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity = new Vector2(10f, 0f);
            
        }
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
        audioSource?.PlayOneShot(shootSound);

    }


    public void FireBigBullet()
    {
        if (sr.flipX)
            Fire(bigBulletPrefab, leftBigBulletSpawn, new Vector2(-bigBulletVel.x, bigBulletVel.y));
        else
            Fire(bigBulletPrefab, rightBigBulletSpawn, bigBulletVel);
        audioSource?.PlayOneShot(shootSound);
    }

    public void FireSmallBullet()
    {
      
        if (sr.flipX)
            Fire(smallBulletPrefab, leftSmallBulletSpawn, new Vector2(-smallBulletVel.x, smallBulletVel.y));
        else
            Fire(smallBulletPrefab, rightSmallBulletSpawn, smallBulletVel);
        audioSource?.PlayOneShot(shootSound);
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