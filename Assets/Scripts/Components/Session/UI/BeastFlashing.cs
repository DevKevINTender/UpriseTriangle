using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BeastFlashing : MonoBehaviour
{
    [SerializeField] private List<Image> beastParts;

    public void Start()
    {
        Action();
    }

    public void InitChilds()
    {
        foreach (Transform childImage in transform)
            beastParts.Add(childImage.GetComponent<Image>());
    }

    public void Action()
    {
        InitChilds();
        Sequence beastAnim = DOTween.Sequence();
        beastAnim.AppendInterval(2f);
        foreach (Image beastPart in beastParts)
        {
            beastAnim.Append(beastPart.DOColor(Color.white, 0.2f));
            beastAnim.AppendInterval(0.1f);
        }
    }

    public void Flashing(Image image)
    {
        image.DOColor(Color.white, 0.2f);
    }
}
