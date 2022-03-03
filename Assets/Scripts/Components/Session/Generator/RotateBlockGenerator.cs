using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlockGenerator : MonoBehaviour
{
    float BPM = 125; //2.08333333333 в секу 0.6
    float speed = 5f; // скорость перемещния игрока в зависимости от разрешения
    [SerializeField] private float time;
    [SerializeField] private float lenght; // длина генерации
    [SerializeField] private List<float> tempList;//1, 0.5 , 0.25 , 2 , 4
    private List<float> timeList;
    float start; // стартовая точка генерации

    [SerializeField] GameObject blockPb;

    public void TempToTiming()
    {
        timeList = new List<float>(tempList);
        for (int i = 0; i < tempList.Count; i++)
        {
            timeList[i] = tempList[i] * (60f / 126f);
        }
    }

    [ContextMenu("GenerateTunnelTemp")]
    private void TunnelTemp()
    {
        lenght = time * speed;
        DestroyChilds();
        start = transform.position.y;
        TempToTiming();
        int direction = ChangeDirection();
        float result = speed / (BPM / 60); // 5 / 1.66 = 3
        float curLenght = 0;
        Vector2 spawnPos = new Vector2(direction, start);
        while (curLenght < lenght)
        {
            foreach (var item in timeList)
            {
                Instantiate(blockPb, spawnPos, Quaternion.identity, transform);
                direction = ChangeDirection();
                spawnPos = new Vector2(direction,  start + curLenght + (result * item));
                curLenght += (result * item);
               
            }
        }
    }

    private int ChangeDirection()
    {

        return Random.Range(-2,2);
    }

    public void DestroyChilds()
    {
        if (transform.childCount > 0)
            for (int i = transform.childCount; i > 0; --i)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
    }
}
