using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingElementComponent : MonoBehaviour
{
    [SerializeField] float TimerStart;
    [SerializeField] private float TimeStartShoot;
    [SerializeField] private float TimeDestroy;
    
    [SerializeField] Vector3 elementScale;
    [Range (1, 10)]
    [SerializeField] float elementSpeed;
    [SerializeField] int elementCount;
    [SerializeField] GameObject RingElementSectionPb;
    
    [SerializeField] private SpriteRenderer spawnerSprite;
    
    private float spawnAngle;

    void Start()
    {
        spawnerSprite.color = new Color32(255,255,255,0);
        spawnAngle = 360f / elementCount;
        StartCoroutine(TimeStartCor());
        StartCoroutine(TimeStartShootCor());
        StartCoroutine(TimeDestroyCor());
    }
    IEnumerator TimeStartCor()
    {
        float timer = TimerStart;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        spawnerSprite.color = new Color32(255,255,255,255);
        
    }
    IEnumerator TimeStartShootCor()
    {
        float timer = TimeStartShoot;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        SpawnBullet();
    }
   
    IEnumerator TimeDestroyCor()
    {
        float timer = TimeDestroy;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    public void SpawnBullet()
    {
        for (int i = 0; i < elementCount; i++)
        {
            GameObject newRingElementSectionObj = Instantiate(RingElementSectionPb, transform);
            newRingElementSectionObj.transform.localScale = elementScale;
            newRingElementSectionObj.GetComponent<RingElementSectionComponent>().SetSpeed(elementSpeed);
            newRingElementSectionObj.transform.Rotate(new Vector3(0,0,spawnAngle * i));
        }
    }
}
