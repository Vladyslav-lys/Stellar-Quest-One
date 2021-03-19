using UnityEngine;

public class SawChangePath : MonoBehaviour, ITakeDamage
{
    public void TakeDamage(int damage, GameObject instigator)
    {
        gameObject.GetComponent<ObjectMoving>()._currentPoint.MoveNext();
    }
}
