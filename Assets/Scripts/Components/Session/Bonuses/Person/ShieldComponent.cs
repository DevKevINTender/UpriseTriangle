using System;
using System.Collections;
using System.Collections.Generic;
using Controlers;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShieldComponent : MonoBehaviour
{
    public delegate void BonusDel(int id, int count);

    public GameObject shieldAnimPb;
    public GameObject shieldSaveAnimPb;

    private BonusDel substractBonus;
    private SkillScrObj skillInfo;
    private GameObject shieldAnimComp; 
    public void InitComponent(BonusDel substractBonus)
    {
        this.substractBonus = substractBonus;
        skillInfo = SkillStorageContoler.GetSkillById(SkillStorageContoler.GetCurrentSkill());
        
        PersonScrObj personInfo = PersonStorageContoler.GetPersonById(PersonStorageContoler.GetCurrentPerson());

        transform.GetComponent<SpriteRenderer>().sprite = personInfo.PersonShield;
        shieldAnimPb.GetComponentInChildren<SpriteRenderer>().sprite = transform.GetComponent<SpriteRenderer>().sprite;
        shieldSaveAnimPb.GetComponentInChildren<SpriteRenderer>().sprite = transform.GetComponent<SpriteRenderer>().sprite;
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
            if (skillInfo.skillType == SkillScrObj.SkillType.ShieldProtect)
            {
                float rnd = Random.Range(0f, 100f);
                if (rnd < skillInfo.skillValue)
                {
                    shieldAnimComp = Instantiate(shieldSaveAnimPb, transform.parent.position, Quaternion.identity);
                    Debug.Log("State 1");
                }
                else
                {
                    shieldAnimComp = Instantiate(shieldAnimPb, transform.parent.position, Quaternion.identity);
                    substractBonus(1, 1);
                    Debug.Log("State 2");
                }
            }
            else
            {
                shieldAnimComp = Instantiate(shieldAnimPb, transform.parent.position, Quaternion.identity);
                substractBonus(1, 1);
                Debug.Log("State 3");
            }
            other.GetComponent<ObstacleComponent>().SelfDestroy();
        }       
    }
}
