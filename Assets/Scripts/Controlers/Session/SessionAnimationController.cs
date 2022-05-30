using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SessionAnimationController : MonoBehaviour
{
    [SerializeField] private Animator panelAnimator, personAnimator;
    [SerializeField] private ElevatorAnimationComponent elevatorAnimationComponent;

    public float GetPersonDeathAnimLength()
    {
        return personAnimator.runtimeAnimatorController.animationClips[0].length;
    }

    public void PersonEnterElevator()
    {
        elevatorAnimationComponent.PersonEnterElevator();
    }

    public void PersoExitElevator()
    {
        elevatorAnimationComponent.PersoExitElevator();
    }


    public void Start()
    {
        DOTween.SetTweensCapacity(500, 50);
    }

    public void PersonWin()
    {
        personAnimator.SetBool("Win", true);
    }

    public void PersonDeath()
    {
        personAnimator.SetBool("Death", true);
    }

    public void StartPause()
    {
        panelAnimator.SetBool("Pause", true);
        personAnimator.SetBool("Pause", true);
    }

    public void EndPause()
    {
        panelAnimator.SetBool("Pause", false);
        personAnimator.SetBool("Pause", false);
    }

    private IEnumerator WaitUntilStart(float _time)
    {
        yield return new WaitForSecondsRealtime(_time);
        panelAnimator.SetBool("Start", true);
    }
}
