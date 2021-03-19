using UnityEngine;
 
public class InstaKill : MonoBehaviour
{
    public bool Kill = true;
    public bool IsWater = false;
    
    public void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        if(Kill && IsWater)
            FindObjectOfType<LevelManager>().KillPlayer();
        
        if (Kill && !player.IsDead && player.TrapRateTime <= 0)
            player.GiveTrapsDamage();
        
    }
    
    public void setKill(bool kill)
    {
        Kill = kill;
    }
}
