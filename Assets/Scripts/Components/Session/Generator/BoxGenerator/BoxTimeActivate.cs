using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxTimeActivate : MonoBehaviour
{
    [SerializeField] private List<float> temp;
    [SerializeField] private Levels levels;
    [SerializeField] private BoxPreGenerator boxPreGenerator;
    private List<float> timing;
    private bool canAction;
    private ElevatorComponent elevator;
    private int timingIndex;
    int paintNum; // индекс текущего рисунка из квадратов
    float boxTime; // время когда box бупает(1,5 сек примерно)


    void Start()
    {
        boxTime = boxPreGenerator.GetBoxTime();
        elevator = transform.parent.GetComponent<ElevatorComponent>();
        elevator.BoxTimeActivate = this;
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

    //итератор списка темпов с зацикливанием
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
                StartCoroutine(SetActiveSquares(timing[timingIndex]));
            }
        }
    }

    public IEnumerator SetActiveSquares(float _timing)
    {
        yield return new WaitForSeconds(_timing);
        for (int j = 0; j < levels.listBool[paintNum].GridSize.y; j++)
        {
            for (int i = 0; i < levels.listBool[paintNum].GridSize.x; i++)
            {
                if (levels.listBool[paintNum].GetCell(i, j))
                    boxPreGenerator.Spawn(i, j); // спавн box'a если bool = true в листе рисунков
            }
        }
        paintNum++;
        if (paintNum >= levels.listBool.Count) paintNum = 0; //зацикливание рисунков
        SetNextTiming();
        Invoke("Shake", boxTime);
    }

    public void Shake()
    {
        //transform.parent.parent.transform.gameObject.GetComponent<ShakeAnimation>().DoShake();
        transform.DOShakePosition(0.1f, 0.15f, 15, 0, false, true);
    }
}
