using UnityEngine;

public class AutoStartToxicParticle : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
