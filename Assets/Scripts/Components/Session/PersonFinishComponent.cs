using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PersonComponent;
public class PersonFinishComponent : MonoBehaviour
{
    private PersonDelegate personWinTrigger;

    public void InitComponent(PersonDelegate personWinTrigger)
    {
        this.personWinTrigger = personWinTrigger;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<FinishLineComponent>())
        {
            personWinTrigger?.Invoke();
        }
    }
}
