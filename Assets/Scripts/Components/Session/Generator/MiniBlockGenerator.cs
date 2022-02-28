using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

// генератор коридора 1 линия
public class MiniBlockGenerator : MonoBehaviour
{
    float BPM = 100; //2.08333333333 в секу 0.6
    float speed = 5f; // скорость перемещния игрока в зависимости от разрешения
    [SerializeField] public float lenght; // длина генерации
    [SerializeField] private float lenghtFill; // насколько заполнена длина
    [SerializeField] public float temp;
    [SerializeField] public List<float> tempList;//1, 0.5 , 0.25 , 2 , 4
    float start; // стартовая точка генерации

    [SerializeField] List<GameObject> blockPbList = new List<GameObject>();
    
    
    //2.4 1.2 0.6 0.3 unity points
    void Start()
    {
        start = transform.position.y; // задается через позицию генератора
        //GenerateFivePointsLine();
        GenerateFivePointsLineTemp();
        lenghtFill = CalculateTempList();
    }

    public float CalculateTempList()
    {
        float lenghtSum = 0;
        float step = speed / (BPM / 60); // if BPM 100 " (speed  5) / 1.66 = 3 " 
        while (lenghtSum < lenght)
        {
            foreach (var tempItem in tempList)
            {
                if (lenghtSum + (step * tempItem) < lenght)
                {
                    lenghtSum += tempItem * step;
                }
                else
                {
                    return lenghtSum;
                }
            }
        }

        return lenghtSum;
    }
    private void GenerateFivePointsLineTemp()
    {
        int direction = Random.Range(0, blockPbList.Count);
        float step = speed / (BPM / 60); // 5 / 1.66 = 3
        float curLenght = 0;
        Vector2 spawnPos = new Vector2(0, start);
        bool generate = true;
        while (generate)
        {
            foreach (var tempItem in tempList)
            {
                Instantiate(blockPbList[direction], spawnPos, Quaternion.identity, transform);
                
                direction = ChangeDirection(direction);
                spawnPos = new Vector2(0,  start + curLenght + (step * tempItem));
                
                
                if (curLenght + (step * tempItem) > lenght)
                {
                    generate = false;
                    break;
                }
                curLenght += (step * tempItem);
            }
        }
    }
    private void GenerateFivePointsLine()
    {
        int direction = Random.Range(0, blockPbList.Count);
        float result = speed / (BPM / 60);
        int curLenght = (int)(lenght / (result * temp));
        Debug.Log(result);
        for (int i = 0; i <= curLenght ; i++)
        {
            Vector2 spawnPos = new Vector2(0, start + i * result * temp);
            Instantiate(blockPbList[direction], spawnPos, Quaternion.identity, transform);
            direction = ChangeDirection(direction);
        }
    }
    private int ChangeDirection(int direction)
    {
        if (direction == 0)
        {
            return direction +=1;
        }

        if (direction == blockPbList.Count - 1)
        {
            return direction-=1;
        }
        
        return direction += (Random.Range(0,2) == 0 ) ? 1 : -1 ;
    }
}
