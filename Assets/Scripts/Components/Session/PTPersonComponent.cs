using UnityEngine;

public class PTPersonComponent : MonoBehaviour
{
    public delegate void PersonDelegate();
    private PersonDelegate personDeathTrigger;
    private PersonDelegate personWinTrigger;
    private bool canMove;

    [SerializeField] private GameObject crackLeft;
    [SerializeField] private GameObject crackRight;

    float timer;

    public void SetCrackLeftActive(bool _active)
    {
        crackLeft.SetActive(_active);
    }

    public void SetCrackRightActive(bool _active)
    {
        crackRight.SetActive(_active);
    }


    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
 
    public void InitComponent(PersonDelegate personDeathTrigger, PersonDelegate personWinTrigger)
    {
        this.personDeathTrigger = personDeathTrigger;
        this.personWinTrigger = personWinTrigger;
    }

    public void Move(Vector3 vector)
    {
        if (canMove)
        {
            transform.position += vector;
            transform.rotation = Quaternion.Euler(0, 0, transform.position.x * 3);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ObstacleComponent>())
        {
            personDeathTrigger?.Invoke();
        }
        if (other.GetComponent<FinishLineComponent>())
        {
            personWinTrigger?.Invoke();
        }
    }
}