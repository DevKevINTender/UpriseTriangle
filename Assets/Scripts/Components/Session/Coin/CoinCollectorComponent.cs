using System.Collections;
using System.Collections.Generic;
using Controlers;
using ScriptableObjects;
using UnityEngine;

public class CoinCollectorComponent : MonoBehaviour
{
    public BonusCollectorComponent BonusCollectorComponent;
    
    [SerializeField] private int coinMultiplier;
    [SerializeField] private int SessionCointTotalCollect;
    [SerializeField] private GameObject coinPrintPb;
    [SerializeField] private GameObject coinBonusPrintPb;
    [SerializeField] private GameObject segmentPrintPb;
    
    private SkillScrObj skillInfo;
    private int increaseCoin;
    // Start is called before the first frame update
    void Start()
    {
        skillInfo = SkillStorageContoler.GetSkillById(SkillStorageContoler.GetCurrentSkill());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CoinComponent>())
        {
            
            increaseCoin = 0;
            switch (skillInfo.skillType)
            {
                case SkillScrObj.SkillType.AddCoinIfCollectCoin:
                {
                    float rnd = Random.Range(0f, 100f);
                    if (rnd < skillInfo.skillValue)
                    {
                        increaseCoin = 1;
                        Instantiate(coinBonusPrintPb, other.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(coinPrintPb, other.transform.position, Quaternion.identity);
                    }
                    break;
                }
                case SkillScrObj.SkillType.AddSegmentWhenCollectCoin:
                {
                    float rnd = Random.Range(0f, 100f);
                    if (rnd < skillInfo.skillValue)
                    {
                        Instantiate(segmentPrintPb, other.transform.position, Quaternion.identity);
                        SegmentControler.UpcreaseSegment(1);
                    }
                    else
                    {
                        Instantiate(coinPrintPb, other.transform.position, Quaternion.identity);
                    }
                    break;
                }
                default:
                {
                    Instantiate(coinPrintPb, other.transform.position, Quaternion.identity);
                    break;
                }
            }
     
            if (BonusCollectorComponent.GetMultiplierBonusCount() > 0)
            {
                CoinsControler.IncreaseCoins((1 + increaseCoin) * coinMultiplier);
                SessionCointTotalCollect += ((1 + increaseCoin) * coinMultiplier);
            }
            else
            {
                CoinsControler.IncreaseCoins(1 + increaseCoin);
                SessionCointTotalCollect += (1 + increaseCoin);
            }
            Destroy(other.gameObject);
        }
    }

    public int GetSessionCoinTotalCollect()
    {
        return SessionCointTotalCollect;
    }
}
