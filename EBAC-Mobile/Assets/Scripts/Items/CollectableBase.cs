using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource audioSource;

    public float timeToHide = 3f;
    public string compareTag = "Player";
    public string collectableTag;
    public ParticleSystem coinsparticleSystem;
    public GameObject graphicItem;

    


    public void Awake()
    {
        if (coinsparticleSystem != null) coinsparticleSystem.transform.SetParent(null);
        if (audioSource != null) audioSource.transform.SetParent(null);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnCollect()

    {
        if (coinsparticleSystem != null) coinsparticleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}
