using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Array2DEditor;
using Services;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

// генератор коридора 1 линия
public class StaticBlockPathernGenerator : MonoBehaviour
{
    float BPM = 125; //2.08333333333 в секу 0.6
    float speed = 5f; // скорость перемещния игрока в зависимости от разрешения
    [Header("Settings")]
    [SerializeField] public float time;
    [SerializeField] public List<float> tempList;
    [Header("Info")]
    [SerializeField] public float lenght; // длина генерации
    [SerializeField] private float lenghtFill; // насколько заполнена длина
    //1, 0.5 , 0.25 , 2 , 4
    GameObject obsObj;
    
    [SerializeField] List<GameObject> blockPbList = new List<GameObject>();
    
    [SerializeField] public Array2DExampleEnum gridTemplate;


    #if (UNITY_EDITOR)
    [ContextMenu("GenerateByTemp")]
    private void GenerateByTemp()
    {
        DestroyChilds();
        
        lenght = time * speed; // высота генерации, к которой стримится текущая высота генерации
        BPM = 126;
        lenghtFill = CalculateTempList(); // насколько заполнена генерация по высоте
        int direction = Random.Range(0, blockPbList.Count);
        float step = speed / (BPM / 60); // 5 / 1.66 = 3
        float curLenght = transform.position.y; // текущая высота генерации
        
        bool generate = true; // генерация активна

        int templateY = gridTemplate.GridSize.y; // размер шаблона генерации по X
        int templateX = gridTemplate.GridSize.x; // размер шаблона генерации по Y
        int teplateCurrentY = templateY - 1; // текущая строка шаблона генерации
        int tempId = 0; // ид текущего темпа в музыке
        
        while (generate)
        {
            // генерация одной строки шаблона генерации
            for (int x = 0; x < templateX; x++)
            {
                switch ( gridTemplate.GetCell(x,teplateCurrentY))
                {
                    case GridEnum.Empty:
                    {
                        break;
                    }
                    case GridEnum.Static:
                    {
                        obsObj = PrefabUtility.InstantiatePrefab(blockPbList[0], transform) as GameObject;
                        obsObj.transform.position = new Vector2(x * 0.6f - 2.4f, curLenght);
                        break;
                    } 
                    case GridEnum.Coin:
                    {
                        obsObj = PrefabUtility.InstantiatePrefab(blockPbList[1], transform) as GameObject;
                        obsObj.transform.position = new Vector2(x * 0.6f - 2.4f, curLenght);
                        break;
                    }
                        
                }
            }

            // изменеие текущей высоты генерации
            if (curLenght + (step * tempList[tempId]) > lenght)
            {
                generate = false;
                break;
            }
            curLenght += (step * tempList[tempId]);
            
            // закцикленность шаблона генерации и шаблона темпа музыки
            teplateCurrentY--;
            tempId++;
            
            if (teplateCurrentY < 0)
            {
                teplateCurrentY = templateY - 1;
            }
            if (tempId >= tempList.Count)
            {
                tempId = 0;
            }
        }
    }
    #endif
    
    private float CalculateTempList()
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

    [ContextMenu("Clear")]
    private void DestroyChilds()
    {
        if (transform.childCount > 0)
            for (int i = transform.childCount; i > 0; --i)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
    }
}
