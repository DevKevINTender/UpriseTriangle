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
        FindObjectOfType<PersonSkinComponent>().GetComponent<SpriteRenderer>().color = new Color32(36,38,46,255);
    }

    public void DeInitComponent()
    {
        transform.parent.GetComponent<CircleCollider2D>().enabled = true;
        FindObjectOfType<PersonSkinComponent>().GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ObstacleComponent>())
        {
            Destroy(other.gameObject);
            GameObject shieldAnimComp = Instantiate(shieldAnimPb, transform.parent.position, Quaternion.identity);
            substractBonus(1, 1);
        }
    }
}
