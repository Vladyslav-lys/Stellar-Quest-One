using UnityEngine;

public class AttackBat : AbstractFlyEnemies
{
    public float AttackRate;
    [SerializeField]
    private float _canAttack;
    
    void LateUpdate()
    {
        if(_canAttack != 0)
            _canAttack -= Time.deltaTime;
    }
    
    protected override void AttackPlayer()
    {
        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            !_isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistance, PlayerMask);
        
        if (attackPlayer.collider)
        {
            Speed = 0;
            if (_canAttack > 0)
                return;

            _animator.SetTrigger("Attack");
            _player.TakeDamage(Damage);
            _canAttack = AttackRate;
        }
    }
    
    public override void TakeDamage(int damage, GameObject instigator)
    {
        _health -= damage;
        FloatingText.Show (string.Format ("-{0} HP", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1f, 120));

        if(_health <= 0)
        {
            ChangeRigidbodyType2D(RigidbodyType2D.Dynamic);
            _animator.SetBool("IsDead", true);
            _isDead = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(WaitDead());
        }
    }
    
    private void ChangeRigidbodyType2D(RigidbodyType2D Type)
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        
        body.bodyType = Type;
    }
}
