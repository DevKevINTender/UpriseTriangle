using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTimeActivate : MonoBehaviour
{
    [SerializeField] private int ObstCount;
    [SerializeField] private List<float> temp;
    private List<float> timing;
    private bool canAction;
    private ElevatorComponent elevator;
    [SerializeField] private List<BoxObstacleComponent> squares = new List<BoxObstacleComponent>();
    private int timingIndex;
    
    void Start()
    {
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
        int obstCountBuff = ObstCount;
        yield return new WaitForSeconds(_timing);
        if (obstCountBuff > squares.Count) obstCountBuff = squares.Count;
        for (int i = 0; i < obstCountBuff; i++)
        {
            int randomIndex = Random.Range(0, squares.Count);
            squares[randomIndex].Active();
            DeleteFromList(squares[randomIndex]);
        }
        SetNextTiming();
    }


}
