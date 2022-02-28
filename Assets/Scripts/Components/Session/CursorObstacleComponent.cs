using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObstacleComponent : MonoBehaviour
{
    [SerializeField] internal GameObject target;
    private Rigidbody2D cursorRB;
    [Header("Render")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] internal Sprite spriteActive;
    [SerializeField] internal Sprite spritePassive;

    [Header("Start")]
    [SerializeField] internal float startDelay;
    internal float forvardMoveSpeed = 15;

    internal float rotateSpeed = 500;
    internal float rotateDuration = 1;

    internal float attackDelay = 0.5f;
    internal float attackSpeed = 5;
    internal float attackDuration = 1.5f;

    private Vector3 attachedTarget;

    void Start()
    {
        cursorRB = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();        
    }

    public void Activate()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(StartAction());
    }

    private IEnumerator StartAction()
    {
        cursorRB.AddForce(transform.right * forvardMoveSpeed);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        float time = rotateDuration;
        while (time >= 0)
        {
            SetDirection();
            time -= Time.deltaTime;           
            yield return null;
        }       
        spriteRenderer.sprite = spriteActive;       
        yield return new WaitForSeconds(attackDelay);        
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
         while (Vector2.Distance(attachedTarget,transform.localPosition) >= 0.02f)
         {
             transform.localPosition = Vector3.Lerp(transform.localPosition, attachedTarget, Time.deltaTime * attackSpeed);
             yield return null;
         }      
         yield return new WaitForSeconds(attackDuration);
         spriteRenderer.sprite = spritePassive;
         Destroy(gameObject);
    }

    public void SetDirection()
    {
        transform.right = (Vector2)(target.transform.localPosition - transform.localPosition);
        attachedTarget = target.transform.localPosition + (target.transform.localPosition - transform.localPosition).normalized * 1f;
    }
}
