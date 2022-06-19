using System.Collections;
using System.Collections.Generic;
using Controlers;
using ScriptableObjects;
using UnityEngine;

public class CoinCollectorComponent : MonoBehaviour
{
    public BonusCollectorComponent BonusCollectorComponent;

    [SerializeField] private int coinWithOutMultiplier;
    [SerializeField] private int coinWithMultiplier;
    [SerializeField] private int SessionCointTotalCollect;
    [SerializeField] private GameObject coinPrintPb;
    
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
            Instantiate(coinPrintPb, other.transform.position, Quaternion.identity);
            increaseCoin = 0;
            if (skillInfo.skillType == SkillScrObj.SkillType.AddCoinIfCollectCoin)
            {
                float rnd = Random.Range(0f, 100f);
                if(rnd < skillInfo.skillValue) increaseCoin = 1;
            }
            if (skillInfo.skillType == SkillScrObj.SkillType.AddSegmentWhenCollectCoin)
            {
                float rnd = Random.Range(0f, 100f);
                if(rnd < skillInfo.skillValue) SegmentControler.UpcreaseSegment(1);
            }
            if (BonusCollectorComponent.GetMultiplierBonusCount() > 0)
            {
                CoinsControler.IncreaseCoins((coinWithOutMultiplier + increaseCoin));
                SessionCointTotalCollect += (coinWithOutMultiplier + increaseCoin);
            }
            else
            {
                CoinsControler.IncreaseCoins((coinWithOutMultiplier + increaseCoin));
                SessionCointTotalCollect += (coinWithOutMultiplier + increaseCoin);
            }
            Destroy(other.gameObject);
        }
    }

    public int GetSessionCoinTotalCollect()
    {
        return SessionCointTotalCollect;
    }
}
