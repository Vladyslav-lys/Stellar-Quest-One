using UnityEngine;

public class Coins : MonoBehaviour
{
    private Player _player;
    public GameObject Effect;
    public AudioClip CollectCoin;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player> () == null)
            return;

        if(PlayerPrefs.GetInt("Audio") != 0)
            AudioSource.PlayClipAtPoint (CollectCoin, transform.position);

        Instantiate (Effect, transform.position, transform.rotation);
        gameObject.SetActive (false);

        FloatingText.Show (string.Format ("+{0} ExyCoin", 1), "PointsStarText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1.5f, 50));
    }
}
