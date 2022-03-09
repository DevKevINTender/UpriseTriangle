using System.Collections;
using UnityEngine;

public class PTPersonComponent : MonoBehaviour
{
    public delegate void PersonDelegate();
    private PersonDelegate personDeathTrigger;
    private PersonDelegate personWinTrigger;
    private PersonDelegate personEndWinTrigger;
    private bool canMove;
    internal bool inElevator;

    [SerializeField] private float moveToCenterTime;
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
 
    public void InitComponent(PersonDelegate personDeathTrigger, PersonDelegate personWinTrigger, PersonDelegate personEndWinTrigger)
    {
        this.personDeathTrigger = personDeathTrigger;
        this.personWinTrigger = personWinTrigger;
        this.personEndWinTrigger = personEndWinTrigger;
    }

    public void MoveToCenter()
    {
        StartCoroutine(MoveToCenterCor());
    }

    public IEnumerator MoveToCenterCor()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        yield return new WaitForSeconds(1f);
        Vector3 velosity = Vector3.zero;
        while (Vector3.Distance(transform.localPosition, Vector3.zero) > 0.1f)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref velosity, moveToCenterTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        personEndWinTrigger?.Invoke();
        float time = 0;
        while (time < 1)
        {
            transform.localPosition += Vector3.Lerp(Vector3.zero, Vector3.up * 8f, 2f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void Move(Vector3 vector)
    {
        if (canMove)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.position.x * 3);
            transform.position += vector;            
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