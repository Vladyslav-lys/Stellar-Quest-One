using UnityEngine;

public class FireProjectileGroundEnemy : AbstractGroundEnemies
{
    public Projectile Projectile;
    public float FireRate;
    private float _canFireIn;

    void LateUpdate()
    {
        if(_canFireIn > 0)
            _canFireIn -= Time.deltaTime;
        
        _animator.SetFloat("Speed", _speed);
    }
    
    protected override void AttackPlayer()
    {
        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistance, PlayerMask);
             
        if (attackPlayer.collider)
        {
            _speed = 0;
            if (_canFireIn > 0)
                return;
            
            var projectile = (Projectile)Instantiate (Projectile, new Vector3(transform.position.x, transform.position.y + 0.7f, 0), transform.rotation);
            projectile.Initialize (gameObject, _direction, _controller._velocity);
            _canFireIn = FireRate;
        }
        else
        {
            _speed = StartSpeed;
        }
    }
}