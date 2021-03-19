using UnityEngine;

public class MainGroundEnemies : AbstractGroundEnemies
{
   private  float _canAttack;
   
   private void LateUpdate()
   {   
      if(_canAttack > 0)
         _canAttack -= Time.deltaTime;  
   }

   protected override void AttackPlayer()
   {
      RaycastHit2D attackPlayer = Physics2D.Raycast(gameObject.transform.position,
         _isFacingRight ? Vector2.right : Vector2.left, AttackPlayerDistance, PlayerMask);
      
      if (attackPlayer.collider)
      {
         _speed = 0;
         if (_canAttack > 0)
            return;
         
         _animator.SetTrigger("Attack");
         _player.TakeDamage(Damage);
         _canAttack = AttackRate;
      }
   }
}