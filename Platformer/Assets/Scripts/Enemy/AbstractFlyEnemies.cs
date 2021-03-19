using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbstractFlyEnemies : MonoBehaviour, ITakeDamage
{
    public Transform[] Points;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;
    public int Damage;
    public int StartHealth;
    public int StartSpeed;
    public int AttackSpeed;
    public LayerMask PlayerMask;
    public float SearchPlayerDistance;
    public float SearchPlayerDistanceFlip;
    public float AttackPlayerDistance;
    public float WeitSearch = 1f;

    protected Vector2 _dir;
    protected int _health;
    protected bool _isDead = false;
    protected Animator _animator;

    private int _direction;
    protected  IEnumerator<Transform> _currentPoint;
    private Vector3 _boundsCenter;
    [SerializeField]
    protected bool _isFacingRight = true;
    protected Player _player;
    [SerializeField]
    private float _canFlipSearch;
    
    public void Start()
    {
        _dir = new Vector2(-1, 0);
        _player = FindObjectOfType<Player>();
        _health = (int)(StartHealth * 2.5);
        _animator = GetComponent<Animator>();
        _currentPoint = GetPathsEnumerator();
        _currentPoint.MoveNext();
        
        if (_currentPoint.Current == null)
            return;
        
        transform.position = _currentPoint.Current.position;
        Flip();
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Speed * Time.deltaTime); 
        
        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            Flip();
            _currentPoint.MoveNext();
            _canFlipSearch = WeitSearch;
        }

        SearchPlayer();
        
        if(_canFlipSearch > 0)
            _canFlipSearch -= Time.deltaTime;
 
    }

    private IEnumerator<Transform> GetPathsEnumerator()
    {
        if (Points == null || Points.Length < 1)
            yield break;
        
        _direction = 1;
        var index = 0;
        
        while (true) {
            yield return Points[index];

            if(Points.Length == 1)
                continue;
            if(index <= 0)
                _direction = 1;
            else if (index >= Points.Length - 1)
                _direction = -1;

            index = index + _direction;
        }
    }

    public void OnDrawGizmos()
    {

        if (Points == null || Points.Length < 2)
            return;

        var points = Points.Where(t => t != null).ToList ();
        
        if (points.Count < 2)
            return;
        
        for(var i = 1; i < points.Count; i++)
        {
            Gizmos.DrawLine (points[i - 1].position, points[i].position);
        }
    }

    private void SearchPlayer()
    {
        if (_isDead || _player.IsDead)
            return;

        RaycastHit2D searchPlayer = Physics2D.Raycast(gameObject.transform.position,
            !_isFacingRight ? Vector2.right : Vector2.left, SearchPlayerDistance, PlayerMask);

        Speed = searchPlayer.collider ? AttackSpeed : StartSpeed;
        if (searchPlayer.collider)
            AttackPlayer();
        
        if(_canFlipSearch > 0)
            return;
        
        RaycastHit2D searchPlayerFlip = Physics2D.Raycast(new Vector2(
                !_isFacingRight ? gameObject.transform.position.x - 3 : gameObject.transform.position.x + 3, gameObject.transform.position.y),
            !_isFacingRight ? Vector2.left : Vector2.right, SearchPlayerDistanceFlip, PlayerMask);
        
        if (searchPlayerFlip.collider)
        {        
            Flip();
            _currentPoint.MoveNext();
            _canFlipSearch = WeitSearch;
        }
    }
    
    protected virtual void AttackPlayer(){}
    
    private void Flip()
    {  
        _dir = -_dir;
        _isFacingRight = !_isFacingRight;
        transform.rotation *= Quaternion.Euler(0, _isFacingRight ? 180f : -180f, 0);
    }

    public virtual void TakeDamage(int damage, GameObject instigator)
    {    
        _health -= damage;
        FloatingText.Show (string.Format ("-{0} HP", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1f, 120));

        if(_health <= 0)
        {
            _animator.SetBool("IsDead", true);
            _isDead = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(WaitDead());
        }
    }

    protected IEnumerator WaitDead()
    {
        Speed = 0;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}