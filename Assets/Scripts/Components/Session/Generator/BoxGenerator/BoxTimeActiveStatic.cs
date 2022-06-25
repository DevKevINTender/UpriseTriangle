using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTimeActiveStatic : BoxTimeActivate
{
    public override IEnumerator SetActiveSquares(float _timing)
    {
        yield return new WaitForSeconds(_timing);
        for (int j = 0; j < levels.listBool[paintNum].GridSize.y; j++)
        {
            for (int i = 0; i < levels.listBool[paintNum].GridSize.x; i++)
            {
                if (levels.listBool[paintNum].GetCell(i, j))
                    boxPreGenerator.Spawn(i, j, elevator.GetElevatorTime() - 2f); // спавн box'a если bool = true в листе рисунков
            }
        }
        paintNum++;
        if (paintNum < levels.listBool.Count) SetNextTiming(); // НЕ! зацикливание рисунков
        Invoke("Shake", boxTime);
    }
}
