using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoboHelperView : MonoBehaviour
{
    public Sequence tPanelAnim;
    void Start()
    {
         InvokeRepeating("Anim",0,4);
    }

    public void Anim()
    {
        tPanelAnim.Kill();
        tPanelAnim = DOTween.Sequence();
        tPanelAnim.Append(transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 250), 1f));
        tPanelAnim.AppendInterval(1f);
        tPanelAnim.Append(transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 1f));
        tPanelAnim.AppendInterval(1f).OnComplete(() => { });
 
            
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
