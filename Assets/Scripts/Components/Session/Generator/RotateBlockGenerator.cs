using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlockGenerator : MonoBehaviour
{
    float BPM = 100; //2.08333333333 в секу 0.6
    float speed = 5f; // скорость перемещния игрока в зависимости от разрешения
    [SerializeField] public float lenght; // длина генерации
    [SerializeField] public float temp;
    [SerializeField] public List<float> tempList;//1, 0.5 , 0.25 , 2 , 4
    float start; // стартовая точка генерации

    [SerializeField] GameObject blockPb;
    
    
    //2.4 1.2 0.6 0.3 unity points
    void Start()
    {
        start = transform.position.y; // задается через позицию генератора
        //GenerateFivePointsLine();
        TunnelTemp();
    }
    private void TunnelTemp()
    {
        int direction = ChangeDirection();
        float result = speed / (BPM / 60); // 5 / 1.66 = 3
        float curLenght = 0;
        Vector2 spawnPos = new Vector2(direction, start);
        while (curLenght < lenght)
        {
            foreach (var item in tempList)
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
}
