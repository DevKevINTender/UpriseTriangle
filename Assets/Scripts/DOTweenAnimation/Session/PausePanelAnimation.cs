using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelAnimation : MonoBehaviour
{
    public RectTransform topPanel;
    public RectTransform bottomPanel;
    public RectTransform mainPanel;
    public Image mainPanelImage;

    public Sequence pausePanelAnim;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.defaultTimeScaleIndependent = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OpenPanelAnim()
    {
        pausePanelAnim.Kill();
        pausePanelAnim = DOTween.Sequence();
        pausePanelAnim.AppendInterval(0.25f);
        pausePanelAnim.Append(mainPanel.DOAnchorPos(Vector2.zero, 0.25f));
        pausePanelAnim.Append(mainPanelImage.DOColor(new Color32(26,27,33,175), 0.25f)).SetEase(Ease.OutExpo);
        pausePanelAnim.Join(bottomPanel.DOAnchorPos(Vector2.zero, 1)).SetEase(Ease.OutExpo);
        pausePanelAnim.Join(topPanel.DOAnchorPos(Vector2.zero, 1)).SetEase(Ease.OutExpo);
        //Sequence openPanel = DOTween.Sequence();    
        //openPanel.Append( transform.DOLocalMove(Vector3.zero, 1));
        //openPanel.AppendInterval(0.25f);
        //openPanel.Append( topPanel.DOLocalMove(Vector3.zero, 0.5f));
        //openPanel.Append( bottomPanel.DOLocalMove(Vector3.zero, 0.5f));

    }
    
    public void ClosePanelAnim(float duration)
    {
        pausePanelAnim.Kill();
        pausePanelAnim = DOTween.Sequence();
        pausePanelAnim.AppendInterval(duration);
        pausePanelAnim.Append(topPanel.DOAnchorPos(new Vector3(0,1000), 1)).SetEase(Ease.OutSine);
        pausePanelAnim.Join(bottomPanel.DOAnchorPos(new Vector3(0,-1000), 1)).SetEase(Ease.OutSine);
        pausePanelAnim.Join(mainPanelImage.DOColor(new Color32(26,27,33,0), 0.25f)).SetEase(Ease.OutSine);;
        pausePanelAnim.Append(mainPanel.DOAnchorPos(new Vector3(1000, 0 ), 1));


    }
}
