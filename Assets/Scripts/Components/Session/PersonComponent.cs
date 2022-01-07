using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonComponent : MonoBehaviour
{
    [SerializeField] private SessionCore SessionCore;
    [SerializeField] private AudioSource deathSong;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject effect;
    public void InitComponent(SessionCore SessionCore)
    {
        this.SessionCore = SessionCore;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("FinishLine"))
        {
            deathEffect.SetActive(true);
            effect.SetActive(false);
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            deathSong.PlayOneShot(deathSong.clip);
            SessionCore.LoseSession();
        }
    }
}
