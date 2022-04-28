using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldComponent : MonoBehaviour
{
    public delegate void BonusDel(int id, int count);

    public GameObject shieldAnimPb;

    private BonusDel substractBonus;
    public void InitComponent(BonusDel substractBonus)
    {
        this.substractBonus = substractBonus;
        transform.parent.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void DeInitComponent()
    {
        transform.parent.GetComponent<CircleCollider2D>().enabled = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ObstacleComponent>())
        {
            Destroy(other.gameObject);
            GameObject shieldAnimComp = Instantiate(shieldAnimPb, transform.parent);
            Destroy(shieldAnimComp,1);
            substractBonus(1, 1);
        }
    }
}
