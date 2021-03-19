using UnityEngine;

public class SimpleProjectile : Projectile, ITakeDamage
{
    public int Damage;
    public float TimeToLive;
    public GameObject Effect;
    public AudioClip FireSound;

    public void Update()
    {
        if ((TimeToLive -= Time.deltaTime) <= 0) {
            DestroyProjectile();
            return;
        }
        transform.Translate (Direction * (Speed * Time.deltaTime), Space.World);
    }


    public void TakeDamage(int damage, GameObject instigator)
    {
        DestroyProjectile();
    }

    protected override void OnInitialized()
    {
        if(PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (FireSound, transform.position);
        
        Instantiate (Effect, transform.position, transform.rotation);
    }
    
    protected override void OnCollideOther(Collider2D other)
    {
        DestroyProjectile();
    }
    
    protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        takeDamage.TakeDamage(Damage, gameObject);
        DestroyProjectile();
    }
    
    private void DestroyProjectile()
    {
        Instantiate (Effect, transform.position, transform.rotation);
        Destroy (gameObject);
    }
}
