using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertPanelView : MonoBehaviour
{
    public Text Header;
    public Text Description;
    public Animator animator;

    public void InitView(string Header, string Description)
    {
        this.Header.text = $"{Header}";
        this.Description.text = $"{Description}";
    }

    public void ClosePanel()
    {
        animator.Play("CloseAnim");
        StartCoroutine(WaitAnimationStartEnd(animator.runtimeAnimatorController.animationClips[1].length));  
    }

    private IEnumerator WaitAnimationStartEnd(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(this.gameObject);
    }
}
