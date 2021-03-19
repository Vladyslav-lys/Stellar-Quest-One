using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{
    private Player _player;
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public void Update()
    {
        if (transform.position.y > (_player.transform.position.y - _player.GetComponent<CharacterController2D>()._boxCollider.size.y / 1.5))
        {
            gameObject.GetComponent<BoxCollider2D> ().enabled = false;
            gameObject.layer = 0;
        } 
        else
        {
            gameObject.GetComponent<BoxCollider2D> ().enabled = true;
            gameObject.layer = 9;
        }
    }
}
