using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour, ITakeDamage
{
    public List<GameObject> Fragments;

    private void Start()
    {
        Fragments = new List<GameObject>();
        foreach (Transform child in transform)
        {
            Fragments.Add(child.gameObject);
        }
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        if(Fragments.Count == 0)
            return;
        
        for (int i = 0; i < 32; i++)
        {
            GameObject fragmet;
            fragmet = Fragments[Random.Range(0, Fragments.Count)];
            fragmet.SetActive(false);
            Fragments.Remove(fragmet);
            gameObject.SetActive(Fragments.Count != 0);
        }
    }
}
