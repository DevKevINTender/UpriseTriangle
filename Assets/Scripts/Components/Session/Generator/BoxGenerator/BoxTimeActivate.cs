using System.Collections;
using System.Collections.Generic;
using Array2DEditor;
using UnityEngine;

public class BoxTimeActivate : MonoBehaviour
{
    [SerializeField] private int ObstCount;
    [SerializeField] private List<float> temp;
    [SerializeField] private Levels levels;
    private List<float> timing;
    private bool canAction;
    private ElevatorComponent elevator;
    [SerializeField] private List<BoxObstacleComponent> squares = new List<BoxObstacleComponent>();
    private int timingIndex;
    int paintNum;


    void Start()
    {
        ObstCount = levels.listBool[0].GridSize.y * levels.listBool[0].GridSize.x;
        elevator = transform.parent.GetComponent<ElevatorComponent>();
        elevator.BoxTimeActivate = this;
        Invoke("AddSquaresToList", 0.2f);
        TempToTiming();
    }

    public void TempToTiming()
    {
        timing = new List<float>(temp);
        for (int i = 0; i < temp.Count; i++)
        {
            timing[i] = temp[i] * (60f / 125f);
        }
    }

    public void StartAction()
    {
        canAction = true;
        SetNextTiming();
        StartCoroutine(LiveTime());
    }

    private IEnumerator LiveTime()
    {
        yield return new WaitForSeconds(elevator.GetElevatorTime() - 3);
        canAction = false;
    }

    public void DeleteFromList(BoxObstacleComponent box)
    {
        squares.Remove(box);
    }

    public void AddToList(BoxObstacleComponent box)
    {
        squares.Add(box);
    }

    public void AddSquaresToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            squares.Add(transform.GetChild(i).GetComponent<BoxObstacleComponent>());
        }
    }

    public void SetNextTiming()
    {    
        if (canAction)
        {
            if (timingIndex < timing.Count)
            {
                StartCoroutine(SetActiveSquares(timing[timingIndex]));
                timingIndex++;
            }
            else
            {
                timingIndex = 0;
                SetNextTiming();
            }
        }
    }

    public IEnumerator SetActiveSquares(float _timing)
    {
        bool[] listObst = GetPaint(paintNum);
        yield return new WaitForSeconds(_timing);
        for (int i = 0; i < ObstCount; i++)
        {
            if (listObst[i])
            { 
                squares[i].Active();
            }
        }
        paintNum++;
        if (paintNum > levels.listBool.Count) paintNum = 0;
        SetNextTiming();
    }

    public bool[] GetPaint(int paint)
    {
        int k = 0;
        bool[] buf = new bool[ObstCount];
        for (int j = 0; j < levels.listBool[paint].GridSize.y; j++)
        {
            for (int i = 0; i < levels.listBool[paint].GridSize.x; i++)
            {
                buf[k] = levels.listBool[paint].GetCell(i,j);
                k++;
            }
        }
        return buf;
    }

}
