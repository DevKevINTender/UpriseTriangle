using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BeastFlashing : MonoBehaviour
{
    [SerializeField] private List<Image> beastParts;
    private float delay;
    private float coloringTime;

    public void Start()
    {
        delay = 0.2f;
        coloringTime = 0.3f;
        Action();
    }

    public void InitChilds()
    {
        foreach (Transform childImage in transform)
            beastParts.Add(childImage.GetComponent<Image>());
    }

    public void Action()
    {
        TweenCallback callback = () => { BlackoutParts(); };
        InitChilds();
        Sequence beastAnim = DOTween.Sequence();
        beastAnim.AppendInterval(5f);
        foreach (Image beastPart in beastParts)
        {
            beastAnim.Append(beastPart.DOColor(Color.white, coloringTime));
            beastAnim.AppendInterval(delay);
            if (coloringTime >= 0.1f) coloringTime -= 0.05f;
            if (delay >= 0.1f) delay -= 0.05f;
        }
        beastAnim.AppendInterval(1f);
        beastAnim.OnComplete(callback);
    }

    public void BlackoutParts()
    {
        foreach (Image beastPart in beastParts)
        {
            beastPart.DOColor(new Color32(255, 255, 255, 0), 0.5f);
        }
    }
}
