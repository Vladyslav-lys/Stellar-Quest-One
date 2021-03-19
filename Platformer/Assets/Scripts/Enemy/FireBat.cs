using UnityEngine;

public class FireBat : AbstractFlyEnemies
{
    public Projectile Projectile;
    public float FireRate;
    private float _canFireIn;

    void LateUpdate()
    {
        if(_canFireIn > 0)
            _canFireIn -= Time.deltaTime;
    }
    
    protected override void AttackPlayer()
    {
        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            !_isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistance, PlayerMask);
        
        if (attackPlayer.collider)
        {
            Speed = 0;
            if (_canFireIn > 0)
                return;

            var projectile = (Projectile) Instantiate(Projectile, new Vector3(transform.position.x + 2, transform.position.y, 0), transform.rotation);
            projectile.Initialize (gameObject, _dir, new Vector2(0f, 0f));
            _canFireIn = FireRate;
        }
        else
        {
            Speed = StartSpeed;
        }
    }
}