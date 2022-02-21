using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObstacleComponent : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D cursorRB;
    [Header("Render")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteActive;
    [SerializeField] Sprite spritePassive;

    [Header("Start")]
    [SerializeField] float forvardMoveSpeed;
    [SerializeField] float startDelay;

    [Header("Rotate")]
    [SerializeField] float rotateSpeed = 10;
    [SerializeField] float rotateDuration = 1;
    [Header("Attack")]
    [SerializeField] float attackDelay = 0.5f;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackDuration;

    private Vector3 attachedTarget;

    void Start()
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
        yield return new WaitForSeconds(2);
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        float time = rotateDuration;
        while (time >= 0)
        {
            GetDirection();
            time -= Time.deltaTime;
            yield return null;
        }
        spriteRenderer.sprite = spriteActive;
        attachedTarget = target.transform.position + (target.transform.position - transform.position).normalized * 1f;
        yield return new WaitForSeconds(attackDelay);        
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
         while (Vector2.Distance(attachedTarget,transform.position) >= 0.02f)
         {
             transform.position = Vector3.Lerp(transform.position, attachedTarget, Time.deltaTime * attackSpeed);
             yield return null;
         }      
         yield return new WaitForSeconds(attackDuration);
         spriteRenderer.sprite = spritePassive;
         StartCoroutine(Rotate());
    }

    public void GetDirection()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
    }
}
