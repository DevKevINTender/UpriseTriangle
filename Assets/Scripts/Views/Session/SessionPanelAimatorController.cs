using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionPanelAimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public GameObject panelStart;

    void Start()
    {
        StartCoroutine(Wait(2f));
    }
    
    private IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
        animator.SetBool("StartGame", true);
        float clipLenght = animator.runtimeAnimatorController.animationClips[1].length;
        yield return new WaitForSeconds(clipLenght);
        //panelStart.SetActive(false);
    }
    
}
