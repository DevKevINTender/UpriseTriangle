using Components.Session;
using UnityEngine;

public class PTPersonComponent : MonoBehaviour
{
    public delegate void PersonDeathDelegate();
    private PersonDeathDelegate personDeathTrigger;
    private bool canMove;

    [SerializeField] private GameObject crackLeft;
    [SerializeField] private GameObject crackRight;

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
 
    public void InitComponent(PersonDeathDelegate personDeathTrigger)
    {
        this.personDeathTrigger = personDeathTrigger;
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
        if (!other.CompareTag("FinishLine") || !other.GetComponent<EnergyBarierComponent>())
        {
            personDeathTrigger?.Invoke();
        }
    }
}