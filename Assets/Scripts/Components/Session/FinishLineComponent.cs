using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineComponent : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    private float direction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            collisionPos = other.transform.position.x; // точка соприкасновения
            StartCoroutine(MoveToCollision(direction));
        }
    }

    private IEnumerator MoveToCollision(float _dir)
    {
        yield return new WaitForSeconds(1);
        winPanel.SetActive(true);
    }
}
