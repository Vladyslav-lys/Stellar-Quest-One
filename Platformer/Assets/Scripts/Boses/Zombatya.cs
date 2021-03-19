using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zombatya : MonoBehaviour, ITakeDamage
{
    public float SearchPlayerDistance;
    public float SearchPlayerDistanceFlip;
    public float AttackPlayerDistanceShoot;
    public float StartSpeed;
    public float AttackSpeed;
    public int Health;
    public LayerMask PlayerMask;
    public float WeitSearch = 1f;
    public Projectile Projectile;
    public Projectile BigProjectile;
    public float FireRate;
    public float BigFireRate;
    public GameObject ZombatyaHealthBar;
    public Transform PathedProjectileLocation;
    public GameObject BigBulletEffect;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private CharacterController2D _controller;
    private float _speed;
    private Vector2 _direction;
    private bool _isFacingRight;
    private bool _isDead = false;
    private Player _player;
    private int _health;
    private float _canAttack;
    private bool _seePlayer = false;
    private float _canFlipSearch;
    private float _canFireIn;
    private float _canBigShoot;

    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRenderer;
    public Color MaxHealthColor = Color.red;
    public Color MinHealthColor = Color.green;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _health = Health;
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
    }

    public void Update()
    {
        ZombatyaHealth();

        if(_player.IsDead)
            _health = Health;

        if (_canFireIn > 0)
            _canFireIn -= Time.deltaTime;

        if (_canFlipSearch > 0)
            _canFlipSearch -= Time.deltaTime;

        if (_canAttack > 0)
            _canAttack -= Time.deltaTime;
        
        if (_canBigShoot > 0)
            _canBigShoot -= Time.deltaTime;

        _controller.SetHorizontalForce(_direction.x * _speed);
        _isFacingRight = transform.localScale.x > 0;
        _animator.SetFloat("Speed", _speed);
        CheckGround();
        SearchPlayer();
    }
    
    private void ZombatyaHealth()
    {
        float healthPercent = _health / (float)Health;
                
        ForegroundSprite.localScale = new Vector3 (healthPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp (MaxHealthColor, MinHealthColor, healthPercent);
    }
    
    private void SearchPlayer()
    {
        if (_isDead || _player.IsDead || _canAttack > 0 || _canFireIn > 0)
            return;
        
        RaycastHit2D searchPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, SearchPlayerDistance, PlayerMask);  
        
       
        RaycastHit2D searchPlayerFlip = Physics2D.Raycast(new Vector2(
                _isFacingRight ? gameObject.transform.position.x - 3 : gameObject.transform.position.x + 3, gameObject.transform.position.y),
            _isFacingRight ? Vector2.left : Vector2.right, SearchPlayerDistanceFlip, PlayerMask);
        
        if (searchPlayerFlip.collider)
            Flip();

        if (searchPlayer.collider)
        {
            _seePlayer = true;
            if (_canBigShoot <= 0)
            {
                _speed = AttackSpeed;
                var random = Random.Range(0, 2);
                if (random == 1)
                    BigShoot(); 
                else 
                    FireAttack();    
            }
            else
            {
                FireAttack();
            }       
        }
        else
        {
            _speed = StartSpeed;
            _seePlayer = false;
        }
    }

    private void CheckGround()
    {
        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) ||
            (_direction.x > 0 && _controller.State.IsCollidingRight))
        {
            _canFlipSearch = WeitSearch;
            Flip();
        }
    }

    private void Flip()
    {
        _direction = -_direction;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.localPosition =
            new Vector3(!_isFacingRight ? transform.localPosition.x + 3f : transform.localPosition.x - 3f,
                transform.localPosition.y, transform.localPosition.z);
    }

     
     private void BigShoot()
     {
         if (_canBigShoot > 0)
             return;
        
         RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
             _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistanceShoot, PlayerMask);
             
         if (attackPlayer.collider)
         {
             BigBulletEffect.SetActive(true);
             _speed = AttackSpeed;
             StartCoroutine(BigFire());
         }
         else
         {
             _speed = StartSpeed;
         }
         _canBigShoot = BigFireRate;
     }
     
    private void FireAttack()
    {
        if (_canFireIn > 0)
            return;
        
        RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistanceShoot, PlayerMask);
             
        if (attackPlayer.collider)
        {
            _speed = AttackSpeed;
            StartCoroutine(Fire());
        }
        else
        {
            _speed = StartSpeed;
        }
        _canFireIn = FireRate;
    }

    IEnumerator Fire()
    {
        _animator.SetTrigger("Fire");
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 2; i++)
        {
            var projectile = (Projectile)Instantiate (Projectile, PathedProjectileLocation.position, transform.rotation);
            projectile.Initialize (gameObject, _direction, _controller._velocity);
            yield return new WaitForSeconds(0.2f);
        }

        _speed = StartSpeed;
    }
    
    IEnumerator BigFire()
    {
        _animator.SetTrigger("Idle");
        _speed = AttackSpeed;
        _canFireIn = 2f;
        yield return new WaitForSeconds(1.5f);
        if (!_isDead)
        {
            BigBulletEffect.SetActive(false);
            _animator.SetTrigger("Fire");
            var bigProjectile = (Projectile)Instantiate (BigProjectile, PathedProjectileLocation.position, transform.rotation);
            bigProjectile.Initialize (gameObject, _direction, _controller._velocity);
            _speed = StartSpeed; 
        }
    }
    
    public void TakeDamage(int damage, GameObject instigator)
    {
        if(!_seePlayer)
            Flip();
        
        _health -= damage;
        FloatingText.Show (string.Format ("-{0} HP", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1f, 120));
        
        if(_health <= 0)
        {
            _controller._boxCollider.size = new Vector2(5.98f,6.86f);
            _animator.SetBool("IsDead", true);
            _isDead = true;
            _controller._boxCollider.enabled = false;
            Time.timeScale = 0.3f;
            StartCoroutine(WaitDead());
            ZombatyaHealthBar.SetActive(false);
        }
    }
    
    protected IEnumerator WaitDead()
    {
        _speed = 0;
        yield return new WaitForSeconds(4);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 3);
        
        if (!PlayerPrefs.HasKey("Forest_10"))
        {
            PlayerPrefs.SetInt("Forest_10", 0);
        }
        SceneManager.LoadScene("Forest_10");
        Time.timeScale = 1;
    }
}
