using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTimeService : MonoBehaviour
{
    [SerializeField] private int ObstCount;
    [SerializeField] private List<float> timing;
    private ElevatorComponent elevator;
    private List<SquareObstacleComponent> squares = new List<SquareObstacleComponent>();
    private int timingIndex;
    
    void Start()
    {
        elevator = transform.parent.GetComponent<ElevatorComponent>();
        elevator.squareTimeService = transform.GetComponent<SquareTimeService>();
        Invoke("AddSquaresToList", 0.2f);
    }


    public void StartAction()
    {
        SetNextTiming();
    }

    public float GetElevatortime()
    {
       return elevator.elevatorTime;
    }

    public void DeleteFromList(SquareObstacleComponent square)
    {
        squares.Remove(square);
    }

    public void AddToList(SquareObstacleComponent square)
    {
        squares.Add(square);
    }

    public void AddSquaresToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            squares.Add(transform.GetChild(i).GetComponent<SquareObstacleComponent>());
        }
    }

    public void SetNextTiming()
    {    
        if (GetElevatortime() > 0)
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
        yield return new WaitForSeconds(_timing);
        if (ObstCount > squares.Count) ObstCount = squares.Count;
        for (int i = 0; i < ObstCount; i++)
        {
            int randomIndex = Random.Range(0, squares.Count);
            squares[randomIndex].Active();
            DeleteFromList(squares[randomIndex]);
        }
        SetNextTiming();
    }


}
