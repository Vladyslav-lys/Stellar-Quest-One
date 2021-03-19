using System.Collections;
using UnityEngine;

public abstract class AbstractGroundEnemies : MonoBehaviour, ITakeDamage
{
    public float AttackRate;
    public int Damage;
    public int StartHealth;
    public float SearchPlayerDistance;
    public float SearchPlayerDistanceFlip;
    public float AttackPlayerDistance;
    public float StartSpeed;
    public float AttackSpeed;
    public Transform GroundDetection;
    public LayerMask GroundMask;
    public LayerMask PlayerMask;
    public float WeitSearch = 1f;
   
    protected Animator _animator;
    protected CharacterController2D _controller;
    protected float _speed;
    protected Vector2 _direction;
    protected bool _isFacingRight;
    protected bool _isDead = false;
    protected Player _player;
    protected int _health;
    protected bool _seePlayer = false;
    public float _canFlipSearch;
    public AudioClip DeathSound;

    public void Start()
    {
        _player = FindObjectOfType<Player>();
        _health = (int)(StartHealth * 2.5);
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
        _speed = StartSpeed;
    }

    private void FixedUpdate()
    {
        _controller.SetHorizontalForce(_direction.x * _speed);
        _isFacingRight = transform.localScale.x > 0;
        
        if (_player.IsDead)
           _speed = StartSpeed;
        
        
        CheckGround();
        SearchPlayer();

        if (_canFlipSearch > 0)     
            _canFlipSearch -= Time.deltaTime;  
    }
    
    protected virtual void SearchPlayer()
    {
        if(_isDead || _player.IsDead)
            return;
        
        RaycastHit2D searchPlayer = Physics2D.Raycast(gameObject.transform.position,
            _isFacingRight ? Vector2.right : Vector2.left, SearchPlayerDistance, PlayerMask);          
        if (searchPlayer.collider)
        {
            _seePlayer = true;
            _speed = AttackSpeed;
                AttackPlayer();
        }
        else
        {
            _speed = StartSpeed;
            _seePlayer = false;
        }
            
        RaycastHit2D searchPlayerFlip = Physics2D.Raycast(new Vector2(
                _isFacingRight ? gameObject.transform.position.x - 3 : gameObject.transform.position.x + 3, gameObject.transform.position.y),
            _isFacingRight ? Vector2.left : Vector2.right, SearchPlayerDistanceFlip, PlayerMask);

        if (searchPlayerFlip.collider)
            Flip();
    }

    protected virtual void AttackPlayer(){}
    
    private void CheckGround()
    {
        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) ||
            (_direction.x > 0 && _controller.State.IsCollidingRight))
                Flip();
        
        
        RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, 2f, GroundMask);
        if (groundInfo.collider == false)
                Flip();       
    }
    
    private void Flip()
    {
        if (_canFlipSearch > 0)
            return;
        
        _canFlipSearch = WeitSearch;
        _direction = -_direction;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.localPosition =
            new Vector3(!_isFacingRight ? transform.localPosition.x + 2f : transform.localPosition.x - 2f,
                transform.localPosition.y, transform.localPosition.z);
    }
    
    public void TakeDamage(int damage, GameObject instigator)
    {
        if(!_seePlayer)
            Flip();
        
        _health -= damage;
        FloatingText.Show (string.Format ("-{0} HP", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1f, 120));

        if(_health <= 0)
        {
            _speed = 0;
            _animator.SetBool("IsDead", true);
            _isDead = true;
            _controller._boxCollider.enabled = false;
            StartCoroutine(WaitDead());
            if (PlayerPrefs.HasKey(gameObject.GetComponent<KillCount>().EnemyName))
            {
                PlayerPrefs.SetInt(gameObject.GetComponent<KillCount>().EnemyName, PlayerPrefs.GetInt(gameObject.GetComponent<KillCount>().EnemyName) + 1);
            }    
        }
    }

    protected IEnumerator WaitDead()
    {
        if(DeathSound != null && PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (DeathSound, transform.position);
        
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}