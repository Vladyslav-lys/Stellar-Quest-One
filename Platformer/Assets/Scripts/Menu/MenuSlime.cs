using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlime : MonoBehaviour
{
    protected Animator _animator;
    protected CharacterController2D _controller;
    protected Vector2 _direction;
    protected bool _isFacingRight;
    public Transform GroundDetection;
    public LayerMask GroundMask;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0); 
    }

    // Update is called once per frame
    void Update()
    {
        _controller.SetHorizontalForce(_direction.x * 3);
        _isFacingRight = transform.localScale.x > 0;
        CheckGround();
    }
    
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
        _direction = -_direction;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.localPosition =
            new Vector3(!_isFacingRight ? transform.localPosition.x + 2f : transform.localPosition.x - 2f,
                transform.localPosition.y, transform.localPosition.z);

    }
}
