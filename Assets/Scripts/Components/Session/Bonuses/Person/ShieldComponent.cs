using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldComponent : MonoBehaviour
{
    public delegate void BonusDel(int id, int count);

    private BonusDel substractBonus;
    public void InitComponent(BonusDel substractBonus)
    {
        this.substractBonus = substractBonus;
        transform.GetComponentInParent<PolygonCollider2D>().enabled = false;
    }

    public void DeInitComponent()
    {
        transform.GetComponentInParent<PolygonCollider2D>().enabled = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ObstacleComponent>())
        {
            Destroy(other.gameObject);
            substractBonus(1, 1);
        }
    }
}
