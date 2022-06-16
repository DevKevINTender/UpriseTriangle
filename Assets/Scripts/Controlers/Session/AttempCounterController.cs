using Controlers;
using UnityEngine;

public class AttempCounterController : MonoBehaviour
{
    [SerializeField] private int currentId;
    private int attemp;

    public void InitControler(int id)
    {
        this.currentId = id;
    }
    public void AddAttemp()
    {
        attemp = GetAttemps();
        attemp++;
        SaveAttemps(attemp);
    }

    public void ClearAttemps()
    {
        attemp = 1;
        SaveAttemps(attemp);
    }

    public void SaveAttemps(int attemps)
    {
        LevelChooseControler.SetSessionAttempCount(currentId, attemps);
    }

    public int GetAttemps()
    {
        return LevelChooseControler.GetSessionAttempCount(currentId);
    }
}
