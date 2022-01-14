using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonComponent : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SessionCore SessionCore;
    [SerializeField] private AudioSource deathSong;
    public void InitComponent(SessionCore SessionCore)
    {
        this.SessionCore = SessionCore;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("FinishLine"))
        {
            deathSong.PlayOneShot(deathSong.clip);
            transform.GetComponent<Animator>().SetBool("Death", true);
            SessionCore.LoseSession(transform.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
            playerAnimator.SetBool("Death", true);
        }
    }
}
