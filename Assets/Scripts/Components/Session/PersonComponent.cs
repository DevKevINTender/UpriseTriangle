using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PersonComponent : MonoBehaviour
{
    public delegate void PersonDelegate();
    private PersonDelegate personDeathTrigger;
    private PersonDelegate personWinTrigger;
    private PersonDelegate personEndWinTrigger;
    private bool canMove;
    internal bool inElevator;
    [SerializeField] private Rigidbody2D personRb;
    [SerializeField] private float moveToCenterTime;
    [SerializeField] private GameObject crackLeft;
    [SerializeField] private GameObject crackRight;

    float timer;

    private float vectroToRotate;
    private float targetvectroToRotate;
    
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
            //transform.rotation = Quaternion.Euler(0, 0, vector.normalized.x * -15);
            //if (Mathf.Abs(vector.x) > 0.1)
            {
                vectroToRotate = vector.normalized.x * -15;
            }
            transform.position += vector;            
        }
    }

    public void Update()
    {
        vectroToRotate = Mathf.MoveTowards(vectroToRotate, 0, Time.deltaTime * 60);
        targetvectroToRotate = Mathf.MoveTowards(targetvectroToRotate, vectroToRotate, Time.deltaTime * 60);
        transform.rotation = Quaternion.Euler( 0,0, targetvectroToRotate); 
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