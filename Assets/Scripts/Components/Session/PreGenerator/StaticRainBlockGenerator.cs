using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Components.Session.PreGenerator
{
    public class StaticRainBlockGenerator : MonoBehaviour
    {
        float BPM = 125; //2.08333333333 в секу 0.6
        float speed = 5f; // скорость перемещния игрока в зависимости от разрешения
        [Header("Settings")]
        [SerializeField] private float time;
        [SerializeField] private List<float> tempList;
        [Header("Info")]
        [SerializeField] private float lenght; // длина генерации
        [SerializeField] private float lenghtFill; // насколько заполнена длина
        //1, 0.5 , 0.25 , 2 , 4
        [Range(-1, 1)]
        [SerializeField] private int side;
        [Header("Resources")]
        [SerializeField] private List<GameObject> SpawnTemplate = new List<GameObject>();
        float start; // стартовая точка генерации
        void Start()
        {
            start = transform.position.y; // задается через позицию генератора
            lenghtFill = CalculateTempList();
        }
        
        #if (UNITY_EDITOR)
        
        [ContextMenu("GenerateRainLineTemp")]
        private void GenerateRainLineTemp()
        {
        
            start = transform.position.y;
            DestroyChilds();
            lenght = time * speed;
            lenghtFill = CalculateTempList();
            
          
            float stepY = speed / (BPM / 60); // 5 / 1.66 = 3
            float stepX = 0.6f;
            float xPos = stepX * 4 * -side;
            float yPos = 0;
            float curLenght = 0;
            bool generate = true;
            int templateId = 0;
            
            while (generate)
            {
                foreach (var tempItem in tempList)
                {
                    GameObject rainObsObj = Instantiate(SpawnTemplate[templateId],transform);
                    rainObsObj.transform.position = new Vector2(xPos,  start + yPos);
                    yPos = curLenght + (stepY * tempItem);
                    templateId++;
                    if (templateId >= SpawnTemplate.Count)
                    {
                        templateId = 0;
                    }
                    xPos += 1 * stepX * side;
                    
                   
                    if(Mathf.Abs(xPos) > 4*stepX)
                    {
                        xPos = xPos > 0? 4 * -stepX: 4 * stepX;
                    }

                    if (curLenght + (stepY * tempItem) > lenght)
                    {
                        generate = false;
                        break;
                    }
                    curLenght += (stepY * tempItem);
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
}