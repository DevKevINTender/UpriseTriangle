using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObstacleComponent : MonoBehaviour
{
    [SerializeField] private float bpm = 125;
    [SerializeField] private GameObject waySprite;
    [SerializeField] internal GameObject target; // игрок берётся сам при генерации генератором
    [Header("Render")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] internal Sprite spriteActive;
    [SerializeField] internal Sprite spritePassive;
    [Header("Start")]
    [SerializeField] internal float startDelay; // время запуска курсора настраивается генератором
    [SerializeField] private CircleCollider2D circleCollider;

    private float appearTime = 1;
    internal float rotateDuration = 2;
    internal float attackDelay = 1;

    internal float attackSpeed = 10f;
    internal float appearSpeed = 5;

    private Vector3 attachedTarget;

    void Start()
    {
        rotateDuration = TempToTime(rotateDuration);
        appearTime = TempToTime(appearTime);
        attackDelay = TempToTime(attackDelay);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        StartCoroutine(StartDelay());
    }

    public float TempToTime(float value)
    {
        return 60 / bpm * value;
    }


    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(StartAction());
    }

    private IEnumerator StartAction()
    {
        float time = appearTime;
        while (time > 0)
        {
            transform.localPosition += transform.right * Time.deltaTime * appearSpeed;
            time -= Time.deltaTime / appearTime;
            yield return null;
        }
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
        StartCoroutine(WayScalling());
        yield return new WaitForSeconds(attackDelay);        
        StartCoroutine(Attack());
    }

    IEnumerator WayScalling()
    {
        float time = 0;
        while (time < 1)
        {
            waySprite.transform.localScale += new Vector3(0, time, 0);
            time += Time.deltaTime * attackDelay;
            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        circleCollider.enabled = true;
        while (Vector2.Distance(attachedTarget,transform.localPosition) >= 0.02f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, attachedTarget, Time.deltaTime * attackSpeed);
            yield return null;
        }      
        spriteRenderer.sprite = spritePassive;
        Destroy(gameObject);
    }

    public void SetDirection()
    {
        transform.right = (Vector2)(target.transform.localPosition - transform.localPosition);
        attachedTarget = target.transform.localPosition + (target.transform.localPosition - transform.localPosition).normalized * 10f;
    }
}
