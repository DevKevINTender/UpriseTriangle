using UnityEngine;

public class AttempCounter : MonoBehaviour
{
    private int attemp;

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
        PlayerPrefs.SetInt("attemps", attemps);
    }

    public int GetAttemps()
    {
        return PlayerPrefs.GetInt("attemps");
    }
}
